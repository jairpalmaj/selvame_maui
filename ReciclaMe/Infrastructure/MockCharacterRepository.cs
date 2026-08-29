using ReciclaMe.Domain;

namespace ReciclaMe.Infrastructure;

public sealed class MockCharacterRepository : ICharacterRepository
{
    private IReadOnlyList<Character>? _characters = null;
    
    public async Task<IReadOnlyList<Character>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (_characters is null)
        {
            _characters =
            [
                new Character(1, "Tucky", "Creativity", "\ue40a", "character1"),
                new Character(2, "Balam", "Agilidad", "\uebc4", "character2"),
                new Character(3, "Drílan", "Inteligencia", "\uefac", "character3"),
                new Character(4, "Flami", "Fuerza", "\uf6e6", "character4"),
            ];
        }
        
        return await Task.FromResult(_characters);
    }

    public async Task<Character> GetCharacterByIdAsync(int characterId, CancellationToken cancellationToken = default)
    {
        var items = await GetAllAsync(cancellationToken);
        return items.First(character => character.Id == characterId);
    }
}