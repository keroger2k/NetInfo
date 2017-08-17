using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class ScreenOSDeviceTest {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void screenos_device_should_correctly_parse_clock_timezone_for_5x_configuration() {
      blob = new AssetBlob {
        Body = @"set clock timezone 0"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      string result = device.GetClockTimezone();

      Assert.AreEqual("set clock timezone 0", result);
    }

    [Test]
    public void screenos_device_should_correctly_parse_clock_timezone_for_4x_configuration() {
      blob = new AssetBlob {
        Body = @"set clock ""timezone"" 0"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      string result = device.GetClockTimezone();

      Assert.AreEqual("set clock \"timezone\" 0", result);
    }

    [Test]
    public void screenos_device_should_correctly_parse_admin_password_from_4x_configuration() {
      blob = new AssetBlob {
        Body = @"set admin password nPPUKJr1JNbOcxBASspDn1DtcHJSrn"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      string result = device.AdminSettings.Password;

      Assert.AreEqual("nPPUKJr1JNbOcxBASspDn1DtcHJSrn", result);
    }

    [Test]
    public void screenos_device_should_correctly_identify_hostname() {
      blob = new AssetBlob {
        Body = @"
!
set hostname MCUSBANDVP00
!
"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      Assert.AreEqual("MCUSBANDVP00", device.Hostname);
    }

    [Test]
    public void screenos_device_should_correctly_identify_scs_setting() {
      blob = new AssetBlob {
        Body = @"
!
set scs enable
!
"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      Assert.True(device.SCSEnabled);
    }

    [Test]
    public void screenos_device_should_correctly_identify_ssh_setting() {
      blob = new AssetBlob {
        Body = @"
!
set ssh enable
!
"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      Assert.True(device.SSHEnabled);
    }

    [Test]
    public void screenos_device_should_correctly_identify_auth_setting1() {
      blob = new AssetBlob {
        Body = @"
!
set auth-server ""Local"" id 0
!
"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      Assert.True(device.AutherServerLocalId);
    }

    [Test]
    public void screenos_device_should_correctly_parse_number_of_interfaces() {
      blob = new AssetBlob {
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
Interface ethernet0/1:
  description ethernet0/1
  number 5, if_info 440, if_index 0
  link up, phy-link up/full-duplex
  member of bgroup0
  vsys Root, zone Null, vr untrust-vr
  *ip 0.0.0.0/0   mac 8071.1f32.0d05
Interface bgroup0:
  description bgroup0
  number 11, if_info 968, if_index 0, mode nat
  link up, phy-link up/full-duplex
  vsys Root, zone Trust, vr trust-vr
  dhcp client disabled
  PPPoE disabled
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 10.46.4.66/30   mac 8071.1f32.0d0b
  *manage ip 10.46.4.66, mac 8071.1f32.0d0b
  route-deny disable
  bandwidth: physical 100000kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps
Interface bgroup3:
  description bgroup3
  number 14, if_info 1232, if_index 0
  link down, phy-link down
  vsys Root, zone Null, vr untrust-vr
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 0.0.0.0/0   mac 8071.1f32.0d0e
  bandwidth: physical 0kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps
NAEAABCDVP00-> get chassis
"
      };

      IScreenOSDevice device = new ScreenOSDevice(blob);

      Assert.AreEqual(5, device.Interfaces.Count());
    }
  }
}