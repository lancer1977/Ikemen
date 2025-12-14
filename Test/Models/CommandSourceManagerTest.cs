using Spotabot.Interfaces;

namespace Spotabot.Test.Models;

[TestFixture]
public class CommandSourceManagerTest
{
    private readonly CommandSourceManager _commandSourceManager;

    public CommandSourceManagerTest()
    {
        _commandSourceManager = new CommandSourceManager();
    }

    [Test]
    public async Task GetDescriptionTest()
    {
        _commandSourceManager.Load();
        var play = _commandSourceManager.GetDescription("play");
        Assert.That(play.Name == "play");
    }
}