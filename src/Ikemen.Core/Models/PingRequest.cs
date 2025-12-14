namespace PolyhydraGames.Ikemen.Models
{
    public sealed record PingRequest(
        string MachineId,
        string? Message = null
    );
}