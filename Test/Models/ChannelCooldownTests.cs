using Spotabot.Models;

namespace Spotabot.Test.Models
{
    [TestFixture]
    public class ChannelCooldownTests
    {
        [Test]
        public void CanUse_FirstCall_ReturnsTrue()
        {
            var cooldown = new ChannelCooldown(10, 5);
            cooldown.VipReduction = false;

            var result = cooldown.CanUse(isVip: false);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanUse_SecondCallImmediately_ReturnsFalse()
        {
            var cooldown = new ChannelCooldown(10, 5);
            cooldown.VipReduction = false;
            cooldown.CanUse(false); // consume it

            var result = cooldown.CanUse(false);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanUse_AfterCooldownExpires_ReturnsTrue()
        {
            var cooldown = new ChannelCooldown(1, 1);
            cooldown.VipReduction = false;
            cooldown.CanUse(false); // consume it
            Thread.Sleep(1100); // wait for cooldown

            var result = cooldown.CanUse(false);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanUse_VIPBeforeVipCooldownExpires_ReturnsFalse()
        {
            var cooldown = new ChannelCooldown(10, 5);
            cooldown.VipReduction = true;
            cooldown.CanUse(true); // consume it

            Thread.Sleep(1000); // too soon

            var result = cooldown.CanUse(true);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanUse_VIPAfterVipCooldownExpires_ReturnsTrue()
        {
            var cooldown = new ChannelCooldown(10, 1);
            cooldown.VipReduction = true;
            cooldown.CanUse(true); // consume it

            Thread.Sleep(1100); // wait past VIP cooldown

            var result = cooldown.CanUse(true);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanUse_VIPReductionDisabled_UsesNormalCooldown()
        {
            var cooldown = new ChannelCooldown(2, 0); // VIP time doesn't matter
            cooldown.VipReduction = false;
            cooldown.CanUse(true); // consume it

            Thread.Sleep(1000); // not enough for normal cooldown

            var result = cooldown.CanUse(true);

            Assert.That(result, Is.False);
        }
    }
}