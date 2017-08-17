using NetInfo.Devices.Juniper.ScreenOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class ScreenOSInterfaceTests {

    [Test]
    public void should_be_able_to_parse_ip_address_from_interface() {
      var iface = new ScreenOSInterface(@"Interface ethernet0/0:
  description ethernet0/0
  number 0, if_info 0, if_index 0, mode route
  link up, phy-link up/full-duplex
  vsys Root, zone Untrust, vr untrust-vr
  dhcp client disabled
  PPPoE disabled
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 70.91.148.89/30   mac 8071.1f32.0d00
  *manage ip 70.91.148.89, mac 8071.1f32.0d00
  route-deny disable
  bandwidth: physical 100000kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps".ToConfig());

      Assert.AreEqual("70.91.148.89", iface.Address.NetworkAddress.ToString());
      Assert.AreEqual(30, iface.Address.MaskBytes);
    }

    [Test]
    public void should_be_able_to_parse_ip_address_from_interface_that_does_not_have_leading_star() {
      var iface = new ScreenOSInterface(@"Interface ethernet0/0:
  description ethernet0/0
  number 0, if_info 0, if_index 0, mode route
  link up, phy-link up/full-duplex
  vsys Root, zone Untrust, vr untrust-vr
  dhcp client disabled
  PPPoE disabled
  admin mtu 0, operating mtu 1500, default mtu 1500
  ip 70.91.148.89/30   mac 8071.1f32.0d00
  manage ip 70.91.148.89, mac 8071.1f32.0d00
  route-deny disable
  bandwidth: physical 100000kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps".ToConfig());

      Assert.AreEqual("70.91.148.89", iface.Address.NetworkAddress.ToString());
      Assert.AreEqual(30, iface.Address.MaskBytes);
    }

    [Test]
    public void should_be_able_to_parse_link_status_for_interface_that_is_up() {
      var iface = new ScreenOSInterface(@"Interface ethernet0/0:
  description ethernet0/0
  number 0, if_info 0, if_index 0, mode route
  link up, phy-link up/full-duplex
  vsys Root, zone Untrust, vr untrust-vr
  dhcp client disabled
  PPPoE disabled
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 70.91.148.89/30   mac 8071.1f32.0d00
  *manage ip 70.91.148.89, mac 8071.1f32.0d00
  route-deny disable
  bandwidth: physical 100000kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps".ToConfig());

      Assert.AreEqual(ScreenOSInterface.Link.up, iface.Status);
    }

    [Test]
    public void should_be_able_to_parse_link_status_for_interface_that_is_down() {
      var iface = new ScreenOSInterface(@"Interface ethernet0/0:
  description ethernet0/0
  number 0, if_info 0, if_index 0, mode route
  link down, phy-link down
  vsys Root, zone Untrust, vr untrust-vr
  dhcp client disabled
  PPPoE disabled
  admin mtu 0, operating mtu 1500, default mtu 1500
  *ip 70.91.148.89/30   mac 8071.1f32.0d00
  *manage ip 70.91.148.89, mac 8071.1f32.0d00
  route-deny disable
  bandwidth: physical 100000kbps, configured egress [gbw 0kbps mbw 0kbps]
             configured ingress mbw 0kbps, current bw 0kbps
             total allocated gbw 0kbps".ToConfig());

      Assert.AreEqual(ScreenOSInterface.Link.down, iface.Status);
    }
  }
}