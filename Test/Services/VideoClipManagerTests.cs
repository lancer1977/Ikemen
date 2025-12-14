using global::Spotabot.Interfaces;
using Google.Apis.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PolyhydraGames.Streaming.SQL.Models;
using Spotabot.Models;
using Spotabot.Services.Channel.Media;
using Spotabot.Services.Site;
using System.Diagnostics;
using DynamicData;
using PolyhydraGames.AI.Interfaces;
using PolyhydraGames.AI.Rest;
using PolyhydraGames.Core.Test;
using Spotabot.Setup;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;

namespace Spotabot.Test.Services;

[TestFixture]
public class AIServiceTests
{
    private MusicTriviaRepository _manager;

    [SetUp]
    public async Task Setup()
    {
        var config = Fixtures.GetConfig();
        var app = Fixtures.GetApp(
            config,
            x =>
            {
                x.AddSingleton<IAIEndpoint>(x=> new AIEndpoint("https://api.polyhydragames.com/ai"));
                x.AddHttpClient();
                x.AddLogging(x =>
                {
                    x.AddDebug();
                });
                x.AddSingleton<MusicTriviaRepository>();
            });

        _manager = app.Services.GetRequiredService<MusicTriviaRepository>();


    }
    //[Test]
    //public async Task GetFolders()
    //{
    //    var result = await _manager.GetTriviaCard("Oingo Boingo", "Dead Mans Party", "Dead Mans party");
    //    Console.WriteLine(result.Message);
    //    Assert.That(result.IsSuccess);
    //    Assert.That(result.Result != null);
    //}
}

[TestFixture]
public class VideoClipManagerTests
{
    private VideoClipManager _manager;

    [SetUp]
    public async Task Setup()
    {
        var config = Fixtures.GetConfig();
        var app = Fixtures.GetApp(config, x =>
        {
            x.AddSingleton<IChannelsManager>(x => x.GetMock<IChannelsManager>());
            x.AddSingleton<VideoClipManager>();
        });

        _manager = app.Services.GetRequiredService<VideoClipManager>();
        try
        {
            await _manager.InitializeAsync(
                 new User()
                 {
                     Id = Guid.Parse("ef4fec43245c4268840503ce53c557dd")
                 });
        }
        catch (Exception ex)
        {

        }

    }

    [Test]
    public async Task GetFolders()
    {
        var result = await _manager.GetFolders();
        Assert.That(result.Any());
    }

    [Test]
    public async Task GetRandomClip()
    {
        var result = await _manager.GetRandomClip();

        Assert.That(result != null);

        var message = "Url: " + result.Url;
        Trace.WriteLine("Trace - " + message);
        Debug.WriteLine("Debug - " + message);
        Console.WriteLine("Console - " + message);


    }

    [Test]
    public async Task GetClips()
    {
        var result = await _manager.GetClips("2stupiddogs");
        Assert.That(result.Any());
        foreach (var item in result)
        {
            var message = "Url: " + item.Url;
            Trace.WriteLine("Trace - " + message);
            Debug.WriteLine("Debug - " + message);
            Console.WriteLine("Console - " + message);
        }

    }
    [TestCase("School House Rock")]
    public async Task GetFiles(string folderName)
    {
        var result = await _manager.GetFiles(folderName);
        foreach (var item in result)
        {
            var message = "Url: " + item.Filename;
            Trace.WriteLine("Trace - " + message);
            Debug.WriteLine("Debug - " + message);
            Console.WriteLine("Console - " + message);
        }
        Assert.That(result.Any());

    }

    [Test]
    public void GetFullFolderUrl()
    {
        var result = new RemoteFolder("test", "https://cdn.polyhydragames.com/video/");
        var fullUrl = result.GetFullUrl();
        Assert.That(fullUrl, Is.EqualTo("https://cdn.polyhydragames.com/video/test"));
    }

    [Test]
    public void GetFullFileUrl()
    {
        var result = new RemoteFile("test.mp4", "https://cdn.polyhydragames.com/video/");
        var fullUrl = result.GetFullUrl();
        Assert.That(fullUrl, Is.EqualTo("https://cdn.polyhydragames.com/video/test.mp4"));
    }
}