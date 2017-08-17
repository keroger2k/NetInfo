using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB005_Tests {

    [Test]
    public void RB005_should_return_true_when_two_or_more_tacacs_servers_are_found() {
      var blob = new AssetBlob {
        Body = @"
 tacacs-server host 10.32.9.233
 tacacs-server host 10.0.16.152
 tacacs-server host 10.16.27.44
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB005(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB005_should_return_true_when_two_or_more_tacacs_servers_are_found_with_this_syntax() {
      var blob = new AssetBlob {
        Body = @"
 tacacs-server host 10.32.9.233 timeout 20 retransmit 1
 tacacs-server host 10.32.9.233 key 7 ""k2ZwBUYw/xQ7CYB7vNozoVWpZsxtddKD""
 tacacs-server host 10.16.27.44 timeout 20 retransmit 1
 tacacs-server host 10.16.27.44 key 7 ""QgAxGn/EF/Ra+hlRvL7B1g/gZBJwr06m""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB005(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB005_should_return_false_when_less_than_two_tacacs_servers_are_found() {
      var blob = new AssetBlob {
        Body = @"
 tacacs-server host 10.32.9.233
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB005(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB005_should_return_false_when_no_tacacs_servers_settings_are_found() {
      var blob = new AssetBlob {
        Body = @"
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB005(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}