using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR181_Tests {

    [Test]
    public void IR181_should_return_true_when_b1_acl_is_configured_on_navy_device() {
      var blob = new AssetBlob {
        Body = @"!
interface Tunnel975
 description <== Tunnel Source PRLH-B1-SDNI-PR ==>
 ip access-group b1acl-in-1033 in
 ip access-group b1acl-out-1033 out
!
ip classless
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR181(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR181_should_return_false_when_b1_acl_is_not_configured_on_navy_device() {
      var blob = new AssetBlob {
        Body = @"!
interface Tunnel975
 description <== Tunnel Source PRLH-B1-SDNI-PR ==>
 ip access-group b1acl-in-1033 in
!
ip classless
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR181(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR181_should_return_true_when_b1_acl_is_configured_on_usmc_device() {
      var blob = new AssetBlob {
        Body = @"!
interface TenGigabitEthernet3/1
 description <== NIPRNET CircuitID=78RP ==>
 ip address 214.40.2.238 255.255.255.252
 ip access-group initial_12_07_2011_NFC450126 out
 ip access-group initial_12_07_2011_NFC450126 in
!
ip classless
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR181(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR181_should_return_false_when_b1_acl_is_not_configured_on_usmc_device() {
      var blob = new AssetBlob {
        Body = @"!
interface TenGigabitEthernet3/1
 description <== NIPRNET CircuitID=78RP ==>
 ip address 214.40.2.238 255.255.255.252
 ip access-group initial_12_07_2011_NFC450126 in
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR181(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}