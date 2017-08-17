using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS033_Tests {

    [Test]
    public void BS033_should_return_true_when_write_memory_is_confirmed_in_test_script() {
      var blob = new AssetBlob {
        Body = @"
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#write memory
..Write startup-config done.
SSH@ALTN-U01-AS-12#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS033(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS033_should_return_true_when_write_memory_is_found_and_next_line_is_blank_in_test_script() {
      var blob = new AssetBlob {
        Body = @"
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#write memory
SSH@ALTN-U01-AS-12#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS033(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS033_should_return_false_when_write_memory_is_not_found() {
      var blob = new AssetBlob {
        Body = @"
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS033(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS033_should_return_false_when_write_memory_does_not_succeed_because_in_incorrect_user_mode() {
      var blob = new AssetBlob {
        Body = @"SSH@PTNH-U01-AS-07>
SSH@PTNH-U01-AS-07>write memory
Invalid input -> write memory
Type ? for a list
SSH@PTNH-U01-AS-07>"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS033(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}