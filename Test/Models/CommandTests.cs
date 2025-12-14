using OAuth.Core;
using OAuth.Core.Bot;
using OAuth.Core.Models;
using Spotabot.Commands;
using Spotabot.Interfaces;

namespace Spotabot.Test.Models;
using System;
using NUnit.Framework;

[TestFixture]
public class CommandTests
{
    private CommandSourceManager Manager { get; } = new();

    public CommandTests()
    {
        Manager.Load();
    }

    public List<CommandDescription> Descriptions(CommandPermission role)
    {
        return Manager.CommandDescriptions.Values.Where(x => x.Permission == role).ToList();
    }

    public void TestCommandPermission(ChannelRole role, CommandPermission permission, bool shouldBeAbleToExecute)
    {
        var everyone = Descriptions(permission);
        foreach (var command in everyone)
        {
            var message = shouldBeAbleToExecute
                ? $"A command of {command.Name} should be executable by {role} "
                : $"A command of {command.Name} should {(shouldBeAbleToExecute ? "" : "NOT")} be executable by {role} ";
           // Assert.That(command.CanExecute(role) == shouldBeAbleToExecute, message);
        }
    }

    [Test]
    public void NoValue()
    {
        TestCommandPermission(ChannelRole.None, CommandPermission.Everyone, true);
        TestCommandPermission(ChannelRole.None, CommandPermission.Follower, false);
        TestCommandPermission(ChannelRole.None, CommandPermission.Vip, false);
        TestCommandPermission(ChannelRole.None, CommandPermission.Subscriber, false);
        TestCommandPermission(ChannelRole.None, CommandPermission.Admin, false);
        TestCommandPermission(ChannelRole.None, CommandPermission.Streamer, false);
    }

    [Test]
    public void PlebBehaviors()
    {
        TestCommandPermission(ChannelRole.Everyone, CommandPermission.Everyone, true);
        TestCommandPermission(ChannelRole.Everyone, CommandPermission.Follower, false);
        TestCommandPermission(ChannelRole.Everyone, CommandPermission.Vip, false);
        TestCommandPermission(ChannelRole.Everyone, CommandPermission.Subscriber, false);
        TestCommandPermission(ChannelRole.Everyone, CommandPermission.Admin, false);
        TestCommandPermission(ChannelRole.Everyone, CommandPermission.Streamer, false);
    }

    [Test]
    public void AdminBehaviors()
    {
        var role = ChannelRole.Admin;
        TestCommandPermission(role, CommandPermission.Everyone, true);
        TestCommandPermission(role, CommandPermission.Follower, true);
        TestCommandPermission(role, CommandPermission.Vip, true);
        TestCommandPermission(role, CommandPermission.Subscriber, true);
        TestCommandPermission(role, CommandPermission.Admin, true);
        TestCommandPermission(role, CommandPermission.Streamer, false);
    }

    [Test]
    public void StreamerBehaviors()
    {
        var role = ChannelRole.Streamer;
        TestCommandPermission(role, CommandPermission.Everyone, true);
        TestCommandPermission(role, CommandPermission.Follower, true);
        TestCommandPermission(role, CommandPermission.Vip, true);
        TestCommandPermission(role, CommandPermission.Subscriber, true);
        TestCommandPermission(role, CommandPermission.Admin, true);
        TestCommandPermission(role, CommandPermission.Streamer, true);
    }

    [Test]
    public void VipBehaviors()
    {
        var role = ChannelRole.Vip;
        TestCommandPermission(role, CommandPermission.Everyone, true);
        TestCommandPermission(role, CommandPermission.Follower, true);
        TestCommandPermission(role, CommandPermission.Vip, true);
        TestCommandPermission(role, CommandPermission.Subscriber, false);
        TestCommandPermission(role, CommandPermission.Admin, false);
        TestCommandPermission(role, CommandPermission.Streamer, false);
    }

    [Test]
    public void FollowerBehaviors()
    {
        var role = ChannelRole.Follower;
        TestCommandPermission(role, CommandPermission.Everyone, true);
        TestCommandPermission(role, CommandPermission.Follower, true);
        TestCommandPermission(role, CommandPermission.Vip, false);
        TestCommandPermission(role, CommandPermission.Subscriber, false);
        TestCommandPermission(role, CommandPermission.Admin, false);
        TestCommandPermission(role, CommandPermission.Streamer, false);
    }

    [TestCase("I am not a command", ExpectedResult = false)]
    [TestCase("I am not a command!", ExpectedResult = false)]
    [TestCase("!eat my juicy ass", ExpectedResult = true)]
    [TestCase("!eat", ExpectedResult = true)]
    [TestCase(" !eat my juicy ass", ExpectedResult = true)]
    [TestCase("", ExpectedResult = false)]
    [TestCase("               !eat my juicy ass", ExpectedResult = true)]
    public bool IsCommand(string message)
    {
        var command = CommandRequest.Create("test", message, "1", "test", ChannelRole.None, "test", Platform.Server);
        return command.IsCommand;
    }

    [TestCase("!eat", ExpectedResult = "eat")]
    [TestCase("!eAt my juicy ass", ExpectedResult = "eat")]
    [TestCase(" !eaT my juicy ass", ExpectedResult = "eat")]
    [TestCase("               !Eat my juicy ass", ExpectedResult = "eat")]
    public string GetCommand(string message)
    {
        var command = CommandRequest.Create("test", message, "1", "test", ChannelRole.None, "test", Platform.Server);
        return command.Command;
    }

    [TestCase("!eat", ExpectedResult = "")]
    [TestCase("!eat my juicy ass", ExpectedResult = "my juicy ass")]
    [TestCase(" !eat my juicy ass", ExpectedResult = "my juicy ass")]
    [TestCase("               !eat my juicy ass", ExpectedResult = "my juicy ass")]
    public string GetArgs(string message)
    {
        var command = CommandRequest.Create("test", message, "1", "test", ChannelRole.None, "test", Platform.Server);
        return command.Args;
    }
}