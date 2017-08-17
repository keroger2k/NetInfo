using System.Linq;
using NetInfo.Devices.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS.Classes {

  [TestFixture]
  public class SNMPSettingsTests {

    [Test]
    public void snmp_object_with_no_snmp_should_return_zero_servers() {
      var snmp = new SNMPSettings();
      snmp.Settings = new string[] { };

      Assert.IsEmpty(snmp.Servers);
    }

    [Test]
    public void snmp_object_should_correclyt_parse_server_addresses() {
      var snmp = new SNMPSettings();
      snmp.Settings = new string[] {
        "   snmp-server host 1.1.1.1 traps version 1 password",
        "   snmp-server host 2.2.2.2 traps version 1 password"
      };

      Assert.AreEqual(2, snmp.Servers.Count());
      Assert.AreEqual("1.1.1.1", snmp.Servers.ElementAt(0).Address.ToString());
      Assert.AreEqual(1, snmp.Servers.ElementAt(0).Version);
      Assert.AreEqual("password", snmp.Servers.ElementAt(0).Password);

      Assert.AreEqual("2.2.2.2", snmp.Servers.ElementAt(1).Address.ToString());
      Assert.AreEqual(1, snmp.Servers.ElementAt(1).Version);
      Assert.AreEqual("password", snmp.Servers.ElementAt(1).Password);
    }

    [Test]
    public void snmp_object_should_return_null_if_location_string_is_not_found() {
      var snmp = new SNMPSettings();

      snmp.Settings = new string[] { };

      Assert.IsNull(snmp.Location);
    }

    [Test]
    public void snmp_object_should_correctly_parse_location_string() {
      var snmp = new SNMPSettings();
      snmp.Settings = new string[] {
        "   snmp-server location \"Bldg_3255_Floor_1_Room_IDF_Rack_4_\""
      };

      Assert.AreEqual("Bldg_3255_Floor_1_Room_IDF_Rack_4_", snmp.Location);
    }
  }
}