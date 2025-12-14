namespace PolyhydraGames.Ikemen.Models
{
    public sealed record StageRef(
        Guid? Id = null,
        string? RelativeStagePath = null,
        string? DisplayName = null
    );
}