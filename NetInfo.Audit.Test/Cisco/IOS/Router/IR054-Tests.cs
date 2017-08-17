using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR054_Tests {

    [Test]
    public void IR054_should_return_true_when_there_are_no_aux_ports() {
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
      ISTIGItem item = new IR054(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR054_should_return_true_when_lin_aux_has_no_exec_transport_output_none_and_no_transport_input() {
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
      ISTIGItem item = new IR054(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR054_should_return_false_when_lin_aux_has_no_exec_no_transport_output_none_and_no_transport_input() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 no exec
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR054(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR054_should_return_false_when_lin_aux_has_NO_no_exec_transport_output_none_and_no_transport_input() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 transport output none
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR054(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR054_should_return_false_when_lin_aux_has_NO_no_exec_NO_transport_output_none_and_no_transport_input() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR054(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR054_should_return_false_when_lin_aux_has_no_exec_transport_output_none_and_HAS_transport_input() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 no exec
 transport output none
 transport input ssh
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR054(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR054_should_return_false_when_lin_aux_has_NO_no_exec_NO_transport_output_none_and_HAS_transport_input() {
      var blob = new AssetBlob {
        Body = @"
!
line aux 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input ssh
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR054(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}