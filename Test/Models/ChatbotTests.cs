using PolyhydraGames.AI.Models;
using PolyhydraGames.Core.Models;
using Spotabot.Models;
using Spotabot.Services.Channel.Twitch.Chat;

namespace Spotabot.Test.Models;

[TestFixture]
public class ChatbotTests
{
    [TestCase("chatresponse.json", "Just got done with my Hiroshima '98 harvest. This strain's go",
        "The Haxan Cloak - Black Sine")]
    public void ChatbotResponseIsLegal(string fileName, string messageStart, string song)
    {
        var data = TestExtensions.Get<AiResponseType>(fileName);
        var chat = data.ToChatbotResponse(true);
        Assert.That(chat.Message.StartsWith(messageStart));
        Assert.That(chat.Song.StartsWith(song));
    }


    [Test]
    public void ChatbotResponseWithSongs()
    {
        var data = TestExtensions.Get<List<AiResponseType>>("chatresponses.json");
        foreach (var aiResponseType in data)
        {
            var chat = aiResponseType.ToChatbotResponse(true);
            Console.WriteLine(chat.Message);
            Console.WriteLine(chat.Song);
            Assert.That(chat.Message != null);
            Assert.That(chat.Song != null);
        }
    }

    [Test]
    public void RecordJsonTest()
    {
        var media = new MediaRequest("fake id");
        var json = media.ToJson();
        var media2 = json.FromJson<MediaRequest>();
        Console.WriteLine(media2.Group);
        Assert.That(media.Group == media2.Group);
    }
}