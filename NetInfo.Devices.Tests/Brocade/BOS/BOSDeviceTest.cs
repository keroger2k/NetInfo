using System.Linq;
using NetInfo.Devices.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class BOSDeviceTest {

    [Test]
    public void bos_device_should_correctly_parse_console_timeout_from_configuration() {
      var config = new AssetBlob {
        Body = @"aaa accounting exec default start-stop  tacacs+
boot sys fl pri
console timeout 3
default-vlan-id 2
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(3, device.ConsoleTimeOut);
    }

    [Test]
    public void bos_device_should_correctly_parse_super_password_from_configuration() {
      var config = new AssetBlob {
        Body = @"enable snmp config-tacacs
enable password-display
enable super-user-password 8 $1$rb1..2H2$7QWsIMCuhHyQ2kw3lYLiH0
enable aaa console
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual("$1$rb1..2H2$7QWsIMCuhHyQ2kw3lYLiH0", device.SuperPassword.Value);
    }

    [Test]
    public void bos_device_should_return_empty_password_when_password_just_contains_periods() {
      var config = new AssetBlob {
        Body = @"enable snmp config-tacacs
enable password-display
enable super-user-password .....
enable aaa console
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual("", device.SuperPassword.Value);
    }

    [Test]
    public void bos_device_should_correctly_have_null_dot1x_when_settings_not_included_in_configuration() {
      var config = new AssetBlob {
        Body = @"!
!"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.IsNull(device.GlobalDot1xSettings);
    }

    [Test]
    public void bos_device_should_correctly_parse_dot1x_gloabl_configuration() {
      var config = new AssetBlob {
        Body = @"!
dot1x-enable
 re-authentication
 servertimeout 15
 timeout quiet-period 30
!"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(true, device.GlobalDot1xSettings.Enabled);
      Assert.AreEqual(true, device.GlobalDot1xSettings.ReAuthentication);
      Assert.AreEqual(15, device.GlobalDot1xSettings.ServerTimeout);
      Assert.AreEqual(30, device.GlobalDot1xSettings.TimeoutQuietPeriod);
    }

    [Test]
    public void bos_device_should_correctly_parse_hostname() {
      var config = new AssetBlob {
        Body = @"!
hostname testing
!"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual("testing", device.Hostname);
    }

    [Test]
    public void bos_device_should_correctly_find_all_lines_in_banner() {
      var config = new AssetBlob {
        Body = @"no web-management http
banner motd ^C
^C
You are accessing a U.S. Government (USG) Information System (IS) that is^C
provided for USG-authorized use only. By using this IS (which includes any^C
device attached to this IS), you consent to the following conditions:^C
^C
Agreement for details.^C
^C
!
ssh access-group 99"
      };

      IBOSDevice device = new BOSDevice(config);
      var results = device.Banner;
      Assert.AreEqual(8, results.Count());
    }

    [Test]
    public void bos_device_should_correctly_return_ip_ssh_settings_with_single_white_spaces() {
      var config = new AssetBlob {
        Body = @"
ip ssh timeout 60
ip ssh idle-time 3
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(60, device.SSH.Timeout);
      Assert.AreEqual(3, device.SSH.AuthenticationRetries);
      Assert.AreEqual(3, device.SSH.IdleTime);
    }

    [Test]
    public void bos_device_should_correctly_return_ip_ssh_settings_with_multiple_spaces() {
      var config = new AssetBlob {
        Body = @"
ip ssh  timeout 60
ip ssh  idle-time 3
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(60, device.SSH.Timeout);
      Assert.AreEqual(3, device.SSH.AuthenticationRetries);
      Assert.AreEqual(3, device.SSH.IdleTime);
    }

    [Test]
    public void bos_device_should_correctly_return_ssh_access_group_setting() {
      var config = new AssetBlob {
        Body = @"
ip ssh  timeout 60
ip ssh  idle-time 3
ssh access-group 99
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(99, device.SSH.AccessGroup);
    }

    [Test]
    public void bos_device_should_correctly_return_0_when_ssh_access_group_is_not_found() {
      var config = new AssetBlob {
        Body = @"
ip ssh  timeout 60
ip ssh  idle-time 3
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(0, device.SSH.AccessGroup);
    }

    [Test]
    public void bos_device_should_return_list_of_radius_servers_when_present() {
      var config = new AssetBlob {
        Body = @"no telnet server
username localadmin password 8 $1$no4..rQ5$Vi7UWWy/5GgDZlDZgjLDr/
radius-server host 10.2.62.161 auth-port 1812 acct-port 1813 default key 1 $g@q@\W8{-| dot1x
radius-server host 10.0.197.237 auth-port 1812 acct-port 1813 default key 1 $\k+W3l!Y-8 dot1x
radius-server timeout 5
tacacs-server host 10.16.27.44
tacacs-server host 10.0.16.152"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(2, device.RadiusServers.Count());
      Assert.AreEqual("10.2.62.161", device.RadiusServers.ElementAt(0).Host.ToString());
      Assert.AreEqual("10.0.197.237", device.RadiusServers.ElementAt(1).Host.ToString());
      Assert.AreEqual("$g@q@\\W8{-|", device.RadiusServers.ElementAt(0).Key.Value);
      Assert.AreEqual("$\\k+W3l!Y-8", device.RadiusServers.ElementAt(1).Key.Value);
    }

    [Test]
    public void bos_device_that_can_not_find_password_encryption_setting_returns_true() {
      var config = new AssetBlob {
        Body = @""
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.True(device.PasswordEncryption);
    }

    [Test]
    public void bos_device_that_finds_password_encryption_setting_returns_false() {
      var config = new AssetBlob {
        Body = @"no service password-encryption"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.False(device.PasswordEncryption);
    }

    [Test]
    public void bos_device_returns_true_when_telnet_server_configuration_is_not_found() {
      var config = new AssetBlob {
        Body = @""
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.True(device.TelnetServer);
    }

    [Test]
    public void bos_device_returns_false_when_telnet_server_configuration_is_found() {
      var config = new AssetBlob {
        Body = @"no telnet server"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.False(device.TelnetServer);
    }

    [Test]
    public void bos_device_has_default_values_for_no_sntp_settings() {
      var config = new AssetBlob {
        Body = @""
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(0, device.SNTP.Servers.Count());
      Assert.AreEqual(0, device.SNTP.PollInterval);
    }

    [Test]
    public void bos_device_correctly_parses_sntp_settings() {
      var config = new AssetBlob {
        Body = @"sntp server 10.26.97.193 4
sntp server 10.26.97.194 4
sntp poll-interval 3600"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(2, device.SNTP.Servers.Count());
      Assert.AreEqual(3600, device.SNTP.PollInterval);
    }

    [Test]
    public void bos_device_should_correctly_find_all_ethernet_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"
mac-authentication hw-deny-age 55
mac-authentication auth-fail-dot1x-override
interface management 1
 disable
!
interface ethernet 1/1/48
 port-name DISABLED
 disable
 speed-duplex 100-full
 no snmp-server enable traps link-change
!
interface ethernet 1/2/1
!
interface ethernet 1/2/2
 disable
!
interface ve 99
 port-name <== Management VLAN ==>
 ip address 10.26.97.20 255.255.255.128
 ip proxy-arp disable
 no ip redirect
!
ip tacacs source-interface ve 99
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(5, device.Interfaces.Count());
      Assert.AreEqual(2, device.Interfaces.Count(c => c.Enabled));
      Assert.AreEqual(1, device.Interfaces.Count(c => !c.Physical));
    }

    [Test]
    public void bos_device_should_correctly_parse_all_vlans_and_their_commands() {
      var config = new AssetBlob {
        Body = @"!
vlan 2 name DEFAULT-VLAN by port
!
vlan 18 name U_NOC_CONSOLES by port
 tagged ethe 1/1/1 ethe 2/1/1
 spanning-tree
!
vlan 978 name U_JEMPRS_COI by port
 tagged ethe 1/1/1 ethe 2/1/1
 spanning-tree
!
!
!
!
dot1x-enable
"
      };

      IBOSDevice device = new BOSDevice(config);
      Assert.AreEqual(3, device.Vlans.Count());
    }
  }
}