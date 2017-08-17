using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS
{

    [TestFixture]
    public class ShowCdpNeighborsTests
    {

        [Test]
        public void should_correctly_count_the_number_of_cdp_enabled_interfaces()
        {
            var sv = new ShowCdpNeighbor(@"Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
PRLH-U00-TA-02.NMCI-ISF.com
                 Gig 6/4            134          S I      WS-C3550-2Gig 0/1
PRLH-U00-TA-01.NMCI-ISF.com
                 Gig 6/3            146          S I      WS-C3550-2Gig 0/1
PRLH-U00-SA-01.NMCI-ISF.com
                 Gig 6/15           167          S I      WS-C4503  Gig 1/1
PRLH-U00-OS-03.NMCI-ISF.com
                 Gig 6/2            125          S I      WS-C4503  Gig 2/1
PRLH-U00-OR-02.NMCI-ISF.com
                 Gig 6/1            151         R S I     WS-C6509  Gig 6/1".ToConfig());

            Assert.AreEqual(5, sv.Interfaces.Count());
        }

        [Test]
        public void should_correctly_account_for_different_interface_types()
        {
            var sv = new ShowCdpNeighbor(@"Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone, 
                  D - Remote, C - CVTA, M - Two-port Mac Relay 
Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
bSite01rt2.[removed].gov
                 Gig 0/0/1         131             R S I  C6832-X-L Ten 1/2
bSite01rt2.[removed].gov
                 Gig 0/0/0         145             R S I  C6832-X-L Ten 1/1
bSite01rt3.[removed].gov
                 Gig 0/0/3         171              R I   ASR1002-X Gig 0/0/1
bSite33rt1.[removed].gov
                 Ser 0/1/7:0       135            R B S I CISCO2901 Ser 0/2/0:0
bSite44rt1.[removed].gov
                 Ser 0/3/2:0       150             R S I  3825      Ser 0/0/0
bSite32rt1.[removed].gov
                 Ser 0/1/6:0       153            R B S I CISCO2901 Ser 0/2/0:0
bSite57rt1.[removed].gov
                 Ser 0/3/5:0       164             R S I  1841      Ser 0/0/0
bSite47rt1.[removed].gov
                 Ser 0/3/3:0       146             R S I  3825      Ser 0/2/0
bSite56rt1.[removed].gov
                 Ser 1/2/6:0       161            R B S I CISCO2901 Ser 0/2/0
bSite24rt1.[removed].gov
                 Ser 0/1/1:0       169             R S I  1841      Ser 0/0/0
bSite25rt1.[removed].gov
                 Ser 0/1/2:0       176             R S I  3825      Ser 0/2/0
bSite34rt1.[removed].gov
                 Ser 0/3/0:0       170             R S I  3825      Ser 0/0/0
bSite80rt1.[removed].gov
                 Ser 1/2/0:0       164            R B S I CISCO2901 Ser 0/0/0
bSite8310rt1.[removed].gov
                 Ser 1/2/4:0       160            R B S I CISCO2901 Ser 0/1/0:0
bSite82rt1.[removed].gov
                 Ser 1/2/3:0       149            R B S I CISCO2901 Ser 0/0/0
bSite3807rt1.[removed].gov
                 Ser 0/3/6:0       123            R B S I CISCO2901 Ser 0/1/0
bSite84rt1.[removed].gov
                 Ser 0/3/7:0       168            R B S I CISCO2901 Ser 0/0/0
bSite3103rt1.[removed].gov
                 Ser 1/2/5:0       155             R S I  ISR4331/K Ser 0/1/0:0
bSite31Rt1.[removed].gov
                 Ser 0/1/5:0       156             R S I  ISR4331/K Ser 0/1/0:0
bSite52rt1.[removed].gov
                 Ser 0/3/4:0       172            R B S I CISCO2901 Ser 0/3/0
bSite83rt1.[removed].gov
                 Ser 1/2/2:0       166            R B S I CISCO2901 Ser 0/1/0
bSite38Rt1.[removed].gov
                 Ser 0/3/1:0       162            R B S I CISCO2901 Ser 0/1/0
bSite85Rt1.[removed].gov
                 Ser 1/2/1:0       139            R B S I CISCO2901 Ser 0/2/0
bSite808rt1.[removed].gov
                 Gig 0/0/9         141             R S I  ISR4451-X Gig 0/0/1
bSite899rt1.[removed].gov
                 Ser 1/3/0:0       169             R S I  ISR4331/K Ser 0/1/0:0".ToConfig());

            Assert.AreEqual(25, sv.Interfaces.Count());
        }
        [Test]
        public void should_correctly_account_for_entries_that_are_only_one_line()
        {
            var sv = new ShowCdpNeighbor(@"Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone, 
                  D - Remote, C - CVTA, M - Two-port Mac Relay 
Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
bSite01rt3.[removed].gov
                 Ten 1/7           131              R I   ASR1002-X Gig 0/0/0
bSite01E4B-VPNrt1.[removed].gov
                 Ten 1/12          156            R B S I CISCO2901 Gig 0/1
bSite01rt1.[removed].gov
                 Ten 1/2           127              R I   ASR1004   Gig 0/0/1
bSite01rt1.[removed].gov
                 Ten 1/1           146              R I   ASR1004   Gig 0/0/0
bSite02rt1.[removed].gov
                 Ten 1/6           166             R S I  ISR4451-X Gig 0/0/1
Annex_OC192_2-2  Ten 1/10          125              R T   ONS-ML100 Gig 0
bSite110Rt1.[removed].gov
                 Ten 1/8           142              R I   ASR1004   Gig 0/0/1
bSite120rt1.[removed].gov
                 Ten 1/13          169            R B S I CISCO1921 Gig 0/1
bSite01sw10.[removed].gov
                 Ten 1/15          139              S I   WS-C3560- Gig 0/2
bSite01sw10.[removed].gov
                 Ten 1/16          139              S I   WS-C3560- Gig 0/1
bSite151rt1.[removed].gov
                 Ten 1/13          168             R S I  ISR4451-X Gig 0/0/2
bSite821rt1.[removed].gov
                 Ten 1/13          171            R B S I CISCO1921 Gig 0/0
bSite901rt2.[removed].gov
                 Ten 1/13          155             R S I  ISR4451-X Gig 2/0/1
bSite807rt2.[removed].gov
                 Ten 1/13          129            R B S I CISCO2901 Gig 0/0".ToConfig());

            Assert.AreEqual(@"Annex_OC192_2-2", sv.Interfaces.ElementAt(5).DestinationHostname);
            Assert.AreEqual(@"bSite01rt3.[removed].gov", sv.Interfaces.ElementAt(0).DestinationHostname);
        }
    }
}