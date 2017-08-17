using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR042_Tests {

    [Test]
    public void IR042_should_return_true_when_snmp_server_group_has_allowed_access_groups_applied_67_68_69_() {
      var blob = new AssetBlob {
        Body = @"!
snmp-server group nmcigroup v3 auth write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF7F access 67
snmp-server group nmcigroup v3 auth write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF7F access 68
snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF0F access 69
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR042(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR042_should_return_false_when_snmp_server_group_has_incorrect_allowed_access_groups_applied_not_67_68_69_() {
      var blob = new AssetBlob {
        Body = @"!
snmp-server group nmcigroup v3 auth write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF7F access 99
snmp-server group nmcigroup v3 auth write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF7F access 68
snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF0F access 69
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR042(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR042_should_return_true_when_snmp_server_group_does_not_contain_access_group_info_instead_checks_show_snmp_user_and_access_list_is_allowed() {
      var blob = new AssetBlob {
        Body = @"!
snmp-server group nmcigroup v3 auth write NMS notify

QUAN-U07-AS-27#show snmp user

User name: nmsops
Engine ID: 00000063000100A28A9C7DE0
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmciuser
Engine ID: 00000063000100A28A9C7DE0
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: None
Group-name: nmcigroup

QUAN-U07-AS-27#!
"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR042(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR042_should_return_false_when_snmp_server_group_does_not_contain_access_group_info_instead_checks_show_snmp_user_and_access_list_is_not_allowed() {
      var blob = new AssetBlob {
        Body = @"!
snmp-server group nmcigroup v3 auth write NMS notify

QUAN-U07-AS-27#show snmp user

User name: nmsops
Engine ID: 00000063000100A28A9C7DE0
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmciuser
Engine ID: 00000063000100A28A9C7DE0
storage-type: nonvolatile	 active	access-list: 99
Authentication Protocol: MD5
Privacy Protocol: None
Group-name: nmcigroup

QUAN-U07-AS-27#!
"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR042(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR042_should_return_false_when_snmp_is_not_enabled() {
      var blob = new AssetBlob {
        Body = @"
PRLH-URB-OR-01#show snmp user
%SNMP agent not enabled
PRLH-URB-OR-01#!
"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR042(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}