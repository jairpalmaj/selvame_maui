namespace ReciclaMe.Domain;

public sealed record CategoryClass
{
    public int Id { get; set; }
    public Category Category { get; set; }
    public required string Title { get; set; }
    /// <summary>
    /// Relevant information about the item
    /// </summary>
    public string? CategoryInformation { get; set; }
    public bool IsRecyclable { get; set; }
}