using GamePlayer.Core.Models;
using PolyhydraGames.Core.Interfaces;
using Spotabot.Models;
using Spotabot.Services.Channel;
using Spotabot.Services.Frotz;

namespace Spotabot.Test.Models
{
    [TestFixture]
    public class FrotzGameTest
    {
        [Test()]
        public void TestNestedExceptions()
        {
            var db = new FrotzDatabase(new FrotzOptions());
            var items = db.Games;
            var first = items.First();
            Assert.That(!string.IsNullOrEmpty(first.Description));
            Assert.That(!string.IsNullOrEmpty(first.FullName));
            Assert.That(!string.IsNullOrEmpty(first.FileName));
            Assert.That(first.Year > 0);

        }
    }

    [TestFixture]
    public class GameTest
    {
        //[TestCase("{platform} : {title}", "Genesis","Sonic the Hedgehog", ExpectedResult = "Genesis : Sonic the Hedgehog")]
        //public string TestNestedExceptions(string pattern, string platform, string title)
        //{
        //    var game = new Game( )
        //    {
        //        Title =title,
        //        Platform = platform
        //    };
        //    return GameExtensions.GetTwitchGameTitle();  
             
        //}
        [TestCase("{platform} : {title}", "Genesis", "Sonic the Hedgehog", 1996, ExpectedResult = "Genesis : Sonic the Hedgehog")]
        [TestCase("{platform} : {title}", "Genesis", "Sonic the Hedgehog", 1996, ExpectedResult = "Genesis : Sonic the Hedgehog")]
        [TestCase("{platform} : {title} ({year})", "Genesis", "Sonic the Hedgehog", 1996, ExpectedResult = "Genesis : Sonic the Hedgehog (1996)")]
        public string TestNestedExceptions(string pattern, string platform, string title, int year)
        { 
            var update = new TitleGameUpdate(pattern)
            {
                Title = title,
                Platform = platform,
                Year = year.ToString()
            };
            
            return GameExtensions.GetTwitchGameTitle(update);

        }
    }
}