using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR018_Tests {

    [Test]
    public void IR018_should_return_true_when_all_privacy_protocol_settings_are_DES_and_show_snmp_displays_privacy_protocol() {
      AssetBlob blob = new AssetBlob {
        Body = @"
ABCD-U00-IR-01#show snmp user

User name: nmsops
Engine ID: 00000063000100A20A00108A
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmsops
Engine ID: 00000063000100A20A101B3E
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_should_return_true_when_all_privacy_protocol_settings_are_DES_and_show_snmp_displays_privacy_protocol_and_username_and_group_are_incorrect() {
      AssetBlob blob = new AssetBlob {
        Body = @"
ABCD-U00-IR-01#show snmp user

User name: fail
Engine ID: 00000063000100A20A00108A
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: fail

User name: fail
Engine ID: 00000063000100A20A101B3E
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: fail

ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_should_return_true_when_all_privacy_protocol_settings_are_3DES_and_show_snmp_displays_privacy_protocol() {
      AssetBlob blob = new AssetBlob {
        Body = @"
ABCD-U00-IR-01#show snmp user

User name: nmsops
Engine ID: 00000063000100A20A00108A
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: 3DES
Group-name: nmcigroup

User name: nmsops
Engine ID: 00000063000100A20A101B3E
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: 3DES
Group-name: nmcigroup

ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_should_return_true_when_all_privacy_protocol_settings_are_AES_and_show_snmp_displays_privacy_protocol() {
      AssetBlob blob = new AssetBlob {
        Body = @"
ABCD-U00-IR-01#show snmp user

User name: nmsops
Engine ID: 00000063000100A20A00108A
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: AES
Group-name: nmcigroup

User name: nmsops
Engine ID: 00000063000100A20A101B3E
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: AES
Group-name: nmcigroup

ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_should_return_false_when_all_privacy_protocol_settings_are_not_all_DES_and_show_snmp_displays_privacy_protocol() {
      AssetBlob blob = new AssetBlob {
        Body = @"
ABCD-U00-IR-01#show snmp user

User name: nmsops
Engine ID: 00000063000100A20A00108A
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmsops
Engine ID: 00000063000100A20A101B3E
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: FAIL
Group-name: nmcigroup

ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR018_true_when_server_uses_inform_instead_of_informs() {
      AssetBlob blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.0000000F access 69
snmp-server host 10.0.16.136 inform version 3 priv nmsops
snmp-server host 10.32.9.224 inform version 3 priv nmsops
snmp-server host 10.32.9.226 inform version 3 priv nmsops
snmp-server host 10.32.9.244 inform version 3 priv nmsops

PRLH-U00-BI-01#show snmp user
User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 8000000903000008E21893C0
storage-type: nonvolatile	 active	access-list: 69

PRLH-U00-BI-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_true_when_server_uses_inform_instead_of_informs_and_username_and_group_are_incorrect() {
      AssetBlob blob = new AssetBlob {
        Body = @"
snmp-server group fail v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.0000000F access 69
snmp-server host 10.0.16.136 inform version 3 priv fail
snmp-server host 10.32.9.224 inform version 3 priv nmsops
snmp-server host 10.32.9.226 inform version 3 priv nmsops
snmp-server host 10.32.9.244 inform version 3 priv nmsops

PRLH-U00-BI-01#show snmp user
User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69

User name: fail
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 8000000903000008E21893C0
storage-type: nonvolatile	 active	access-list: 69

PRLH-U00-BI-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_should_return_true_when_privacy_protocol_is_not_found_in_show_command_and_priv_is_declared_on_group_and_all_hosts() {
      AssetBlob blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv write NMS notify
snmp-server host 10.0.16.134 informs version 3 priv nmsops
snmp-server host 10.0.16.138 informs version 3 priv nmsops
snmp-server host 10.16.27.33 informs version 3 priv nmsops
snmp-server host 10.16.27.62 informs version 3 priv nmsops
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_should_return_true_when_privacy_protocol_is_not_found_in_show_command_and_priv_is_declared_on_group_and_all_hosts_and_groupname_and_username_are_incorrect() {
      AssetBlob blob = new AssetBlob {
        Body = @"
snmp-server group fail v3 priv write NMS notify
snmp-server host 10.0.16.134 informs version 3 priv fail
snmp-server host 10.0.16.138 informs version 3 priv nmsops
snmp-server host 10.16.27.33 informs version 3 priv nmsops
snmp-server host 10.16.27.62 informs version 3 priv nmsops
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR018_should_return_false_when_privacy_protocol_is_not_found_in_show_command_and_priv_is_not_declared_on_group_and_all_hosts_have_priv_declared() {
      AssetBlob blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 auth write NMS notify
snmp-server host 10.0.16.134 informs version 3 priv nmsops
snmp-server host 10.0.16.138 informs version 3 priv nmsops
snmp-server host 10.16.27.33 informs version 3 priv nmsops
snmp-server host 10.16.27.62 informs version 3 priv nmsops
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR018_should_return_false_when_privacy_protocol_is_not_found_in_show_command_and_priv_is_declared_on_group_and_all_hosts_do_not_have_priv_declared() {
      AssetBlob blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 priv write NMS notify
snmp-server host 10.0.16.134 informs version 3 auth nmsops
snmp-server host 10.0.16.138 informs version 3 priv nmsops
snmp-server host 10.16.27.33 informs version 3 priv nmsops
snmp-server host 10.16.27.62 informs version 3 priv nmsops
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR018_should_return_false_when_privacy_protocol_is_not_found_in_show_command_and_priv_is_not_declared_on_group_or_host_settings() {
      AssetBlob blob = new AssetBlob {
        Body = @"
snmp-server group nmcigroup v3 auth write NMS notify
snmp-server host 10.0.16.134 informs version 3 auth nmsops
snmp-server host 10.0.16.138 informs version 3 auth nmsops
snmp-server host 10.16.27.33 informs version 3 auth nmsops
snmp-server host 10.16.27.62 informs version 3 auth nmsops
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR018_should_return_false_when_snmp_is_not_enabled() {
      AssetBlob blob = new AssetBlob {
        Body = @"
 PRLH-URB-OR-01#show snmp user
%SNMP agent not enabled
PRLH-URB-OR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR018(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}