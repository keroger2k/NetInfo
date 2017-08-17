﻿using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS067_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IS067_should_return_true_when_all_physical_interfaces_that_are_disabled_are_assigned_to_vlan_2() {
      blob = new AssetBlob {
        Body = @"!
interface Vlan30
!
interface GigabitEthernet3/30
switchport access vlan 2
shutdown
!
interface GigabitEthernet3/31
switchport access vlan 2
shutdown
!
interface GigabitEthernet3/32
switchport access vlan 2
shutdown
!
interface GigabitEthernet3/33
switchport access vlan 2
shutdown
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS067(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS067_should_return_false_when_not_all_physical_interfaces_that_are_disabled_are_assigned_to_vlan_2() {
      blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet3/30
shutdown
!
interface GigabitEthernet3/31
shutdown
!
interface GigabitEthernet3/32
shutdown
!
interface GigabitEthernet3/33
shutdown
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS067(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS067_should_return_false_when_not_all_physical_interfaces_that_are_disabled_are_assigned_to_vlan_2_and_not_default_vlan_1() {
      blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet3/30
switchport access vlan 3
shutdown
!
interface GigabitEthernet3/31
switchport access vlan 3
shutdown
!
interface GigabitEthernet3/32
switchport access vlan 3
shutdown
!
interface GigabitEthernet3/33
switchport access vlan 3
shutdown
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS067(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}