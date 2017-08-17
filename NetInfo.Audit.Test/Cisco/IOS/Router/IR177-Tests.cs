﻿using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR177_Tests {

    [Test]
    public void IR177_should_return_true_when_service_call_home_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR177(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR177_should_return_true_when_no_service_call_home_is_found() {
      var blob = new AssetBlob {
        Body = @"no service call-home"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR177(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR177_should_return_false_when_serice_call_home_is_found() {
      var blob = new AssetBlob {
        Body = @"service call-home"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR177(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}