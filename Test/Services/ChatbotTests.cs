using Spotabot.Services.Channel.Twitch.Chat;

namespace Spotabot.Test.Services
{
    [TestFixture]
    public class ChatbotTests
    {
        private const string BotNameTrue = "I am the spottie of bots!";
        private const string KeyphraseTrue = "Where is that silly alexa";
        private const string KeyphraseAndBotName = "Where is the spottie bot and his alter ego alexa";
        private const string NoMatch = "Where are all the hotties at";
        [TestCase("Spottie Bot","alexa", BotNameTrue, ExpectedResult = true)]
        [TestCase("Spottie ", "alexa", BotNameTrue, ExpectedResult = true)]
        [TestCase("Spottie Bot", "alexa", KeyphraseTrue, ExpectedResult = true)]
        [TestCase("Spottie Bot", "alexa", KeyphraseAndBotName, ExpectedResult = true)]
        [TestCase("Spottie Bot", "alexa", NoMatch, ExpectedResult = false)]
        public bool IsDirectedAtBot(string personaName, string keyword, string message)
        {
            var result = ChatMatch.IsNameInMessage(message, personaName, keyword);
            return result;
        }
    }
}