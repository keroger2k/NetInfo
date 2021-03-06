﻿using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS025_Tests {

    [Test]
    public void IS025_should_return_true_when_service_password_encryption_is_found() {
      var blob = new AssetBlob {
        Body = @"service password-encryption"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS025(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS025_should_return_false_when_service_password_encryption_is_not_enabled() {
      var blob = new AssetBlob {
        Body = @"no service password-encryption"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS025(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS025_should_return_false_when_service_password_encryption_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS025(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}