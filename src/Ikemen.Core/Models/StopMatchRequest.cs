namespace PolyhydraGames.Ikemen.Models
{
    public sealed record StopMatchRequest(
        string MachineId,
        bool ForceKill = false
    );
}