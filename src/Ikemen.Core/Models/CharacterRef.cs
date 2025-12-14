namespace PolyhydraGames.Ikemen.Models;

/// <summary>
/// Reference a character either by Catalog Id (preferred later) or by relative path (POC).
/// Exactly one of (Id, RelativeDefPath) should be set.
/// </summary>
public sealed record CharacterRef(
    Guid? Id = null,
    string? RelativeDefPath = null,
    string? DisplayName = null
);