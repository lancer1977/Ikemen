using Microsoft.Extensions.Logging;
using Moq;
using OAuth.Core.Interfaces;
using PolyhydraGames.Api.CDN.Services;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.Test;
using PolyhydraGames.Streaming.SQL.Models;
using Spotabot.Interfaces;
using Spotabot.Models;
using Spotabot.Services.Channel.Media;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolyhydraGames.Api.CDN;

namespace Spotabot.Test.Services;

[TestFixture]
public class AudioClipManagerTests
{ 
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private Mock<IMediaRequestService> _mediaRequestServiceMock;
    private Mock<IChannelsManager> _channelsManagerMock;
    private Mock<EndpointService> EndpointServiceMock;

    private AudioClipManager _manager;
#pragma warning disable NUnit1032
    public IHost App { get; }
#pragma warning restore NUnit1032
    public AudioClipManagerTests()
    {
        App = TestHelpers.GetHost((x, services) =>
        {
            services.AddSingleton(x => PolyMocks.GetMock<ICdnUrlService>(x, z => { z.Setup(x => x.BaseImagesGameUrl).Returns("https://cdn.polyhydragames.com/images/retro_v2"); }));
            services.AddSingleton<HttpClient>();
            services.AddSingleton<GameArtService>();
            services.AddLogging();
        });
        _manager = App.Services.GetService<AudioClipManager>();
    }

    [SetUp]
    public async Task Setup()
    { 
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _mediaRequestServiceMock = new Mock<IMediaRequestService>();
        _channelsManagerMock = new Mock<IChannelsManager>();
        EndpointServiceMock = new Mock<EndpointService>();
        var cacheMock = new Mock<ICacheService>();
        // Use a derived class to expose Source mock
        _manager = new AudioClipManager(
            new FakeLoggerFactory(),
            new HttpClientFactory(),
            _mediaRequestServiceMock.Object,
            _channelsManagerMock.Object, 
            EndpointServiceMock.Object,
            cacheMock.Object);
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
         

        var message = "Url: " + result.Url;
        Trace.WriteLine("Trace - " + message);
        Debug.WriteLine("Debug - " + message);
        Console.WriteLine("Console - " + message);


    }

    [Test]
    public async Task GetClips()
    {
        var result = await _manager.GetClips("");
        Assert.That(result.Any());
        foreach (var item in result)
        {
            var message = "Url: " + item.Url;
            Trace.WriteLine("Trace - " + message);
            Debug.WriteLine("Debug - " + message);
            Console.WriteLine("Console - " + message);
        }

    }
    [TestCase("")]
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
