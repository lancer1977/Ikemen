using Spotabot.Test.Models;
using Spotabot.Trivia;
using Spotabot.Trivia.Judges;
using SpotifyAPI.Web;
using System.Diagnostics;

namespace Spotabot.Test.Trivia;

[TestFixture]
public class ScoreTests
{
    public string User1 = "Breadcrumb";
    public string User2 = "seaninretro";
    public string User3 = "yoyoyopo5";

    /// <summary>
    ///     Get a test object for a FullTrack
    /// </summary>
    /// <param name="round"></param>
    [TestCase]
    public async Task WhoWon()
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("fulltrack.json");
        var round = new Round(1, track);
        round.AddVote(User1, "Carley ray jepsen - Cut To The Feeling");
        Task.Delay(1000).GetAwaiter().GetResult();
        round.AddVote(User2, "carley ray jepsen - Cut To The Feeling");

        //Test
    }

    /// <summary>
    ///     Get a test object for a FullTrack
    /// </summary>
    /// <param name="round"></param>
    //[TestCase("Carley ray jepsen - Cut To The Feeling")]
    //public async Task ArtistWasSimilar(string guess)
    //{
    //    //Prepare
    //    var track = TestDataHelper.GetTestFile<FullTrack>("fulltrack.json");
    //    //Test

    //    var scorecard = track.ArtistWasSimilar(guess);
    //    Assert.That(scorecard == 100);
    //}
    ///// <summary>
    ///// Get a test object for a FullTrack
    ///// </summary>
    ///// <param name="round"></param>
    //[TestCase("Carley ray jepsen - Cut To The Feeling")]
    //public async Task NameWasSimilar(string guess)
    //{

    //    //Prepare
    //    var track = TestDataHelper.GetTestFile<FullTrack>("fulltrack.json");
    //    //Test

    //    var scorecard = track.NameWasSimilar(guess);
    //    Assert.That(scorecard == 100);
    //}
    [TestCase("Carley ray jepsen - Cut To The Feeling")]
    public void JaccardJudge2Digit(string guess)
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("fulltrack.json");
        var round = new Round(1, track);
        round.AddVote(User1, "Carley ray jepsen - Cut To The Feeling");
        var judge = new JaccardJudge();
        var scorecard = judge.ScoreResponse(round.Track, new Answer(guess));
        Console.WriteLine(scorecard.Print());
        Assert.That(scorecard.Total > .65);
    }

    [TestCase("Carley ray jepsen - Cut To The Feeling")]
    public void LevinJudge2Digit(string guess)
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("fulltrack.json");
        var round = new Round(1, track);
        round.AddVote(User1, guess);
        var judge = new LevinJudge();
        var scorecard = judge.ScoreResponse(round.Track, new Answer(guess));
        Console.WriteLine(scorecard.Print());
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        Assert.That(scorecard.Total > 90);
    }

    [TestCase("Carly Rae Jepsen - Cut To The Feeling")]
    public void LevinJudgeOneToOne(string guess)
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("fulltrack.json");
        var round = new Round(1, track);
        round.AddVote(User1, "Carley ray jepsen - Cut To The Feeling");
        var judge = new LevinJudge();
        var scorecard = judge.ScoreResponse(round.Track, new Answer(guess));
        Console.WriteLine(scorecard.Print());
        Assert.That(scorecard.Total == 100);
    }

    [TestCase("Regular - I like to party", ExpectedResult = false)]
    public bool LevinJudgeBadMatch(string guess)
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("fulltrack.json");
        var round = new Round(1, track);
        round.AddVote(User1, guess);
        var judge = new LevinJudge();
        var scorecard = judge.ScoreResponse(round.Track, round.Votes.First().Choice);
        Console.WriteLine(scorecard.Print());
        return scorecard.Total > 60;
    }

    [TestCase("Carley ray jepsen - Cut To The Feeling", ExpectedResult = "carleyrayjepsencuttothefeeling")]
    public string Normalize(string value1)
    {
        return value1.NormalizeSong();
    }

    [TestCase("Carly Rae Jepsen - Cut To The Feeling")]
    public void ScoreCard(string value)
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("dead.json");
        var round = new Round(1, track);
        round.AddVote(User1, "the beatles - friend of the devill");
        round.AddVote(User2, "grateful dead - friend of the dev");
        round.AddVote(User3, "grateful dead - friend of the devil");
        var judge = new LevinJudge();

        var card = new ScoreCard(round, judge);
        foreach (var item in card.GeneralScores)
        {
            Trace.WriteLine(item.Key + ": " + item.Value);
        }

        Assert.That(card.GeneralScores.Count == 3);
        //ScoreCard 
    }

    //FormatScores

    [TestCase("Carly Rae Jepsen - Cut To The Feeling")]
    public void FormatScores(string value)
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("dead.json");
        var round = new Round(1, track);
        round.AddVote(User1, "the beatles - friend of the devill");
        round.AddVote(User2, "grateful dead - friend of the dev");
        round.AddVote(User3, "grateful dead - friend of the devil");
        var judge = new LevinJudge();

        var card = new ScoreCard(round, judge);
        foreach (var item in card.GeneralScores)
        {
            Trace.WriteLine(item.Key + ": " + item.Value);
        }

        var results = card.GeneralScores.FormatScores();
        Debug.WriteLine(results);
        Assert.That(true);
    }

    [TestCase("Carly Rae Jepsen - Cut To The Feeling")]
    public void GetCombinedScores(string value)
    {
        var track = TestDataHelper.GetTestFile<FullTrack>("dead.json");
        var round = new Round(1, track);
        round.AddVote(User1, "the beatles - friend of the devill");
        round.AddVote(User2, "grateful dead - friend of the dev");
        round.AddVote(User3, "grateful dead - friend of the devil");
        var judge = new LevinJudge();

        var card = new ScoreCard(round, judge);
        foreach (var item in card.GeneralScores)
        {
            Trace.WriteLine(item.Key + ": " + item.Value);
        }

        var rounds = new List<Round> { round };
        var scores = card.GeneralScores;
        var scoreCards = new List<ScoreCard> { card };
        var combinedscores = TriviaGameState.GetCombinedScores(rounds, judge, scoreCards);
        Assert.That(true);
    }
}