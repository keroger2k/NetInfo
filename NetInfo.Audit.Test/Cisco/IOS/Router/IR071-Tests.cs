using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router
{

    [TestFixture]
    public class IR071_Tests
    {
        private AssetBlob blob;

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void ir071_should_return_true_when_exec_timeout_X_X_is_less_than_ten_minutes_on_console_port()
        {
            blob = new AssetBlob
            {
                Body = @"!
line con 0
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input none
!"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET1624(device);

            var result = item.Compliant();

            Assert.True(result);
        }

        [Test]
        public void ir071_should_return_false_when_exec_timeout_X_X_is_zero_on_console_port()
        {
            blob = new AssetBlob
            {
                Body = @"!
line con 0
 exec-timeout 0 0
 password 7 011F47401F5E3E282B1C743F2B071D08
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input none
!"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET1624(device);

            var result = item.Compliant();

            Assert.False(result);
        }

        [Test]
        public void ir071_should_return_false_when_exec_timeout_X_X_is_greater_than_ten_minutes_on_console_port()
        {
            blob = new AssetBlob
            {
                Body = @"!
line con 0
 exec-timeout 11 0
 password 7 011F47401F5E3E282B1C743F2B071D08
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input none
!"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET1624(device);

            var result = item.Compliant();

            Assert.False(result);
        }
    }
}