using Microsoft.AspNetCore.Components;

namespace Spotabot.Test.Services;

[TestFixture]
public class NavigationExtensionsTests
{
    private class MockNavigationManager : NavigationManager
    {
        public MockNavigationManager(string baseUri)
        {
            Initialize(baseUri, baseUri);
        }
    }

    [TestCase("https://dev.channelcheevos.com/", ExpectedResult = "dev.channelcheevos.com")]
    [TestCase("http://localhost:5000/", ExpectedResult = "localhost")]
    [TestCase("https://example.com/path/to/page", ExpectedResult = "example.com")]
    [TestCase("http://sub.domain.example.com/", ExpectedResult = "sub.domain.example.com")]
    [TestCase("https://channelcheevos.com:8080/", ExpectedResult = "channelcheevos.com")]
    public string GetRawUrl_ReturnsCorrectHost(string baseUri)
    {
        // Arrange
        var navigationManager = new MockNavigationManager(baseUri);

        // Act
        var result = navigationManager.GetRawUrl();

        // Assert
        return result;
    }


    [TestCase("https://dev.channelcheevos.com/queue/abc-123", ExpectedResult = "abc-123")]
    [TestCase("http://localhost:5000/go/abc-123", ExpectedResult = "abc-123")]
    [TestCase("https://example.com/path/to/pagego/abc-123", ExpectedResult = "abc-123")]
    [TestCase("http://sub.domain.example.com/go/abc-123", ExpectedResult = "abc-123")]
    [TestCase("https://channelcheevos.com:8080/go/abc-123", ExpectedResult = "abc-123")]
    
    public string? GetChannelCode(string baseUri)
    {
        // Arrange
        var navigationManager = new MockNavigationManager(baseUri);

        // Act
        var result = navigationManager.GetChannelCode();

        // Assert
        return result;
    }

    [TestCase("https://debug.polyhydragames.com/admin?code=bbhfpkfox1g4lbu1vldxfkqqoi03zj&scope=whispers%3Aread+whispers%3Aedit+user%3Aread%3Afollows+moderator%3Aread%3Afollowers+channel%3Amanage%3Abroadcast+moderator%3Amanage%3Abanned_users+bits%3Aread+user%3Amanage%3Awhispers+channel%3Amanage%3Aredemptions+channel_check_subscription+channel_commercial+channel_editor+channel_feed_edit+channel_feed_read+channel_read+channel_stream+channel_subscriptions+chat%3Aread+chat%3Aedit+collections_edit+communities_edit+communities_moderate+user_blocks_edit+user_blocks_read+user_follows_edit+user_read+user_subscriptions+viewing_activity_read+openid+analytics%3Aread%3Aextensions+analytics%3Aread%3Agames+channel%3Aedit%3Acommercial+channel%3Amanage%3Aextensions+channel%3Amanage%3Amoderators+channel%3Amanage%3Apolls+channel%3Amanage%3Apredictions+channel%3Amanage%3Aschedule+channel%3Amanage%3Avideos+channel%3Amanage%3Avips+channel%3Aread%3Acharity+channel%3Aread%3Aeditors+channel%3Aread%3Agoals+channel%3Aread%3Ahype_train+channel%3Aread%3Apolls+channel%3Aread%3Apredictions+channel%3Aread%3Aredemptions+channel%3Aread%3Astream_key+channel%3Aread%3Asubscriptions+channel%3Aread%3Avips+clips%3Aedit+moderation%3Aread+moderator%3Amanage%3Ablocked_terms+moderator%3Amanage%3Aannouncements+moderator%3Amanage%3Aautomod+moderator%3Amanage%3Aautomod_settings+moderator%3Amanage%3Achat_messages+moderator%3Amanage%3Achat_settings+moderator%3Aread%3Ablocked_terms+moderator%3Aread%3Aautomod_settings+moderator%3Aread%3Achat_settings+moderator%3Aread%3Achatters+user%3Aedit+user%3Aedit%3Abroadcast+user%3Aedit%3Afollows+user%3Amanage%3Ablocked_users+user%3Amanage%3Achat_color+user%3Aread%3Ablocked_users+user%3Aread%3Abroadcast+user%3Aread%3Aemail+user%3Aread%3Asubscriptions")]
    public void GetChannelCodeIsNull(string baseUri)
    {
        // Arrange
        var navigationManager = new MockNavigationManager(baseUri);

        // Act
        var result = navigationManager.GetChannelCode();

        // Assert
        Assert.That(result == null);
    }

}