using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS100_Tests {

    [Test]
    public void IS100_should_return_true_when_there_are_no_interfaces_with_test() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS100(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS100_should_return_true_when_all_test_interfaces_are_connected() {
      var blob = new AssetBlob {
        Body = @"AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#show interface status
ATM0 is up, line protocol is up
  Hardware is PQUICC_SAR (with Globespan G.SHDSL module)
  Description: <== TEST ==>
  MTU 1500 bytes, sub MTU 1500, BW 2304 Kbit/sec, DLY 80 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ATM, loopback not set
  Encapsulation(s): AAL5  AAL2, PVC mode
  10 maximum active VCs, 1024 VCs per VP, 3 current VCCs
  VC Auto Creation Disabled.
  VC idle disconnect time: 300 seconds
  Last input 00:00:00, output 00:00:00, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: Per VC Queueing
  5 minute input rate 9000 bits/sec, 12 packets/sec
  5 minute output rate 4000 bits/sec, 6 packets/sec
     113366695 packets input, 838421142 bytes, 20007 no buffer
     Received 60124849 broadcasts, 0 runts, 0 giants, 0 throttles
     0 input errors, 409 CRC, 0 frame, 0 overrun, 0 ignored, 0 abort
     681040 packets output, 132289286 bytes, 0 underruns
     0 output errors, 0 collisions, 620 interface resets
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out
BVI99 is up, line protocol is up
  Hardware is BVI, address is 0000.0cb2.2b44 (bia 0008.21b8.9dec)
  Internet address is 10.31.3.50/23
  MTU 1500 bytes, BW 2312 Kbit/sec, DLY 5000 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/422053/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/0 (size/max)
  5 minute input rate 4000 bits/sec, 4 packets/sec
  5 minute output rate 4000 bits/sec, 6 packets/sec
     1773890 packets input, 155774761 bytes, 0 no buffer
     Received 0 broadcasts, 0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored, 0 abort
     461600 packets output, 58217710 bytes, 0 underruns
     0 output errors, 0 collisions, 0 interface resets
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out
AKOZ-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS100(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS100_should_return_false_when_a_test_interface_is_not_connected() {
      var blob = new AssetBlob {
        Body = @"AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#show interface status
ATM0 is up, line protocol is down
  Hardware is PQUICC_SAR (with Globespan G.SHDSL module)
  Description: <== TEST ==>
  MTU 1500 bytes, sub MTU 1500, BW 2304 Kbit/sec, DLY 80 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ATM, loopback not set
  Encapsulation(s): AAL5  AAL2, PVC mode
  10 maximum active VCs, 1024 VCs per VP, 3 current VCCs
  VC Auto Creation Disabled.
  VC idle disconnect time: 300 seconds
  Last input 00:00:00, output 00:00:00, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: Per VC Queueing
  5 minute input rate 9000 bits/sec, 12 packets/sec
  5 minute output rate 4000 bits/sec, 6 packets/sec
     113366695 packets input, 838421142 bytes, 20007 no buffer
     Received 60124849 broadcasts, 0 runts, 0 giants, 0 throttles
     0 input errors, 409 CRC, 0 frame, 0 overrun, 0 ignored, 0 abort
     681040 packets output, 132289286 bytes, 0 underruns
     0 output errors, 0 collisions, 620 interface resets
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out
BVI99 is up, line protocol is up
  Hardware is BVI, address is 0000.0cb2.2b44 (bia 0008.21b8.9dec)
  Internet address is 10.31.3.50/23
  MTU 1500 bytes, BW 2312 Kbit/sec, DLY 5000 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/422053/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/0 (size/max)
  5 minute input rate 4000 bits/sec, 4 packets/sec
  5 minute output rate 4000 bits/sec, 6 packets/sec
     1773890 packets input, 155774761 bytes, 0 no buffer
     Received 0 broadcasts, 0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored, 0 abort
     461600 packets output, 58217710 bytes, 0 underruns
     0 output errors, 0 collisions, 0 interface resets
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out
AKOZ-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS100(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS100_should_return_true_when_a_test_interface_is_disabled() {
      var blob = new AssetBlob {
        Body = @"AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#show interface status
ATM0 is administratively down, line protocol is down
  Hardware is PQUICC_SAR (with Globespan G.SHDSL module)
  Description: <== TEST ==>
  MTU 1500 bytes, sub MTU 1500, BW 2304 Kbit/sec, DLY 80 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ATM, loopback not set
  Encapsulation(s): AAL5  AAL2, PVC mode
  10 maximum active VCs, 1024 VCs per VP, 3 current VCCs
  VC Auto Creation Disabled.
  VC idle disconnect time: 300 seconds
  Last input 00:00:00, output 00:00:00, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: Per VC Queueing
  5 minute input rate 9000 bits/sec, 12 packets/sec
  5 minute output rate 4000 bits/sec, 6 packets/sec
     113366695 packets input, 838421142 bytes, 20007 no buffer
     Received 60124849 broadcasts, 0 runts, 0 giants, 0 throttles
     0 input errors, 409 CRC, 0 frame, 0 overrun, 0 ignored, 0 abort
     681040 packets output, 132289286 bytes, 0 underruns
     0 output errors, 0 collisions, 620 interface resets
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out
BVI99 is up, line protocol is up
  Hardware is BVI, address is 0000.0cb2.2b44 (bia 0008.21b8.9dec)
  Internet address is 10.31.3.50/23
  MTU 1500 bytes, BW 2312 Kbit/sec, DLY 5000 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/422053/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/0 (size/max)
  5 minute input rate 4000 bits/sec, 4 packets/sec
  5 minute output rate 4000 bits/sec, 6 packets/sec
     1773890 packets input, 155774761 bytes, 0 no buffer
     Received 0 broadcasts, 0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored, 0 abort
     461600 packets output, 58217710 bytes, 0 underruns
     0 output errors, 0 collisions, 0 interface resets
     0 unknown protocol drops
     0 output buffer failures, 0 output buffers swapped out
AKOZ-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS100(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}