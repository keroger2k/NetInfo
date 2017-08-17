using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR026_Tests {

    [Test]
    public void IR026_should_return_true_when_all_interfaces_with_CDP_enabled_are_connected_to_NMCI_devices() {
      var blob = new AssetBlob {
        Body = @"QUAN-UB1-EO-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/2 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#show cdp neighbor
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
QUAN-UB1-OR-01.NMCI-ISF.com
                 Gig 3/1            168          R S      WS-C6506  Gig 2/5
QUAN-UB1-OS-02.NMCI-ISF.com
                 Gig 3/2            161           S       WS-C4503  Fas 3/22
QUAN-UB1-EO-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR026(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR026_should_return_false_when_interfaces_that_are_not_connected_to_NMCI_devices_have_CDP_enabled() {
      var blob = new AssetBlob {
        Body = @"QUAN-UB1-EO-01#show cdp interface
Serial1/0/4:0 is up, line protocol is up
  Encapsulation PPP
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Serial1/0/5:0 is up, line protocol is up
  Encapsulation PPP
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/2 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#show cdp neighbor
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
QUAN-UB1-OR-01.NMCI-ISF.com
                 Gig 3/1            168          R S      WS-C6506  Gig 2/5
QUAN-UB1-OS-02.NMCI-ISF.com
                 Gig 3/2            161           S       WS-C4503  Fas 3/22
QUAN-UB1-EO-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR026(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR026_should_return_false_when_the_number_of_interfaces_match_but_connected_device_is_non_NMCI() {
      var blob = new AssetBlob {
        Body = @"QUAN-UB1-EO-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/2 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#show cdp neighbor
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
QUAN-UB1-OR-01.NMCI-ISF.com
                 Gig 3/1            168          R S      WS-C6506  Gig 2/5
SOME-External-Router.com
                 Gig 3/2            161           S       WS-C4503  Fas 3/22
QUAN-UB1-EO-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR026(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR026_should_ignore_VRF_interfaces() {
      var blob = new AssetBlob {
        Body = @"QUAN-UB1-EO-01#show cdp interface
VRF_0_vlan1015 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds  <--- CDP enabled
  Holdtime is 180 seconds
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
GigabitEthernet3/2 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#!
QUAN-UB1-EO-01#show cdp neighbor
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce    Holdtme    Capability  Platform  Port ID
QUAN-UB1-OR-01.NMCI-ISF.com
                Gig 3/1            168          R S      WS-C6506  Gig 2/5
QUAN-UB1-OS-02.NMCI-ISF.com
                Gig 3/2            128          S      WS-C4503  Fas 3/22
QUAN-UB1-EO-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR026(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}