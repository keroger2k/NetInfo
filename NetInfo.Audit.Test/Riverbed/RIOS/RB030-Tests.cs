using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB030_Tests {

    [Test]
    public void RB030_should_return_true_when_all_unused_interfaces_are_shutdown() {
      var blob = new AssetBlob {
        Body = @" PRLH-U00-CM-01 # show interface brief
Interface primary state
   Up:                 yes
   IP address:         10.32.8.56
   Netmask:            255.255.254.0
   Speed:              100Mb/s
   Duplex:             full
   Interface type:     ethernet
   MTU:                1400
   HW address:         00:0E:B6:2B:9E:00
   Link:               yes

Interface aux state
   Up:                 no
   IP address:
   Netmask:
   Speed:              UNKNOWN
   Duplex:             half (auto)
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:2B:9E:01
   Link:               no

Interface lo state
   Up:                 yes
   IP address:         127.0.0.1
   Netmask:            255.0.0.0
   Speed:              N/A
   Duplex:             N/A
   Interface type:     loopback
   MTU:                16436
   HW address:         N/A

PRLH-U00-CM-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB030(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB030_should_should_return_true_when_all_interaface_are_shutdown_correctly() {
      var blob = new AssetBlob {
        Body = @"PRLH-U00-WI-01 # show interface brief
Interface primary state
   Up:                 yes
   IP address:         10.32.8.58
   Netmask:            255.255.254.0
   Speed:              100Mb/s
   Duplex:             full
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:2D:97:58
   Link:               yes

Interface aux state
   Up:                 no
   IP address:
   Netmask:
   Speed:              UNKNOWN
   Duplex:             half (auto)
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:2D:97:59
   Link:               no

Interface lo state
   Up:                 yes
   IP address:         127.0.0.1
   Netmask:            255.0.0.0
   Speed:              N/A
   Duplex:             N/A
   Interface type:     loopback
   MTU:                16436
   HW address:         N/A

Interface wan0_0 state
   Up:                 yes
   IP address:
   Netmask:
   Speed:              1000Mb/s (auto)
   Duplex:             full (auto)
   Interface type:     ethernet
   MTU:                1420
   HW address:         00:0E:B6:85:9E:DE
   Link:               yes

Interface lan0_0 state
   Up:                 yes
   IP address:
   Netmask:
   Speed:              1000Mb/s (auto)
   Duplex:             full (auto)
   Interface type:     ethernet
   MTU:                1420
   HW address:         00:0E:B6:85:9E:DF
   Link:               yes

Interface inpath0_0 state
   Up:                 yes
   IP address:         10.32.0.132
   Netmask:            255.255.255.240
   Speed:              N/A
   Duplex:             N/A
   Interface type:     ethernet
   MTU:                1420
   HW address:         00:0E:B6:85:9E:DF

PRLH-U00-WI-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB030(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB030_should_return_false_when_not_all_unused_interfaces_are_shutdown() {
      var blob = new AssetBlob {
        Body = @"PRLH-UST-WI-01 # show interface brief
Interface primary state
   Up:                 yes
   IP address:         172.19.1.69
   Netmask:            255.255.255.224
   Speed:              100Mb/s
   Duplex:             full
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:2D:8C:80
   Link:               yes

Interface aux state
   Up:                 no
   IP address:
   Netmask:
   Speed:              UNKNOWN
   Duplex:             half (auto)
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:2D:8C:81
   Link:               no

Interface lo state
   Up:                 yes
   IP address:         127.0.0.1
   Netmask:            255.0.0.0
   Speed:              N/A
   Duplex:             N/A
   Interface type:     loopback
   MTU:                16436
   HW address:         N/A

Interface wan0_1 state
   Up:                 no
   IP address:
   Netmask:
   Speed:              UNKNOWN
   Duplex:             UNKNOWN
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:26:75:2F
   Link:               no

Interface lan0_1 state
   Up:                 yes
   IP address:
   Netmask:
   Speed:              UNKNOWN
   Duplex:             UNKNOWN
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:26:75:2E
   Link:               no

Interface wan0_0 state
   Up:                 yes
   IP address:
   Netmask:
   Speed:              100Mb/s
   Duplex:             full
   Interface type:     ethernet
   MTU:                1420
   HW address:         00:0E:B6:26:75:2D
   Link:               yes

Interface lan0_0 state
   Up:                 yes
   IP address:
   Netmask:
   Speed:              100Mb/s
   Duplex:             full
   Interface type:     ethernet
   MTU:                1420
   HW address:         00:0E:B6:26:75:2C
   Link:               yes

Interface inpath0_0 state
   Up:                 yes
   IP address:         10.242.0.3
   Netmask:            255.255.255.224
   Speed:              N/A
   Duplex:             N/A
   Interface type:     ethernet
   MTU:                1420
   HW address:         00:0E:B6:26:75:2C

Interface inpath0_1 state
   Up:                 yes
   IP address:
   Netmask:
   Speed:              N/A
   Duplex:             N/A
   Interface type:     ethernet
   MTU:                1500
   HW address:         00:0E:B6:26:75:2E

PRLH-UST-WI-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB030(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}