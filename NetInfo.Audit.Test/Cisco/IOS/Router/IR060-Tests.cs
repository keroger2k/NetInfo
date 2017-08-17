using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR060_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void ir060_should_return_true_for_router_with_correct_exec_timeout() {
      blob = new AssetBlob {
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
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR060(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void ir060_should_return_false_for_router_with_incorrect_exec_timeout() {
      blob = new AssetBlob {
        Body = @"
!
line con 0
 exec-timeout 3 0
 password 7 password
line vty 0 4
 access-class 98 in
 exec-timeout 2 0
 password 7 password
 transport input ssh
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR060(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void ir060_should_return_false_for_router_with_no_exec_timeout() {
      blob = new AssetBlob {
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
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR060(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}