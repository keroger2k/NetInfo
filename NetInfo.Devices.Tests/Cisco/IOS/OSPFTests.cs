using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS
{

    [TestFixture]
    public class OSPFTests
    {

        [Test]
        public void ospf_should_be_able_to_parse_its_process_id()
        {
            var ospf = new OpenShortestPathFirstProtocol();
            ospf.Settings = @"router ospf 616
 log-adjacency-changes
".ToConfig();

            Assert.AreEqual(616, ospf.ProcessId);
        }

        [Test]
        public void ospf_should_be_able_to_parse_which_interfaces_are_configured_for_passive()
        {
            var ospf = new OpenShortestPathFirstProtocol();
            ospf.Settings = @"router ospf 1001
 log-adjacency-changes
 area 0 authentication message-digest
 passive-interface default
 no passive-interface Vlan90
 no passive-interface Vlan91
 network 10.25.68.0 0.0.0.63 area 0".ToConfig();

            Assert.AreEqual(2, ospf.NonPassiveInterfaces.Count());
            Assert.AreEqual("Vlan90", ospf.NonPassiveInterfaces.ElementAt(0));
            Assert.AreEqual("Vlan91", ospf.NonPassiveInterfaces.ElementAt(1));
        }


        [Test]
        public void ospf_should_be_able_to_parse_area_numbers_participating_in_authentication()
        {
            var ospf = new OpenShortestPathFirstProtocol();
            ospf.Settings = @"router ospf 1001
 log-adjacency-changes
 area 0 authentication message-digest
 passive-interface default
 no passive-interface Vlan90
 no passive-interface Vlan91
 network 10.25.68.0 0.0.0.63 area 0".ToConfig();

            Assert.AreEqual(1, ospf.AuthenticaionDigestAreas.Count());
            Assert.AreEqual(0, ospf.AuthenticaionDigestAreas.ElementAt(0));
        }
    }
}