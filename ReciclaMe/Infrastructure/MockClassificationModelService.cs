using System.Diagnostics;
using ReciclaMe.Domain;
using ReciclaMe.Services;

namespace ReciclaMe.Infrastructure;

public sealed class MockClassificationModelService : IClassificationModelService
{
    private readonly ICategoryClassRepository _categoryClassRepository;
    private readonly IAnalyticsService _analyticsService;
    private readonly IImageService _imageService;
    private readonly MobileNetService _classifier;


    public MockClassificationModelService(ICategoryClassRepository  categoryClassRepository,
        IAnalyticsService analyticsService,
        IImageService imageService)
    {
        _classifier = new MobileNetService();
        _imageService = imageService;
        _analyticsService = analyticsService;
        _categoryClassRepository = categoryClassRepository;
    }

    public async Task<ModelClassification> GetClassificationAsync(CancellationToken cancellationToken = default)
    {
        var classification = await GenerateRandomClassificationAsync();
        return classification;
    }

    public async Task<ModelClassification> GetClassificationAsync(Category category, CancellationToken cancellationToken = default)
    {
        var categories = await CreateClassificationsAsync();
        var classification = categories.First(c => c.CategoryClass.Category == category);
        return classification;
    }

    public async Task<ModelClassification> GetClassificationAsync(string photoPath, CancellationToken cancellationToken = default)
    {
        var photoStream = _imageService.ReadPhoto(photoPath);
        await _classifier.InitializeAsync();
        var stopwatch = Stopwatch.StartNew();
        var (categoryId, label, confidence, probabilities) = _classifier.Predict(photoStream);
        stopwatch.Stop();
        var category = await _categoryClassRepository.GetCategoryByClassId(categoryId, cancellationToken);
        var classification = new ModelClassification
        {
            CategoryClass = category,
            Accuracy = confidence,
            ImagePath = photoPath
        };
        _analyticsService.EmitDistribution(AnalyticsKeys.ModelClassification, classification.Accuracy, type: DistributionType.Percentage, [
            new KeyValuePair<string, object>("Category", classification.CategoryClass.Category.ToString()),
        ]);
        _analyticsService.EmitDistribution(AnalyticsKeys.ClassificationTime, stopwatch.ElapsedMilliseconds, type: DistributionType.Milliseconds, [
            new KeyValuePair<string, object>("Category", classification.CategoryClass.Category.ToString()),
        ]);
        return classification;
    }

    private async Task<IReadOnlyList<ModelClassification>> CreateClassificationsAsync()
    {
        var categories = await _categoryClassRepository.GetCategoriesAsync();
        List<ModelClassification> classifications =
        [
            new ModelClassification
            {
                ImagePath = "cardboard",
                Accuracy = 0.80f,
                CategoryClass = categories[0]
            },
            new ModelClassification
            {
                ImagePath = "glass",
                Accuracy = 0.85f,
                CategoryClass = categories[1]
            },
            new ModelClassification
            {
                ImagePath = "metal",
                Accuracy = 0.75f,
                CategoryClass = categories[2]
            },
            new ModelClassification
            {
                ImagePath = "paper",
                Accuracy = 0.88f,
                CategoryClass = categories[3]
            },
            new ModelClassification
            {
                ImagePath = "plastic",
                Accuracy = 0.77f,
                CategoryClass = categories[4]
            },
            new ModelClassification
            {
                ImagePath = "trash",
                Accuracy = 0.70f,
                CategoryClass = categories[5]
            },
            //fake for testing
            new ModelClassification
            {
                ImagePath = "trash1",
                Accuracy = 0.60f, //menos de 70%
                CategoryClass = categories[5]
            },
        ];

        return classifications;
    }

    private async Task<ModelClassification> GenerateRandomClassificationAsync()
    {
        var categories = await CreateClassificationsAsync();
        int position = Random.Shared.Next(0, 7);
        return categories[position];
    }
}