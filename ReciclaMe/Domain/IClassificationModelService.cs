namespace ReciclaMe.Domain;

public interface IClassificationModelService
{
    Task<ModelClassification> GetClassificationAsync(CancellationToken cancellationToken = default);
    Task<ModelClassification> GetClassificationAsync(Category category, CancellationToken cancellationToken = default);
}