using NetInfo.Devices.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS.Classes {

  [TestFixture]
  internal class BOSIPSSHSettingsTests {

    [Test]
    public void creating_new_ip_ssh_setting_object_with_no_ide_time_setting_will_return_zero() {
      var result = new IPSSHSettings();
      result.Settings = new string[] { "" };

      Assert.AreEqual(0, result.IdleTime);
    }

    [Test]
    public void creating_new_ip_ssh_setting_object_with_no_authentication_retries_setting_will_return_zero() {
      var result = new IPSSHSettings();
      result.Settings = new string[] { "" };

      Assert.AreEqual(3, result.AuthenticationRetries);
    }

    [Test]
    public void creating_new_ip_ssh_setting_object_with_no_timeout_setting_will_return_zero() {
      var result = new IPSSHSettings();
      result.Settings = new string[] { "" };

      Assert.AreEqual(0, result.Timeout);
    }

    [Test]
    public void creating_new_ip_ssh_setting_object_with_timeout_setting_will_return_correct_value() {
      var result = new IPSSHSettings();
      result.Settings = new string[] { "ip ssh timeout 60" };

      Assert.AreEqual(60, result.Timeout);
    }

    [Test]
    public void creating_new_ip_ssh_setting_object_with_authentication_retries_setting_will_return_correct_value() {
      var result = new IPSSHSettings();
      result.Settings = new string[] { "ip ssh authentication-retries 10" };

      Assert.AreEqual(10, result.AuthenticationRetries);
    }

    [Test]
    public void creating_new_ip_ssh_setting_object_with_idle_time_setting_will_return_correct_value() {
      var result = new IPSSHSettings();
      result.Settings = new string[] { "ip ssh idle-time 3" };

      Assert.AreEqual(3, result.IdleTime);
    }

    [Test]
    public void creating_new_ip_ssh_setting_object_with_access_group_applied_will_return_correct_value() {
      var result = new IPSSHSettings();
      result.Settings = new string[] { "ssh access-group 99" };

      Assert.AreEqual(99, result.AccessGroup);
    }
  }
}