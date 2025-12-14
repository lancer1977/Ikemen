namespace PolyhydraGames.Ikemen.Models
{
    public sealed record LaunchMatchRequest(
        string MachineId,                 // which host to target
        CharacterRef P1,
        CharacterRef P2,
        int P1AiLevel = 8,
        int P2AiLevel = 8,
        MatchMode Mode = MatchMode.CpuVsCpu,
        StageRef? Stage = null,
        IReadOnlyDictionary<string, string>? ExtraArgs = null // optional future expansion
    );
}