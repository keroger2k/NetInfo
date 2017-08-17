using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR116_Tests {

    [Test]
    public void IR116_should_return_true_when_there_is_a_vtp_domain_and_password_configured() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#show config
Using 18348 out of 491512 bytes
!
! Last configuration change at 14:39:10 utc Thu Jul 25 2013 by nasadmin
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
version 15.1
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
service sequence-numbers
!
hostname ABCD-U00-IR-01
!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp status
VTP Version                     : running VTP1 (VTP2 capable)
Configuration Revision          : 0
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 9
VTP Operating Mode              : Transparent
VTP Domain Name                 : VSSD
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0xFA 0xC9 0x47 0xB3 0x66 0xA2 0x68 0xBD
Configuration last modified by 0.0.0.0 at 0-0-00 00:00:00
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp password
VTP Password: C1dQeF!@r
ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR116(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR116_should_return_true_when_there_is_a_vtp_domain_configured_but_show_vtp_password_command_is_not_supported() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#show config
Using 18348 out of 491512 bytes
!
! Last configuration change at 14:39:10 utc Thu Jul 25 2013 by nasadmin
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
version 15.1
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
service sequence-numbers
!
hostname ABCD-U00-IR-01
!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp status
VTP Version                     : running VTP1 (VTP2 capable)
Configuration Revision          : 0
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 9
VTP Operating Mode              : Transparent
VTP Domain Name                 : VSSD
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0xFA 0xC9 0x47 0xB3 0x66 0xA2 0x68 0xBD
Configuration last modified by 0.0.0.0 at 0-0-00 00:00:00
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp password
                        ^
% Invalid input detected at '^' marker.

ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR116(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR116_should_return_false_when_there_is_a_not_a_vtp_domain_configured() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#show config
Using 18348 out of 491512 bytes
!
! Last configuration change at 14:39:10 utc Thu Jul 25 2013 by nasadmin
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
version 15.1
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
service sequence-numbers
!
hostname ABCD-U00-IR-01
!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp status
VTP Version                     : running VTP1 (VTP2 capable)
Configuration Revision          : 0
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 9
VTP Operating Mode              : Transparent
VTP Domain Name                 :
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0xFA 0xC9 0x47 0xB3 0x66 0xA2 0x68 0xBD
Configuration last modified by 0.0.0.0 at 0-0-00 00:00:00
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp password
VTP Password: C1dQeF!@r
ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR116(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR116_should_return_false_when_there_is_a_not_a_vtp_password_configured() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#show config
Using 18348 out of 491512 bytes
!
! Last configuration change at 14:39:10 utc Thu Jul 25 2013 by nasadmin
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
version 15.1
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
service sequence-numbers
!
hostname ABCD-U00-IR-01
!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp status
VTP Version                     : running VTP1 (VTP2 capable)
Configuration Revision          : 0
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 9
VTP Operating Mode              : Transparent
VTP Domain Name                 : VSSD
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0xFA 0xC9 0x47 0xB3 0x66 0xA2 0x68 0xBD
Configuration last modified by 0.0.0.0 at 0-0-00 00:00:00
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp password
The VTP password is not configured.
ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR116(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR116_should_return_false_when_there_is_a_not_a_vtp_password_or_vtp_domain_configured() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#show config
Using 18348 out of 491512 bytes
!
! Last configuration change at 14:39:10 utc Thu Jul 25 2013 by nasadmin
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
version 15.1
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
service sequence-numbers
!
hostname ABCD-U00-IR-01
!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp status
VTP Version                     : running VTP1 (VTP2 capable)
Configuration Revision          : 0
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 9
VTP Operating Mode              : Transparent
VTP Domain Name                 :
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0xFA 0xC9 0x47 0xB3 0x66 0xA2 0x68 0xBD
Configuration last modified by 0.0.0.0 at 0-0-00 00:00:00
ABCD-U00-IR-01#!
ABCD-U00-IR-01#show vtp password
The VTP password is not configured.
ABCD-U00-IR-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR116(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR116_should_return_true_when_device_type_is_GW_voice_gateway_per_bug_11620() {
      var blob = new AssetBlob {
        Body = @"NFLK-U00-GW-01#show config
Using 18348 out of 491512 bytes
!
! Last configuration change at 14:39:10 utc Thu Jul 25 2013 by nasadmin
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
! NVRAM config last updated at 23:40:13 utc Wed Jul 31 2013 by cmang
version 15.1
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
service sequence-numbers
!
hostname NFLK-U00-GW-01
!
NFLK-U00-GW-01#!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR116(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}