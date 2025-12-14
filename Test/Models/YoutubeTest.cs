using Spotabot.Models.Youtube;
using Spotabot.Services.Channel;

namespace Spotabot.Test.Models;

[TestFixture]
public class YoutubeTest
{
    [TestCase("https://www.youtube.com/watch?v=tbM7hdvMSPE&ab_channel=SpongeBobSquarePantsOfficial", ExpectedResult = "tbM7hdvMSPE")]
    [TestCase("https://www.youtube.com/watch?v=tbM7hdvMSPE", ExpectedResult = "tbM7hdvMSPE")]
    [TestCase("https://www.youtube.com/watch?v=tbM7hdvMSPE", ExpectedResult = "tbM7hdvMSPE")]
    [TestCase("https://youtu.be/-SNEwiG98SM?si=j_FVj5EYn9E5XUON", ExpectedResult = "-SNEwiG98SM")]
    [TestCase("https://www.youtube.com/shorts/tCDf9KyA65g", ExpectedResult = "tCDf9KyA65g")]
    [TestCase("tCDf9KyA65g", ExpectedResult = "tCDf9KyA65g")]
    public string GetVideoIdTest(string url)
    {
        return url.GetVideoId();
    }

    [TestCase("1qN72LEQnaU 5 30", ExpectedResult = "1qN72LEQnaU")]
    [TestCase("srbo1ipy6kc 0 140", ExpectedResult = "srbo1ipy6kc")]
    [TestCase("srbo1ipy6kc", ExpectedResult = "srbo1ipy6kc")]
    [TestCase(" ", ExpectedResult = YoutubeData.DefaultVideoId)]
    [TestCase("", ExpectedResult = YoutubeData.DefaultVideoId)]
    public string YoutubeDataTypeCreate(string url)
    {
        var data = YoutubeData.Create(url);
        return data.VideoId;
    }

    [TestCase("1qN72LEQnaU 5 465", ExpectedResult = 465)]
    [TestCase("srbo1ipy6kc 0 140", ExpectedResult = 140)]
    [TestCase("srbo1ipy6kc", ExpectedResult = 30)]
    public int ValidateDuration(string url)
    {
        var data = YoutubeData.Create(url);
        return data.Duration;
    }



    [TestCase("https://www.youtube.com/watch?v=1qN72LEQnaU", ExpectedResult = "https://www.youtube.com/embed/1qN72LEQnaU?autoplay=1"),
     TestCase("1qN72LEQnaU", ExpectedResult = "https://www.youtube.com/embed/1qN72LEQnaU?autoplay=1"),
    ]
    public string ToYoutubePass(string url)
    {
        var video = VideoRequestService.ToYoutubeUrl(url);
        return video;
    }

    [TestCase("https://www.youtube.com/wach?v=1qN72LEQnaU"),
     TestCase("https://www.sexy.com/wach?v=1qN72LEQnaU"),
     TestCase("/1qN72LEQnaU"),
    ]
    public void ToYoutubeFail(string url)
    {
        try
        {
            var video = VideoRequestService.ToYoutubeUrl(url);
            Assert.Fail(video);
        }
        catch (Exception ex)
        {

            Assert.Pass(ex.Message);
        }


    }

}