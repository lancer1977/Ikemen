using Microsoft.Extensions.Hosting;
using OAuth.Core.Chat;
using PolyhydraGames.Core.Test;
using Spotabot.Setup;

namespace Spotabot.Test.AI;

public class AITests
{
    public AITests()
    {
        _host = TestHelpers.GetHost((ctx, services) => { services.AddRedis(ctx.Configuration); });
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("streamid", "test123");
    }

    [SetUp]
    public async Task Setup()
    {
    }


    [TestCase(
        "Here is a possible response: { \"message\": \"Hey Cosmic Groovers! Ready to soak up some good vibes and healing tunes? \", \"song\": {\"name\":\"Taco Tuesday\", \"artist\":\"Taco Tuesday\"} }")]
    [TestCase(
        "Here is a possible response: { \"message\": \"Hey Cosmic Groovers! Ready to soak up some good vibes and healing tunes? \", \"song\": null }")]
    [TestCase(
        "{ \"message\": \"Hey there, Cosmic Groovers! Ready to soak up some good vibes and healing tunes? \", \"song\": null } { \"message\": \"Just like the waves, let’s ride the ups and downs of life together. Peace, love, and good music, always. \", \"song\": null }")]
    public async Task GetResponseTests(string value)
    {
        var result = value.GetChatbotResponse();
        Assert.That(result != null);
        Assert.That(!string.IsNullOrEmpty(result.Message));
    }
#pragma warning disable NUnit1032
    private readonly IHost _host;
    private readonly HttpClient _client;
#pragma warning restore NUnit1032
}