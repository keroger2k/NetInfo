using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR133_Tests {

    [Test]
    public void IR133_should_return_true_when_there_are_no_aux_ports() {
      var blob = new AssetBlob {
        Body = @"
!
line con 0
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 no modem enable
 transport output all
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR133(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR133_should_return_true_when_line_aux_has_no_access_classes_applied() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 no exec
 transport output none
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR133(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR133_should_return_false_when_aux_port_has_access_class_97_configured() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 access-class 97 in
 no exec
 transport output none
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR133(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR133_should_return_false_when_aux_port_has_access_class_98_configured() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 access-class 98 in
 no exec
 transport output none
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR133(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR133_should_return_false_when_aux_port_has_access_class_99_configured() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 access-class 99 in
 no exec
 transport output none
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR133(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR133_should_return_false_when_aux_port_has_access_class_1_configured() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 access-class 1 in
 no exec
 transport output none
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR133(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}