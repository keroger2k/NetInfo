using NetInfo.Devices.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS.Classes {

  [TestFixture]
  public class WebSettingsTests {

    [Test]
    public void websettings_object_should_correctly_identify_when_sslv2_is_disabled() {
      var web = new WebSettings();
      web.Settings = new string[] {
        " no web ssl protocol sslv2"
      };

      Assert.False(web.SSL.V2Enabled);
    }

    [Test]
    public void websettings_object_should_correctly_identify_when_sslv2_is_enbled() {
      var web = new WebSettings();
      web.Settings = new string[] {
        " web ssl protocol sslv2"
      };

      Assert.True(web.SSL.V2Enabled);
    }

    [Test]
    public void websettings_object_should_correctly_identify_when_http_is_not_enabled() {
      var web = new WebSettings();
      web.Settings = new string[] {
        " no web web http enable"
      };

      Assert.False(web.Http.Enabled);
    }

    [Test]
    public void websettings_object_should_correctly_identify_when_http_is_enabled() {
      var web = new WebSettings();
      web.Settings = new string[] {
        " web http enable"
      };

      Assert.True(web.Http.Enabled);
    }
  }
}