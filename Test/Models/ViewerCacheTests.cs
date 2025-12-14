using PolyhydraGames.Streaming.SQL.Models;
using Spotabot.Models;

namespace Spotabot.Test.Models
{
    [TestFixture]
    public class ViewerCacheTests
    {
        [Test]
        public void FirstTimeShouldShoutout()
        {
            var cache = new ViewerCache(new ChannelRecord());
            var shoutout = !cache.SkipShoutout();
            Assert.That(shoutout);
        }
        [Test]
        public void SecondTimeShouldNotShoutout()
        {
            var cache = new ViewerCache(new ChannelRecord());
            var shoutout = !cache.SkipShoutout();
            Assert.That(shoutout);
            shoutout = !cache.SkipShoutout();
            Assert.That(!shoutout);
        }
    }
}