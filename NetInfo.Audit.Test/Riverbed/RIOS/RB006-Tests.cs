using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB006_Tests {

    [Test]
    public void RB006_should_return_true_all_usernames_are_configured_with_encryption() {
      var blob = new AssetBlob {
        Body = @"
 no pm process xinetd launch enable

##
## Network management configuration
##
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""monitor"" password 7 $1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/
   banner login ""

You are accessing a U.S. Government (USG) Information System (IS) that is
""
   cli default auto-logout 3.0
no cmc appliance ""A42YQ0005B7EF"" auto-configuration enable
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB006(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB006_should_return_false_when_not_all_username_passwords_are_encrypted() {
      var blob = new AssetBlob {
        Body = @"
 no pm process xinetd launch enable

##
## Network management configuration
##
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""monitor"" password PLAIN_TEXT
   banner login ""

You are accessing a U.S. Government (USG) Information System (IS) that is
""
   cli default auto-logout 3.0
no cmc appliance ""A42YQ0005B7EF"" auto-configuration enable
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB006(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}