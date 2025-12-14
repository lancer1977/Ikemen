namespace Spotabot.Test.Models
{
    [TestFixture]
    public class ExtensionTests
    {
        [TestCase("", "DATA:")]
        public void Sloppy(string message, string split)
        {
            var result = message.SloppySplit(split);
            Assert.That(!result.Item1.Contains(split) && !result.Item2.Contains(split));

            Assert.That(result.Item1.Length > 1 && result.Item2.Length > 1); //messages.Length > 10);
        }
    }
}