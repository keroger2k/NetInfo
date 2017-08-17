using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class SNMPSettingsTests {

    [Test]
    public void smtp_object_with_no_settings_should_return_zero_servers() {
      var settings = new SNMPSettings();

      Assert.IsEmpty(settings.Servers);
    }

    [Test]
    public void smtp_object_should_correcly_parse_server_groups() {
      var snmp = new SNMPSettings();
      snmp.Settings = new string[] {
        "snmp-server group nmcigroup v3 auth write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF7F access 99",
        "snmp-server group nmcigroup v3 priv write NMS notify *tv.FFFFFFFF.FFFFFFFF.FFFFFFFF.FFFFFFFF0F access 69"
      };

      Assert.AreEqual(2, snmp.Groups.Count());
      Assert.AreEqual(99, snmp.Groups.ElementAt(0).AccessGroup);
      Assert.AreEqual(69, snmp.Groups.ElementAt(1).AccessGroup);
    }

    [Test]
    public void smtp_object_should_correcly_parse_server_addresses() {
      var snmp = new SNMPSettings();
      snmp.Settings = new string[] {
        "snmp-server host 1.1.1.1 informs version 3 priv nmsops",
        "snmp-server host 2.2.2.2 informs version 3 priv nmsops"
      };

      Assert.AreEqual(2, snmp.Servers.Count());
      Assert.AreEqual("1.1.1.1", snmp.Servers.ElementAt(0).Address.ToString());
      Assert.AreEqual(3, snmp.Servers.ElementAt(0).Version);

      Assert.AreEqual("2.2.2.2", snmp.Servers.ElementAt(1).Address.ToString());
      Assert.AreEqual(3, snmp.Servers.ElementAt(1).Version);
    }

    [Test]
    public void smtp_object_should_return_null_if_location_string_is_not_found() {
      var snmp = new SNMPSettings();

      Assert.IsNull(snmp.Location);
    }

    [Test]
    public void smtp_object_should_correctly_parse_location_string() {
      var snmp = new SNMPSettings();
      snmp.Settings = new string[] {
        "snmp-server location \"Bldg_3255_Floor_1_Room_IDF_Rack_4_\""
      };

      Assert.AreEqual("\"Bldg_3255_Floor_1_Room_IDF_Rack_4_\"", snmp.Location);
    }

    [Test]
    public void smtp_object_should_correctly_parse_source_interface() {
      var snmp = new SNMPSettings();
      snmp.Settings = new string[] {
        "snmp-server source-interface informs Vlan30"
      };

      Assert.AreEqual("Vlan30", snmp.SourceInterface);
    }
  }
}