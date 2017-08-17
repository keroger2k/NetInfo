using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP000_Tests {

    [Test]
    public void VP000_should_return_true_when_test_script_is_applied_correctly() {
      var blob = new AssetBlob {
        Body = @"HA:AHDSEDSTFW51z(M)-> set console page 0
HA:AHDSEDSTFW51z(M)-> get system
HA:AHDSEDSTFW51z(M)-> get chassis
HA:AHDSEDSTFW51z(M)-> get license
HA:AHDSEDSTFW51z(M)-> get nsrp
HA:AHDSEDSTFW51z(M)-> get pki x509 list cert
HA:AHDSEDSTFW51z(M)-> get route
HA:AHDSEDSTFW51z(M)-> get config
set hostname AHDSEDSTFW51z
HA:AHDSEDSTFW51z(M)-> set console page 20
HA:AHDSEDSTFW51z(M)-> set console timeout 3
HA:AHDSEDSTFW51z(M)-> save

END-OF-TEST-SCRIPT"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP000_should_return_false_when_test_script_is_applied_incorrectly() {
      var blob = new AssetBlob {
        Body = @"HA:AHDSEDSTFW51z(M)-> set console page 1
HA:AHDSEDSTFW51z(M)-> get system
HA:AHDSEDSTFW51z(M)-> get chassis
HA:AHDSEDSTFW51z(M)-> get license
HA:AHDSEDSTFW51z(M)-> get pki x509 list cert
HA:AHDSEDSTFW51z(M)-> get route
HA:AHDSEDSTFW51z(M)-> get config
set hostname AHDSEDSTFW51z
HA:AHDSEDSTFW51z(M)-> set console page 20
HA:AHDSEDSTFW51z(M)-> set console timeout 3
HA:AHDSEDSTFW51z(M)-> save

END-OF-TEST-SCRIPT"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP000(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP000_should_ignore_commands_that_are_in_configuration_and_not_from_test_script() {
      var blob = new AssetBlob {
        Body = @"HA:AHDSEDSTFW51z(M)-> set console page 0
HA:AHDSEDSTFW51z(M)-> get system
HA:AHDSEDSTFW51z(M)-> get chassis
HA:AHDSEDSTFW51z(M)-> get license
HA:AHDSEDSTFW51z(M)-> get nsrp
HA:AHDSEDSTFW51z(M)-> get pki x509 list cert
HA:AHDSEDSTFW51z(M)-> get route
HA:AHDSEDSTFW51z(M)-> get config
set hostname AHDSEDSTFW51z
set console timeout 3
set console page 0
set domain navy.mil
set hostname AHDSEDSTFW11
HA:AHDSEDSTFW51z(M)-> set console page 20
HA:AHDSEDSTFW51z(M)-> set console timeout 3
HA:AHDSEDSTFW51z(M)-> save

END-OF-TEST-SCRIPT"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP000_should_return_true_when_set_console_timeout_3_is_missing_for_USMC_device_in_testscript() {
      var blob = new AssetBlob {
        Body = @"HA:MCUSDAYTVP00(M)-> set console page 0
HA:MCUSDAYTVP00(M)-> get system
HA:MCUSDAYTVP00(M)-> get chassis
HA:MCUSDAYTVP00(M)-> get license
HA:MCUSDAYTVP00(M)-> get nsrp
HA:MCUSDAYTVP00(M)-> get pki x509 list cert
HA:MCUSDAYTVP00(M)-> get route
HA:MCUSDAYTVP00(M)-> get config
set hostname MCUSDAYTVP00
set console timeout 3
set console page 0
set domain navy.mil
set hostname AHDSEDSTFW11
HA:MCUSDAYTVP00(M)-> set console page 20
HA:MCUSDAYTVP00(M)-> save

END-OF-TEST-SCRIPT"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP000_should_return_true_when_set_console_timeout_3_is_not_missing_for_USMC_device_in_testscript() {
      var blob = new AssetBlob {
        Body = @"HA:MCUSDAYTVP00(M)-> set console page 0
HA:MCUSDAYTVP00(M)-> get system
HA:MCUSDAYTVP00(M)-> get chassis
HA:MCUSDAYTVP00(M)-> get license
HA:MCUSDAYTVP00(M)-> get nsrp
HA:MCUSDAYTVP00(M)-> get pki x509 list cert
HA:MCUSDAYTVP00(M)-> get route
HA:MCUSDAYTVP00(M)-> get config
set hostname MCUSDAYTVP00
set console timeout 3
set console page 0
set domain navy.mil
set hostname AHDSEDSTFW11
HA:MCUSDAYTVP00(M)-> set console page 20
HA:MCUSDAYTVP00(M)-> set console timeout 3
HA:MCUSDAYTVP00(M)-> save

END-OF-TEST-SCRIPT"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP000_can_handle_scripts_with_collected_manually() {
      var blob = new AssetBlob {
        Body = @"NAEACHRLVP00(M)-> set console page 0
NAEACHRLVP00(M)-> get system
NAEACHRLVP00(M)-> get chassis
NAEACHRLVP00(M)-> get license
NAEACHRLVP00(M)-> get nsrp
NAEACHRLVP00(M)-> get pki x509 list cert
NAEACHRLVP00(M)-> get route
NAEACHRLVP00(M)-> get config
set hostname NAEACHRLVP00
NAEACHRLVP00(M)-> set console page 20
NAEACHRLVP00(M)-> set console timeout 3
NAEACHRLVP00(M)-> save
NAEACHRLVP00(M)->
NAEACHRLVP00(M)-> END-OF-TEST-SCRIPT
                  ^--------------------unknown keyword END-OF-TEST-SCRIPT
NAEACHRLVP00(M)->
NAEACHRLVP00(M)->
NAEACHRLVP00(M)->
NAEACHRLVP00(M)-> "
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP000(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}