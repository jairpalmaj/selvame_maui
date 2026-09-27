namespace ReciclaMe.Infrastructure;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SkiaSharp;

public class MobileNetService : IDisposable
{
    private InferenceSession? _session;
    private readonly string[] _classNames;

    public MobileNetService()
    {
        _classNames = ["paper", "metal", "cardboard", "trash", "glass", "plastic"];
    }

    // Carga asíncrona del modelo desde los recursos Raw de MAUI
    public async Task InitializeAsync()
    {
        if (_session is not null)
        {
            return;
        }

        var modelFileName = "mobilenetlarge-trash.onnx";
        await using var modelStream = await FileSystem.OpenAppPackageFileAsync(modelFileName);
        using var memoryStream = new MemoryStream();
        await modelStream.CopyToAsync(memoryStream);

        // Se inicializa la sesión en memoria nativa
        _session = new InferenceSession(memoryStream.ToArray());
    }

    // Recibe directamente un Stream de la imagen (cámara, galería o archivo)
    public (int classId, string Label, float Confidence, float[] AllProbabilities) Predict(Stream imageStream)
    {
        if (_session == null)
            throw new InvalidOperationException("El modelo no ha sido inicializado. Llama a InitializeAsync primero.");

        // 1. Decodificar la imagen usando SkiaSharp
        using var originalBitmap = SKBitmap.Decode(imageStream);

        // 2. Redimensionar a 224x224 (filtro Lanczos/High para mejor calidad)
        var resizeInfo = new SKImageInfo(224, 224, SKColorType.Rgba8888, SKAlphaType.Premul);
        using var resizedBitmap = originalBitmap.Resize(resizeInfo, SKSamplingOptions.Default);

        if (resizedBitmap == null)
            throw new InvalidOperationException("No se pudo redimensionar la imagen.");

        // 3. Crear el tensor en formato NCHW: [1, 3, 224, 224] (System.Single)
        var tensor = new DenseTensor<float>(new[] { 1, 3, 224, 224 });

        // Extraer píxeles canal por canal: R (0), G (1), B (2)
        for (int y = 0; y < 224; y++)
        {
            for (int x = 0; x < 224; x++)
            {
                SKColor color = resizedBitmap.GetPixel(x, y);

                // MobileNetV3 en Keras espera valores en rango [0.0, 255.0]
                tensor[0, 0, y, x] = color.Red;   // Canal R
                tensor[0, 1, y, x] = color.Green; // Canal G
                tensor[0, 2, y, x] = color.Blue;  // Canal B
            }
        }

        // 4. Preparar entrada ONNX
        string inputName = _session.InputMetadata.Keys.First();
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor(inputName, tensor)
        };

        // 5. Inferencia
        using var results = _session.Run(inputs);
        var outputTensor = results.First().AsTensor<float>();
        float[] probabilities = outputTensor.ToArray();

        // 6. ArgMax para obtener la clase de mayor probabilidad
        int maxIndex = 0;
        float maxProb = probabilities[0];
        for (int i = 1; i < probabilities.Length; i++)
        {
            if (probabilities[i] > maxProb)
            {
                maxProb = probabilities[i];
                maxIndex = i;
            }
        }

        return (maxIndex, _classNames[maxIndex], maxProb, probabilities);
    }

    public void Dispose()
    {
        _session?.Dispose();
    }
}