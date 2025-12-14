namespace PolyhydraGames.Ikemen.Models
{
    public sealed record RegisterCompanionRequest(
        string MachineId,
        string CompanionVersion,
        string? Hostname = null,
        string? IkemenVersion = null
    );
}