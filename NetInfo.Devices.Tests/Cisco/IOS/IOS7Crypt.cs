using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class IOS7CryptTests {

    [Test]
    public void can_successfully_decrypt_passwords() {
      string hash = "014647321A1A374E731A1D254E264744";
      string password = "5!V!qQ!263L7C06";

      var a = IOS7Crypt.Decrypt(hash);
      Assert.AreEqual(password, a);
    }
  }
}