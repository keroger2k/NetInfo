using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS043_Tests {

    [Test]
    public void BS043_should_return_true_when_the_correct_nms_test_script_version_is_applied() {
      var blob = new AssetBlob {
        Body = @"!
alias Brocade-AS-Stack_int=uban-access-FCX648-stack-initial-configuration-v1_0_2
alias nms=uNRFK-INSIDE-SNMPV3-Brocade-Switch-v4_1_0
alias harden=uNAVY-INSIDE-BROCADE-L2-v4_1_0
end"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS043(device, 4);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS043_should_return_false_when_the_incorrect_nms_test_script_version_is_applied() {
      var blob = new AssetBlob {
        Body = @"!
alias Brocade-AS-Stack_int=uban-access-FCX648-stack-initial-configuration-v1_0_2
alias nms=uNRFK-INSIDE-SNMPV3-Brocade-Switch-v4_1_0
alias harden=uNAVY-INSIDE-BROCADE-L2-v4_1_0
end"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS043(device, 1);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS043_should_return_false_no_alias_for_nms_is_found() {
      var blob = new AssetBlob {
        Body = @"!
end"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS043(device, 1);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}