using PolyhydraGames.Extensions;
using PolyhydraGames.Streaming.SQL.Models;
using Spotabot.Interfaces;

namespace Spotabot.Test.Models;

[TestFixture]
public class AIPersonalityTesting
{
    private readonly IAIPersonalitySource source = new AiPersonalityRepository(null);
    private readonly Guid Id = "EF4FEC43-245C-4268-8405-03CE53C557DD".ToGuid();

    public AIPersonalityTesting()
    {
        source.InitializeAsync(new User
        {
            Id = Id
        });
    }

    [TestCase(ExpectedResult = 2)]
    public async Task<int> CountTestItems()
    {
        var items = await source.GetItems();
        return items.Count();
    }
}