using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB012_Tests {

    [Test]
    public void RB012_should_return_true_when_the_usernames_match_the_hardening_script() {
      var blob = new AssetBlob {
        Body = @"
    ##
## Network management configuration
##
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""monitor"" password 7 $1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB012(device, new[] { "$1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/", "$1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB012_should_return_false_when_it_is_missing_a_username() {
      var blob = new AssetBlob {
        Body = @"
    ##
## Network management configuration
##
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB012(device, new[] { "$1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/", "$1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB012_should_return_false_when_it_is_has_an_extra_username() {
      var blob = new AssetBlob {
        Body = @"
    ##
## Network management configuration
##
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""monitor"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""kyle"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB012(device, new[] { "$1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/", "$1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB012_should_return_true_when_default_usernames_show_up() {
      var blob = new AssetBlob {
        Body = @"
   no telnet-server enable
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""administrator"" password 7 *
   username ""monitor"" password 7 $1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/
   username ""rcud"" password 7 *
   username ""root"" password 7 *
   username ""vserveruser"" password 7 *
   web auto-logout ""3""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB012(device, new[] { "$1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/", "$1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/", "*" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB012_should_return_false_when_default_username_show_up_and_a_non_approved_user() {
      var blob = new AssetBlob {
        Body = @"
   no telnet-server enable
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""administrator"" password 7 *
   username ""monitor"" password 7 $1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/
   username ""kyle"" password 7 $1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/
   username ""rcud"" password 7 *
   username ""root"" password 7 *
   username ""vserveruser"" password 7 *
   web auto-logout ""3""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB012(device, new[] { "$1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/", "$1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}