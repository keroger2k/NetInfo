using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowVtpPasswordTests {

    private IEnumerable<string> config = @"VTP Password: C1dQeF!@r
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    private IEnumerable<string> configEncrypted = @"VTP Encrypted Password: 112B0B563A1739182B24
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void show_vtp_password_should_correctly_parse_password() {
      var show = new ShowVtpPassword(config);

      Assert.AreEqual("C1dQeF!@r", show.Password);
    }

    [Test]
    public void show_vtp_encrypted_password_should_correctly_parse_password() {
      var show = new ShowVtpPassword(configEncrypted);

      Assert.AreEqual("112B0B563A1739182B24", show.Password);
    }
  }
}