using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class SNMPSettingsTests {
    private SNMPSettings snmp;

    [SetUp]
    public void Init() {
      snmp = new SNMPSettings();
      snmp.Settings = genericSettings;
    }

    private IEnumerable<string> genericSettings = @"
set snmp community ""Traj!S5wu7g01C!"" Read-Only Trap-on version v1
set snmp host ""Traj!S5wu7g01C!"" 138.156.125.224 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""Traj!S5wu7g01C!"" 138.156.125.235 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp host ""Traj!S5wu7g01C!"" 205.110.245.164 255.255.255.255 src-interface ethernet0/0 trap v1
set snmp location ""Bldg_24009_Floor_0001_Room_Telco1_Rack_0001_""
set snmp name ""MCUSQUANFWZ00""
set snmp port listen 161
set snmp port trap 162
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void can_correctly_parse_snmp_name() {
      Assert.AreEqual("MCUSQUANFWZ00", snmp.Name);
    }

    [Test]
    public void can_correctly_parse_snmp_location() {
      Assert.AreEqual("Bldg_24009_Floor_0001_Room_Telco1_Rack_0001_", snmp.Location);
    }

    [Test]
    public void can_correctly_parse_snmp_listen_port() {
      Assert.AreEqual(161, snmp.ListenPort);
    }

    [Test]
    public void can_correctly_parse_snmp_trap_port() {
      Assert.AreEqual(162, snmp.TrapPort);
    }
  }
}