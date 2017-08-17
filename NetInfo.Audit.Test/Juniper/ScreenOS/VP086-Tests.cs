using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP086_Tests {

    [Test]
    public void VP086_should_return_true_when_unlimited_number_of_users_is_defined_for_capacity() {
      var blob = new AssetBlob {
        Body = @"MCUSABQZVP00-> get license
Model:              Advanced
Sessions:           128064 sessions
Capacity:           unlimited number of users
NSRP:               ActiveActive
VPN tunnels:        1000 tunnels
Vsys:               None
Vrouters:           16 virtual routers
Zones:              60 zones
VLANs:              150 vlans
Drp:                Enable
Deep Inspection:    Enable
Deep Inspection Database Expire Date: Disable
Signature pack:     Signature update key is missing
IDP:                Disable
AV:                 Disable(0)
Anti-Spam:          Disable(0)
Url Filtering:      Disable

Update server url: nextwave.netscreen.com/key_retrieval
MCUSABQZVP00-> "
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP086(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP086_should_return_false_when_unlimited_number_of_users_is_not_defined_for_capacity() {
      var blob = new AssetBlob {
        Body = @"MCUSABQZVP00-> get license
Model:              Advanced
Sessions:           128064 sessions
Capacity:           limited number of users
NSRP:               ActiveActive
VPN tunnels:        1000 tunnels
Vsys:               None
Vrouters:           16 virtual routers
Zones:              60 zones
VLANs:              150 vlans
Drp:                Enable
Deep Inspection:    Enable
Deep Inspection Database Expire Date: Disable
Signature pack:     Signature update key is missing
IDP:                Disable
AV:                 Disable(0)
Anti-Spam:          Disable(0)
Url Filtering:      Disable

Update server url: nextwave.netscreen.com/key_retrieval
MCUSABQZVP00-> "
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP086(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP086_should_return_false_when_capcity_is_not_found() {
      var blob = new AssetBlob {
        Body = @"MCUSABQZVP00-> get license
Model:              Advanced
Sessions:           128064 sessions
NSRP:               ActiveActive
VPN tunnels:        1000 tunnels
Vsys:               None
Vrouters:           16 virtual routers
Zones:              60 zones
VLANs:              150 vlans
Drp:                Enable
Deep Inspection:    Enable
Deep Inspection Database Expire Date: Disable
Signature pack:     Signature update key is missing
IDP:                Disable
AV:                 Disable(0)
Anti-Spam:          Disable(0)
Url Filtering:      Disable

Update server url: nextwave.netscreen.com/key_retrieval
MCUSABQZVP00-> "
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP086(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP086_should_return_false_when_command_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP086(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}