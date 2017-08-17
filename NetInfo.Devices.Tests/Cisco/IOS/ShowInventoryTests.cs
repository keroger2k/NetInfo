using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowInventoryTests {

    [Test]
    public void should_correctly_parse_model_for_output_in_show_inventory_1() {
      var sv = new ShowInventory(@"NAME: ""2611XM chassis"", DESCR: ""2611XM chassis""
PID: C2611XM-2FE       , VID: 3.1, SN: FOC08300Y0S

NAME: ""WAN Interface Card - ATM (With GSHDSL module)"", DESCR: ""WAN Interface Card - ATM (With GSHDSL module)""
PID: WIC-1SHDSL=       , VID: 1.0, SN: FOC08461TKN

NAME: ""WAN Interface Card - ATM (With GSHDSL module)"", DESCR: ""WAN Interface Card - ATM (With GSHDSL module)""
PID: WIC-1SHDSL=       , VID: 1.0, SN: FOC08461TGE

".ToConfig());
      Assert.AreEqual("C2611XM-2FE", sv.Model);
    }

    [Test]
    public void should_correctly_parse_model_for_output_in_show_inventory_2() {
      var sv = new ShowInventory(@"NAME: ""WS-C6503-E"", DESCR: ""Cisco Systems Catalyst 6500 3-slot Chassis System""
PID: WS-C6503-E        , VID: V02, SN: FOX1544GUH1

NAME: ""CLK-7600 1"", DESCR: ""OSR-7600 Clock FRU 1""
PID: CLK-7600          , VID:    , SN: NWG15410AVH

".ToConfig());
      Assert.AreEqual("WS-C6503-E", sv.Model);
    }
  }
}