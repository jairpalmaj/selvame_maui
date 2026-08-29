namespace ReciclaMe.Domain;

public interface ICategoryClassRepository
{
    Task<IReadOnlyList<CategoryClass>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task<CategoryClass> GetCategoryByClassId(int categoryClassId, CancellationToken cancellationToken = default);
}