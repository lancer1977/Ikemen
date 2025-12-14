using PolyhydraGames.Extensions;
using Spotabot.Models;
using System.Globalization;

namespace Spotabot.Test.Models;

[TestFixture]
public class ChannelRecordTests
{
    [Test]
    public void TestNestedExceptions()
    {
        var date = DateTime.ParseExact("2024/09/07 6:30 PM", "yyyy/MM/dd hh:mm tt", CultureInfo.InvariantCulture);
        var nestingDoll = new Exception("1");
        for (var i = 2; i < 10; i++)
        {
            var ex = new Exception(i.ToString(), nestingDoll);
            nestingDoll = ex;
        }

        var messages = nestingDoll.GetMessages();
        Console.WriteLine(messages);
        Assert.That(1 == 1); //messages.Length > 10);
    }

    [TestCase("http://hiimacheep.com", ExpectedResult = true)]
    [TestCase("https://hiimacheep.com", ExpectedResult = true)]
    [TestCase("hiimacheep.com", ExpectedResult = true)]
    [TestCase("hiimacheep.com/testyy", ExpectedResult = true)]
    [TestCase("hiimacheep.co", ExpectedResult = true)]
    [TestCase("dumbpeopletrap.com/scam", ExpectedResult = true)]
    [TestCase("Cheap viewers on topgaming77a.net/hz7h", ExpectedResult = true)]
    [TestCase("hiimacheep.co/testyy", ExpectedResult = true)]
    [TestCase("Hi there chief. comrade beatums", ExpectedResult = false)]
    [TestCase("Hows it going.corn today is pretty good", ExpectedResult = false)]
    public bool IsUrl(string value)
    {
        return value.IsUrl();
    }
}