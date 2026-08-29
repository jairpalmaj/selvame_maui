using ReciclaMe.Domain;

namespace ReciclaMe.Infrastructure;

public sealed class SqliteProfileRepository : IProfileRepository
{
    private readonly ICharacterRepository  _characterRepository;
    public string ProfileId => Preferences.Get(AppPreferences.ProfileId, string.Empty);
    public int CharacterId => Preferences.Get(AppPreferences.CharacterId, 0);
    
    public SqliteProfileRepository(ICharacterRepository  characterRepository)
    {
        _characterRepository =  characterRepository;
    }

    public async Task<bool> SaveAsync(UserProfile? profile, CancellationToken cancellationToken = default)
    {
        if (profile is null)
        {
            return false;
        }

        await DatabaseService.GetInstance().InitAsync();
        if (profile?.Id is null)
        {
            profile?.Id = Guid.NewGuid().ToString();
            Preferences.Set(AppPreferences.ProfileId, profile?.Id);
            await DatabaseService.GetInstance().Database.InsertAsync(profile);
        }
        else
        {
            await DatabaseService.GetInstance().Database.UpdateAsync(profile);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(CancellationToken cancellationToken = default)
    {
        await DatabaseService.GetInstance().InitAsync();
        var profile = await GetAsync(cancellationToken);
        await DatabaseService.GetInstance().Database.DeleteAsync(profile);
        Preferences.Remove(AppPreferences.ProfileId);
        Preferences.Remove(AppPreferences.CharacterId);
        return true;
    }

    public async Task<UserProfile?> GetAsync(CancellationToken cancellationToken = default)
    {
        var profileId = Preferences.Get(AppPreferences.ProfileId, string.Empty);
        await DatabaseService.GetInstance().InitAsync();
        return await DatabaseService.GetInstance().Database.Table<UserProfile>().FirstOrDefaultAsync(p => p.Id == profileId);
    }

    public async Task<bool> SaveProgressAsync(int points, CancellationToken cancellationToken = default)
    {
        var profile = await GetAsync(cancellationToken);
        var pointsObtained = (profile?.Points ?? 0) + points;
        
        //check for negative values/
        if (pointsObtained <= 0)
        {
            pointsObtained = 0;
        }
        profile?.Points = pointsObtained;
        return await SaveAsync(profile, cancellationToken);
    }

    public async Task<bool> SaveCharacterAsync(int characterId, CancellationToken cancellationToken = default)
    {
        var profile = await GetAsync(cancellationToken);
        profile?.CharacterId = characterId;
        Preferences.Set(AppPreferences.CharacterId, characterId);
        return await SaveAsync(profile, cancellationToken);
    }

    public async Task<Character> GetCharacterAsync(CancellationToken cancellationToken = default)
    {
        var characterId = Preferences.Get(AppPreferences.CharacterId, 0);
        return await _characterRepository.GetCharacterByIdAsync(characterId, cancellationToken);
    }
}