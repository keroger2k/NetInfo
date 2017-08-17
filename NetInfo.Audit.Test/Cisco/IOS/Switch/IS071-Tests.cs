using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS071_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void is071_should_return_true_when_exec_timeout_3_0_is_found_on_console_port() {
      blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS071(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void is071_should_return_false_when_exec_timeout_3_0_is_not_found_on_console_port() {
      blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS071(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}