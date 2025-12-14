using GamePlayer.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.Test;
using Spotabot.Setup;

namespace Spotabot.Test.Models;

public class RetroArchToGameTests
{
#pragma warning disable NUnit1032
    private readonly IHost _host;
#pragma warning restore NUnit1032
    public RetroArchToGameTests()
    {
        _host = TestHelpers.GetHost((ctx, services) => { services.AddRedis(ctx.Configuration); });
    }

    [SetUp]
    public async Task Setup()
    {
    }

    [TestCase("batman_forever", "USA")]
    public async Task ValidateCountry(string filename, string country)
    {
        var cache = _host.Services.GetRequiredService<ICacheService>();
        var game1 = await cache.Get("test123", async () =>
        {
            return new BatoceraGame
            {
                Path = "Test game"
            };
        });

        var game2 = await cache.Get("test123", async () => new BatoceraGame());
        Assert.That(game1.Path == game2.Path);
    }
}