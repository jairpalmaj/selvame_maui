using SQLite;

namespace ReciclaMe.Domain;

public sealed record UserProfile
{
    /// <summary>
    /// Goal to unlock awards.
    /// Con 20 puntos desbloquea el logro Guardián del Agua
    /// Con 40 puntos desbloquea el logro Héroe del Aire.
    /// Con 60 puntos desbloquea el logro Amigo del Bosque.
    /// Con 100 puntos desbloquea el logro Rey de la Jungla.
    /// </summary>
    public const int Progress = 100;
    [PrimaryKey]
    public string? Id { get; set; }
    public int CharacterId { get; set; }
    public int Points { get; set; } = 0;
}