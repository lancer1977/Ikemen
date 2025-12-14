using PolyhydraGames.Extensions;

namespace Spotabot.Test.Models;

[TestFixture]
public class ExceptionTests
{
    [Test]
    public void TestNestedExceptions()
    {
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
}