using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowVtpStatusTests {

    private IEnumerable<string> config = @"VTP Version                     : running VTP1 (VTP2 capable)
Configuration Revision          : 0
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 9
VTP Operating Mode              : Transparent
VTP Domain Name                 : VSSD
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0xFA 0xC9 0x47 0xB3 0x66 0xA2 0x68 0xBD
Configuration last modified by 0.0.0.0 at 0-0-00 00:00:00
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void show_vtp_status_should_correctly_parse_domain_name() {
      var show = new ShowVtpStatus(config);

      Assert.AreEqual("VSSD", show.DomainName);
    }
  }
}