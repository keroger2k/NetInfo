using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.Riverbed.RIOS {

  [TestFixture]
  public class NMCISNMPSettingsTests {

    [Test]
    public void snmp_settings_should_return_null_when_no_location_string_is_found() {
      var settings = new NMCISNMPSettings();
      settings.Settings = new string[] { };

      Assert.IsNull(settings.Location);
    }

    [Test]
    public void snmp_settings_should_correctly_parse_location_string() {
      var setttings = new NMCISNMPSettings();
      setttings.Settings = new string[] {
        "   snmp-server location \"Bldg_3255_Floor_1_Room_IDF_Rack_4_\""
      };

      Assert.AreEqual("3255", setttings.Location.Building);
      Assert.AreEqual("1", setttings.Location.Floor);
      Assert.AreEqual("IDF", setttings.Location.Room);
      Assert.AreEqual("4", setttings.Location.Rack);
    }
  }
}