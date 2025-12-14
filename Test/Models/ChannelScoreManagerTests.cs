using Microsoft.Extensions.Logging;
using PolyhydraGames.Streaming.Interfaces;
using Spotabot.Interfaces;
using Spotabot.Services.Channel;

namespace Spotabot.Test.Models
{
    [TestFixture]
    public class ChannelScoreManagerTests
    {
        private ChannelScoreManager Manager { get; set; }
        public ChannelScoreManagerTests()
        {

            IChipManager moqService;
            IUserService moqSite;
            ILogger<ChannelSettingsManager> moqLog;
            moqService = (new Moq.Mock<IChipManager>()).Object;
            moqSite = (new Moq.Mock<IUserService>()).Object;
            moqLog = (new Moq.Mock<ILogger<ChannelSettingsManager>>()).Object;
            Manager = new ChannelScoreManager(moqSite, moqService); 
        }

        [Test]
        public async Task AddScoreTest()
        { 
            Manager.Subscriber = "Daniel";
            Assert.That(Manager.Dirty == true);
            //Assert.That(Manager.GetScore(user) == 10, "Score should be 10 after adding 10 points.");
        }
    }
}