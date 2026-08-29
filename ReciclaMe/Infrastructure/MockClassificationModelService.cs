using ReciclaMe.Domain;

namespace ReciclaMe.Infrastructure;

public sealed class MockClassificationModelService : IClassificationModelService
{
    private readonly ICategoryClassRepository _categoryClassRepository;
    
    public MockClassificationModelService(ICategoryClassRepository  categoryClassRepository)
    {
        _categoryClassRepository = categoryClassRepository;
    }

    public async Task<ModelClassification> GetClassificationAsync(CancellationToken cancellationToken = default)
    {
        return await GenerateRandomClassificationAsync();
    }

    public async Task<ModelClassification> GetClassificationAsync(Category category, CancellationToken cancellationToken = default)
    {
        var categories = await CreateClassificationsAsync();
        return categories.First(c => c.CategoryClass.Category == category);
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