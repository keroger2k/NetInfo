using System.Collections.Generic;
using NetInfo.Devices.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class BOSWebManagementTests {

    [Test]
    public void sending_web_management_object_an_empty_settings_array_should_return_true_for_everything() {
      var wm = new WebManagement();
      wm.Settings = new List<string>();

      Assert.IsTrue(wm.HpTopTools);
      Assert.IsTrue(wm.Http);
    }

    [Test]
    public void sending_web_management_object_settings_for_http_should_return_false() {
      var wm = new WebManagement();
      wm.Settings = new string[] {
        "no web-management http"
      };

      Assert.IsTrue(wm.HpTopTools);
      Assert.IsFalse(wm.Http);
    }

    [Test]
    public void sending_web_management_object_settings_for_hp_top_tools_should_return_false() {
      var wm = new WebManagement();
      wm.Settings = new string[] {
        "no web-management hp-top-tools"
      };

      Assert.IsFalse(wm.HpTopTools);
      Assert.IsTrue(wm.Http);
    }
  }
}