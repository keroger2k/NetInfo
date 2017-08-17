using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR167_Tests {

    [Test]
    public void should_return_true_for_device_with_correct_ntp_source_vlan() {
      AssetBlob blob = new AssetBlob {
        Body = @"exec-timeout 3 0
password 7 011F47401F5E3E282B1C743F2B071D08
transport input none
!
ntp clock-period 36029701
ntp source Vlan23
ntp server 10.32.9.254
ntp server 10.0.16.10
ntp server 10.16.27.68
end
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR167(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void should_return_true_for_device_with_incorrect_ntp_source_vlan() {
      AssetBlob blob = new AssetBlob {
        Body = @"exec-timeout 3 0
password 7 011F47401F5E3E282B1C743F2B071D08
transport input none
!
ntp clock-period 36029701
ntp source Vlan30
ntp server 10.32.9.254
ntp server 10.0.16.10
ntp server 10.16.27.68
end
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR167(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}