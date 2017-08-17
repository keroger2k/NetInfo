using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.Juniper.ScreenOS;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP027_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void ensure_manager_addresses_can_compare_correctly() {
      var source = new AdminSettings.ManagerAddress { Network = IPAddress.Parse("1.1.1.1"), Subnet = IPAddress.Parse("255.255.255.255") };

      var compareTo = new List<AdminSettings.ManagerAddress> {
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("1.1.1.1"), Subnet = IPAddress.Parse("255.255.255.255") },
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("2.2.2.2"), Subnet = IPAddress.Parse("255.255.255.255") },
      };

      Assert.True(compareTo.Contains(source));
    }

    [Test]
    public void VP027_should_return_true_for_juniper_complaint_device() {
      blob = new AssetBlob {
        Body = @"
set admin manager-ip 1.1.1.1 255.255.255.255
set admin manager-ip 2.2.2.2 255.255.255.255"
      };

      var compareTo = new List<AdminSettings.ManagerAddress> {
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("1.1.1.1"), Subnet = IPAddress.Parse("255.255.255.255") },
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("2.2.2.2"), Subnet = IPAddress.Parse("255.255.255.255") },
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP027(device, compareTo);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP027_should_return_false_for_juniper_noncomplaint_device_example1() {
      blob = new AssetBlob {
        Body = @"
set admin manager-ip 1.1.1.1 255.255.255.255
set admin manager-ip 2.2.2.2 255.255.255.255
set admin manager-ip 3.3.3.3 255.255.255.255"
      };

      var compareTo = new List<AdminSettings.ManagerAddress> {
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("1.1.1.1"), Subnet = IPAddress.Parse("255.255.255.255") },
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("2.2.2.2"), Subnet = IPAddress.Parse("255.255.255.255") },
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP027(device, compareTo);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP027_should_return_false_for_juniper_noncomplaint_device_example2() {
      blob = new AssetBlob {
        Body = @"
set admin manager-ip 1.1.1.1 255.255.255.255"
      };

      var compareTo = new List<AdminSettings.ManagerAddress> {
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("1.1.1.1"), Subnet = IPAddress.Parse("255.255.255.255") },
        new AdminSettings.ManagerAddress { Network = IPAddress.Parse("2.2.2.2"), Subnet = IPAddress.Parse("255.255.255.255") },
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP027(device, compareTo);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}