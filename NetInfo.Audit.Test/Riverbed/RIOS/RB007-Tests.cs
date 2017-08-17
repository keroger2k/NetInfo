using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB007_Tests {

    [Test]
    public void RB007_should_return_true_when_the_community_string_matches_the_approved_string_in_NMS_scripts() {
      var blob = new AssetBlob {
        Body = @"
    rbm role CMC_Group_PRLH primitive /role_primitive/cmc/config/group/PRLH
   snmp-server community ""eY129sYgPP0B""
   snmp-server host 10.32.9.224 traps version 1 eY129sYgPP0B
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB007(device, "eY129sYgPP0B");

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB007_should_return_false_when_the_community_string_does_not_match_the_approved_string_in_NMS_scripts() {
      var blob = new AssetBlob {
        Body = @"
    rbm role CMC_Group_PRLH primitive /role_primitive/cmc/config/group/PRLH
   snmp-server community ""fail""
   snmp-server host 10.32.9.224 traps version 1 eY129sYgPP0B
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB007(device, "eY129sYgPP0B");

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}