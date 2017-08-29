using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router
{

    [TestFixture]
    public class IR060_Tests
    {
        private AssetBlob blob;

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void ir060_should_return_true_for_router_with_correct_exec_timeout()
        {
            blob = new AssetBlob
            {
                Body = @"
!
line con 0
 exec-timeout 3 0
 password 7 password
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 password 7 password
 transport input ssh
!"
            };
            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET1639(device);

            var result = item.Compliant();

            Assert.True(result);
        }

        [Test]
        public void ir060_should_return_false_for_router_with_no_exec_timeout()
        {
            blob = new AssetBlob
            {
                Body = @"
!
line con 0
 exec-timeout 3 0
 password 7 password
line vty 0 4
 access-class 98 in
 password 7 password
 transport input ssh
!"
            };
            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET1639(device);

            var result = item.Compliant();

            Assert.False(result);
        }

        [Test]
        public void ir060_should_return_false_for_router_with_zero_set_for_exec_timeout()
        {
            blob = new AssetBlob
            {
                Body = @"
!
line con 0
 exec-timeout 3 0
 password 7 password
line vty 0 4
 access-class 98 in
 exec-timeout 0 0
 password 7 password
 transport input ssh
!"
            };
            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET1639(device);

            var result = item.Compliant();

            Assert.False(result);
        }

        [Test]
        public void ir060_should_return_false_for_router_with_exec_timeout_greater_than_ten_minutes()
        {
            blob = new AssetBlob
            {
                Body = @"
!
line con 0
 exec-timeout 3 0
 password 7 password
line vty 0 4
 access-class 98 in
 exec-timeout 0 0
 password 7 password
 transport input ssh
!"
            };
            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET1639(device);

            var result = item.Compliant();

            Assert.False(result);
        }
    }
}