using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowInterfaceTests {

    [Test]
    public void show_interface_can_determine_correct_number_of_interfaces() {
      var status = new ShowInterface(@"Ethernet0 is administratively down, line protocol is down
  Hardware is PQUICC Ethernet, address is 0008.21b8.9dec (bia 0008.21b8.9dec)
  MTU 1500 bytes, BW 10000 Kbit/sec, DLY 1000 usec,
     reliability 255/255, txload 1/255, rxload 1/255
     0 lost carrier, 0 no carrier
     0 output buffer failures, 0 output buffers swapped out
ATM0 is up, line protocol is up
  Hardware is PQUICC_SAR (with Globespan G.SHDSL module)
  Description: <== PAXR-U06-DH-01 ==>
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out
ATM0.2 is up, line protocol is up
  Hardware is PQUICC_SAR (with Globespan G.SHDSL module)
  MTU 1500 bytes, BW 2312 Kbit/sec, DLY 80 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  AAL5 Oversized SDUs : 0
  Last clearing of ""show interface"" counters never
BVI99 is up, line protocol is up
  Hardware is BVI, address is 0000.0cb2.2b44 (bia 0008.21b8.9dec)
  5 minute output rate 4000 bits/sec, 6 packets/sec
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out".ToConfig());

      Assert.AreEqual(4, status.Interfaces.Count());
    }
  }
}