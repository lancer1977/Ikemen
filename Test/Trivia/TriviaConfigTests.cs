using Spotabot.Trivia;

namespace Spotabot.Test.Trivia;

[TestFixture]
public class TriviaConfigTests
{
    [TestCase("0", "0")]
    [TestCase("1", "0")]
    [TestCase("2", "0")]
    [TestCase("3", "10")]
    public void Invalid_Is_Invalid(string round, string songLengthRequest)
    {
        var config = new TriviaConfig(round, songLengthRequest);

        Assert.That(!config.IsValid);
    }

    [TestCase("15 30ads")]
    [TestCase("15_ 30_asdasdasdasadssd ")]
    [TestCase("15_ 30_asdasdasdasadssddasdaads ")]
    [TestCase("15_ 30_asdasdasdasadssddasdaads adsasads123312132")]
    public void OneArg(string args)
    {
        var config = new TriviaConfig(args);
        Assert.That(config.SongLength == 30);
        Assert.That(config.RoundCount == 15);
        Assert.That(config.IsValid);
    }

    [TestCase("15____", "30as")]
    [TestCase("15_---", "30___D_")]
    [TestCase("15*^&", "30")]
    [TestCase("15_", " 30_asdasdasdasadssddasdaads adsasads123312132")]
    public void TwoArgs(string args, string songLengthRequest)
    {
        var config = new TriviaConfig(args, songLengthRequest);
        Assert.That(config.SongLength == 30);
        Assert.That(config.RoundCount == 15);
        Assert.That(config.IsValid);
    }
}