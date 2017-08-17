﻿using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR168_Tests {

    [Test]
    public void should_return_true_for_device_correct_tacacs_source_interface() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip tacacs source-interface Vlan23"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR168(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void should_return_false_for_device_with_incorrect_tacacs_source_interface() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip tacacs source-interface Vlan30"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR168(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}