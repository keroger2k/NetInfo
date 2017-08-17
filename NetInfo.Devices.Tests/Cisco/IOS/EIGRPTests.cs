using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class EIGRP {

    [Test]
    public void eigrp_should_be_able_to_parse_its_asn() {
      var eigrp = new EnhancedInteriorGatewayRoutingProtocol();
      eigrp.Settings = @"router eigrp 1
 redistribute static metric 10000 100 255 1 1486 route-map CRTY_CIDR
 redistribute ospf 1001 metric 10000 100 255 1 1486 route-map CRTY_CIDR
 network 138.168.38.16 0.0.0.3
 network 138.168.64.172 0.0.0.3
 distance eigrp 90 105
 no auto-summary
".ToConfig();

      Assert.AreEqual(1, eigrp.ASN);
    }

    [Test]
    public void eigrp_should_be_able_to_handle_no_network_statements() {
      var eigrp = new EnhancedInteriorGatewayRoutingProtocol();
      eigrp.Settings = @"router eigrp 1
 redistribute static metric 10000 100 255 1 1486 route-map CRTY_CIDR
 redistribute ospf 1001 metric 10000 100 255 1 1486 route-map CRTY_CIDR
 distance eigrp 90 105
 no auto-summary
".ToConfig();

      Assert.AreEqual(0, eigrp.Networks.Count());
    }

    [Test]
    public void eigrp_should_be_able_to_parse_its_network_statements_to_network_objects() {
      var eigrp = new EnhancedInteriorGatewayRoutingProtocol();
      eigrp.Settings = @"router eigrp 1
 redistribute static metric 10000 100 255 1 1486 route-map CRTY_CIDR
 redistribute ospf 1001 metric 10000 100 255 1 1486 route-map CRTY_CIDR
 network 138.168.38.16 0.0.0.3
 network 138.168.64.172 0.0.0.3
 distance eigrp 90 105
 no auto-summary
".ToConfig();

      Assert.AreEqual(2, eigrp.Networks.Count());
    }
  }
}