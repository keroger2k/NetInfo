using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP001_Tests {

    [Test]
    public void VP001_should_return_true_when_all_interfaces_that_are_down_have_ip_address_0_0_0_0() {
      var blob = new AssetBlob {
        Body = @"
NAEAABCDVP00-> get system
get system
Product Name: SSG5-Serial
Serial Number: 0162052010009109, Control Number: 00000000
Hardware Version: 0710(0)-(00), FPGA checksum: 00000000, VLAN1 IP (0.0.0.0)
Flash Type: Samsung
Software Version: 5.4.0r12-vw5.0, Type: Firewall+VPN
Feature: AV-K
Compiled by build_master at: Wed Apr 7 17:50:20 PDT 2010
Base Mac: 8071.1f32.0d00
File Name: ssg5ssg20.5.4.0r12-vw5.0, Checksum: 4db68db1
, Total Memory: 128MB

Date 10/08/2012 21:00:33, Daylight Saving Time disabled
The Network Time Protocol is Disabled
Up 1274 hours 6 minutes 45 seconds Since 16Aug2012:18:53:48
Total Device Resets: 0

System in NAT/route mode.

Use interface IP, Config Port: 80
Mng Host IP: 138.162.0.0/255.254.0.0
Mng Host IP: 10.16.6.0/255.255.255.0
Mng Host IP: 10.16.27.32/255.255.255.224
Mng Host IP: 10.0.18.0/255.255.255.0
Mng Host IP: 10.0.16.128/255.255.255.224
Mng Host IP: 10.32.9.224/255.255.255.224
User Name: NS-ADMIN

Interface serial0/0:
  description serial0/0
  number 21, if_info 1848, if_index 0
  link down, phy-link down
  vsys Root, zone Null, vr untrust-vr
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 0.0.0.0/0   mac 8071.1f32.0d15
  bandwidth: physical 92kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps
Interface ethernet0/0:
  description ethernet0/0
  number 0, if_info 0, if_index 0, mode route
  link up, phy-link up/full-duplex
  vsys Root, zone Untrust, vr untrust-vr
  dhcp client disabled
  PPPoE disabled
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 70.91.148.89/30   mac 8071.1f32.0d00
  *manage ip 70.91.148.89, mac 8071.1f32.0d00
  route-deny disable
  bandwidth: physical 100000kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps
NAEAABCDVP00-> get chassis"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP001(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP001_should_return_false_when_not_all_interfaces_that_are_down_have_ip_address_0_0_0_0() {
      var blob = new AssetBlob {
        Body = @"
NAEAABCDVP00-> get system
get system
Product Name: SSG5-Serial
Serial Number: 0162052010009109, Control Number: 00000000
Hardware Version: 0710(0)-(00), FPGA checksum: 00000000, VLAN1 IP (0.0.0.0)
Flash Type: Samsung
Software Version: 5.4.0r12-vw5.0, Type: Firewall+VPN
Feature: AV-K
Compiled by build_master at: Wed Apr 7 17:50:20 PDT 2010
Base Mac: 8071.1f32.0d00
File Name: ssg5ssg20.5.4.0r12-vw5.0, Checksum: 4db68db1
, Total Memory: 128MB

Date 10/08/2012 21:00:33, Daylight Saving Time disabled
The Network Time Protocol is Disabled
Up 1274 hours 6 minutes 45 seconds Since 16Aug2012:18:53:48
Total Device Resets: 0

System in NAT/route mode.

Use interface IP, Config Port: 80
Mng Host IP: 138.162.0.0/255.254.0.0
Mng Host IP: 10.16.6.0/255.255.255.0
Mng Host IP: 10.16.27.32/255.255.255.224
Mng Host IP: 10.0.18.0/255.255.255.0
Mng Host IP: 10.0.16.128/255.255.255.224
Mng Host IP: 10.32.9.224/255.255.255.224
User Name: NS-ADMIN

Interface serial0/0:
  description serial0/0
  number 21, if_info 1848, if_index 0
  link down, phy-link down
  vsys Root, zone Null, vr untrust-vr
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 1.1.1.1/32   mac 8071.1f32.0d15
  bandwidth: physical 92kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps
Interface ethernet0/0:
  description ethernet0/0
  number 0, if_info 0, if_index 0, mode route
  link up, phy-link up/full-duplex
  vsys Root, zone Untrust, vr untrust-vr
  dhcp client disabled
  PPPoE disabled
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 70.91.148.89/30   mac 8071.1f32.0d00
  *manage ip 70.91.148.89, mac 8071.1f32.0d00
  route-deny disable
  bandwidth: physical 100000kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps
NAEAABCDVP00-> get chassis"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP001(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}