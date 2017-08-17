using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR163_Tests {

    [Test]
    public void IR163_should_return_true_when_acl_is_applied_to_internal_interfaces_in_an_inbound_direction() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface Vlan23
 description <== U_ODMN_TB ==>
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
interface Vlan39
 description <== Public Side cTB TACLANE Black Side ==>
!
interface Vlan40
 description <== Public Side IA Transport Boundary ==>
 ip access-group OR01_NMCI_UTB_OUT_V1-1-0 in
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR163(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR163_should_return_false_when_acl_is_applied_to_internal_interfaces_in_an_outbound_direction() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface Vlan23
 description <== U_ODMN_TB ==>
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
interface Vlan39
 description <== Public Side cTB TACLANE Black Side ==>
!
interface Vlan40
 description <== Public Side IA Transport Boundary ==>
 ip access-group OR01_NMCI_UTB_OUT_V1-1-0 out
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR163(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR163_should_return_false_when_acl_is_not_applied_to_internal_interface() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface Vlan23
 description <== U_ODMN_TB ==>
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 in
 ip access-group ODMN_ONLY_TRAFFIC_ACL_V2-0-0 out
!
interface Vlan39
 description <== Public Side cTB TACLANE Black Side ==>
!
interface Vlan40
 description <== Public Side IA Transport Boundary ==>
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR163(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}