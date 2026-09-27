namespace ReciclaMe.Domain;

public interface IProfileRepository
{
    Task<bool> SaveAsync(UserProfile? profile, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(CancellationToken cancellationToken = default);
    Task<UserProfile?> GetAsync(CancellationToken cancellationToken = default);
    Task<bool> SaveProgressAsync(int points, CancellationToken cancellationToken = default);
    Task<bool> SaveCharacterAsync(int characterId, CancellationToken cancellationToken = default);
    Task<Character> GetCharacterAsync(CancellationToken cancellationToken = default);
    string ProfileId { get; }
    int CharacterId { get; }
}