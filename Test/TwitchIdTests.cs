using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OAuth.Core.Interfaces;
using PolyhydraGames.Api.CDN;
using PolyhydraGames.Api.CDN.Services;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.Test;
using Spotabot.Models;
using Spotabot.Services.Frotz;
using Spotabot.Services.Site;
using Spotabot.Setup;

namespace Spotabot.Test;

public class FakeLoggerFactory : ILoggerFactory
{

    public void Dispose()
    {
        // TODO release managed resources here
    }
    public ILogger CreateLogger(string categoryName)
    {
        return new Logger<FakeLoggerFactory>(this);
    }
    public void AddProvider(ILoggerProvider provider)
    { 
    }
}
public static class Lyrics
{
    public static string Sweat =
       "Sweat!\r\nSweat!\r\nSweat!\r\nBorn for trouble, poised for action\r\nReady to spring at a moment's notice\r\nNerves like a trigger, waiting to be pulled\r\nCovered with sweat, it ain't nice\r\nSweat!\r\nHelp me please I'm burning up\r\nI got this fire in my heart\r\nWon't let me sleep, can't concentrate\r\nEven when it's cold I'm dripping sweat\r\nIt ain't nice\r\nSweat!\r\nRivers running down my back\r\nMakes me slippery, like a fish\r\nIf I don't stop, I might drown\r\nFalling down, down, down, down, not dead yet\r\nCovered with\r\nSweat\r\nThe cool boys bit the dust\r\nThey couldn't take the pressure\r\nThe cool girls got knocked up\r\nThey only wanted to have fun\r\n(Where did they go?)\r\nThey fell in low and suffered\r\n(Where did they go?)\r\nThey picked up guns and hammers\r\n(Where did they go?)\r\nWithout friction there's no heat\r\nWIthout heat there's no fire\r\nWithout fire there's no desire\r\nYou're making me hot, hot, hot, hot!\r\nSweat! Sweat!\r\nTake my baby, Saturday night\r\nIt's hundred and ten, it's alright\r\nClose the door to my little room\r\nStarting to sweat, fun starts soon\r\nSweat!\r\nPrincipal caught me after school\r\nGave me hell, called me a fool\r\nPointed his finger, at my face\r\nStarted to sweat all over the place\r\nFlowed like rivers, onto the floor\r\nI can take it, give me some more\r\nSweat!\r\nWar breaks out throughout the land\r\nDodging bullets in the sand\r\nEnemy's getting much to close\r\nSun beats down on the back of my neck\r\nFingers twitchin', covered with sweat\r\nCovered with sweat\r\nThe cool boys bit the dust\r\nThey couldn't take the pressure\r\nThe cool girls got knocked up\r\nThey only wanted to have fun\r\n(Where did they go?)\r\nThey fell in low and suffered\r\n(Where did they go?)\r\nThey picked up guns and hammers\r\n(Where did they go?)\r\nWithout friction there's no heat\r\nWIthout heat there's no fire\r\nWithout fire there's no desire\r\nYou're making me hot, hot, hot, hot!\r\nSweat!";
}
[TestFixture]
public class LyricTest
{
    [TestCase("zzfoopad")]
    [Test] public void AnsiNameTest(string value)
    {
        var ansi = AnsiHelper.GetANSI(value);
    }

    [TestCase(" test")]
    public void TestSplits(string value)
    {
        string text = Lyrics.Sweat;

        // Split on empty lines
        var parts = text.SplitOnEmptyLines();

        foreach (var part in parts)
        {
            Console.WriteLine($"[{part.Trim()}]");
        }
        Assert.That(parts.LastOrDefault() == "Sweat!");
    }
}

[TestFixture]
public class TwitchIdTests
{
    [Test]
    public void CreateFromInt_ShouldStoreCorrectly()
    {
        var id = new IdPoc(12345);
        Assert.That(id.ToString(), Is.EqualTo("12345"));
        Assert.That((int)id, Is.EqualTo(12345));
    }

    [Test]
    public void CreateFromString_ShouldStoreCorrectly()
    {
        var id = new IdPoc("12345");
        Assert.That(id.ToString(), Is.EqualTo("12345"));
        Assert.That((int)id, Is.EqualTo(12345));
    }

    [Test]
    public void AsInt_ShouldReturnValue_WhenValid()
    {
        var id = new IdPoc("54321");
        Assert.That((int)id == 54321);

    }




    [Test]
    public void EqualityOperator_ShouldWork()
    {
        var id = new IdPoc(3);
        var ids = new List<int>(){1, 2, 3
            }
        ;//[]}; 

        Assert.That(ids.Any(x => x == id));
    }


    [Test]
    public void ExplicitCastToInt_ShouldWork_WhenValid()
    {
        var id = new IdPoc("789");
        var intId = (int)id;
        Assert.That(intId, Is.EqualTo(789));
    }


    [Test]
    public void ToString_ShouldReturnEmpty_ForNullOrEmptyId()
    {
        var id = new IdPoc(null);
        Assert.That(id.ToString() == string.Empty);
    }
}

public static class Fixtures
{
    public static IConfiguration GetConfig()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets("a9b62041-bb80-4158-ac82-6b9632500d66"); // Use the UserSecretsId generated earlier

        return builder.Build();
    }

    public static IHost GetApp(IConfiguration config, Action<IServiceCollection>? onCollection = null)
    {
        return TestHelpers.GetHost((x, services) =>
        {
            x.Configuration = config;
            services.AddSingleton(x => PolyMocks.GetMock<ICdnUrlService>(x,
                z =>
                {
                    var urls = config.GetSection("CDN");
                    z.Setup(x => x.BaseUrl).Returns(urls["BasePath"]);
                    z.Setup(x => x.BaseImagesUrl).Returns(urls["ImagesPath"]);
                    z.Setup(x => x.BaseImagesGameUrl).Returns(urls["ImageGamesPath"]);

                }));
            services.AddSingleton<IMediaRequestService>(x => x.GetMock<IMediaRequestService>());
            services.AddHttpClient();
            services.AddRedis(config);
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton(x => x.GetMock<ICdnUrlService>(z => { z.Setup(x => x.BaseImagesGameUrl).Returns("https://cdn.polyhydragames.com/images/retro_v2"); }));
            services.AddSingleton<HttpClient>();
            services.AddSingleton<GameArtService>();
            services.AddLogging();
            onCollection?.Invoke(services);
        });
    }
}