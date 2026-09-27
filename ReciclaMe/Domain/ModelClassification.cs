namespace ReciclaMe.Domain;

public record ModelClassification
{
    public string? ImagePath { get; set; }
    public CategoryClass CategoryClass { get; init; }
    /// <summary>
    /// 0.0 - 1
    /// </summary>
    public float Accuracy { get; set; } = 0;
}