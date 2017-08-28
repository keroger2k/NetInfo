using System.Collections.Generic;
using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR007_Tests {

    [Test]
    public void IR007_should_return_true_for_non_inner_router_device_with_single_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U01-AS-01
ntp server 1.1.1.1
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR007_should_return_true_for_non_inner_router_device_with_single_sntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U01-AS-01
sntp server 1.1.1.1
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR007_should_return_false_for_non_inner_router_device_with_zero_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U01-AS-01
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR007_should_return_false_for_inner_router_device_with_zero_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U00-IR-01
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR007_should_return_false_for_inner_router_device_with_one_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U00-IR-01
ntp server 1.1.1.1
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR007_should_return_true_for_inner_router_device_with_two_or_more_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U00-IR-01
ntp server 1.1.1.1
ntp server 1.1.1.2
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR007_should_return_false_for_outer_router_device_with_zero_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U00-OR-01
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR007_should_return_false_for_outer_router_device_with_one_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U00-OR-01
ntp server 1.1.1.1
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR007_should_return_true_for_outer_router_device_with_two_or_more_ntp_server_at_non_redundant_site() {
      var blob = new AssetBlob {
        Body = @"
hostname ABCD-U00-OR-01
ntp server 1.1.1.1
ntp server 1.1.1.2
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR007_should_return_false_for_site_it_does_find_site_type() {
      var blob = new AssetBlob {
        Body = @"
hostname XXXX-U00-OR-01
ntp server 1.1.1.1
ntp server 1.1.1.2
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET0812(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}