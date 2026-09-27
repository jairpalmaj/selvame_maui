using Microsoft.Maui.Graphics.Platform;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace ReciclaMe.Infrastructure;

public sealed class ImageService : IImageService
{
    private readonly string ImagePath = $"{FileSystem.Current.AppDataDirectory}/ImagePath/";
    
    private Stream DownSize(Stream content, float maxWidth = 224f, float maxHeight = 224f, bool disposeOriginal = false)
    {
        IImage image = PlatformImage.FromStream(content);
        IImage downSizeImage = image.Downsize(maxWidth, maxHeight, disposeOriginal);
        return downSizeImage.AsStream();
    }

    private string Save(Stream content, string fileName = "camera-view-image.png")
    {
        // Recommended directory for platform-safe app storage
        string filePath = Path.Combine(ImagePath, fileName);

        if (!Directory.Exists(ImagePath))
        {
            Directory.CreateDirectory(ImagePath);
        }

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        using var capturedImageStream = new MemoryStream();
        content.CopyTo(capturedImageStream);

        var imageBytes = capturedImageStream.ToArray();

        using var localFileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
        localFileStream.Write(imageBytes, 0, imageBytes.Length);
        localFileStream.Flush(flushToDisk: true);
        return filePath;
    }

    private void DeleteFile(string fileName = "img.png")
    {
        string filePath = Path.Combine(ImagePath, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public Stream ReadPhoto(string path)
    {
        return GetFileStream(path);
    }

    public void DeleteAll()
    {
        try
        {
            if (Directory.Exists(ImagePath))
            {
                Directory.Delete(ImagePath, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public string SavePicture(Stream stream, string fileName = "img.png")
    {
        var filePath = Save(stream, fileName); 
        return filePath;
    }

    public void DeletePicture(string fileName)
    {
        DeleteFile(fileName);
    }

    private Stream GetFileStream(string fileName)
    {
        string filePath = Path.Combine(ImagePath, fileName);
        return File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    }
    
    private Stream GetFileByPathStream(string path)
    {
        return File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public Stream ResizeByFileNameAsync(string fileName = "img.png")
    {
        var fileStream = GetFileStream(fileName);
        return DownSize(fileStream);
    }

    public Stream ResizeByPathAsync(string path)
    {
        var fileStream = GetFileByPathStream(path);
        return DownSize(fileStream);
    }
}