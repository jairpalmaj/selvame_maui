namespace ReciclaMe.Domain;

public interface ICharacterRepository
{
    Task<IReadOnlyList<Character>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Character> GetCharacterByIdAsync(int characterId, CancellationToken cancellationToken = default);
}