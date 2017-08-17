using NetInfo.Devices.Brocade.BOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS.Classes {

  [TestFixture]
  public class AliasSettingsTests {

    [Test]
    public void alias_exec_object_should_correct_determine_value_for_given_key() {
      var setttings = new AliasSettings();
      setttings.Settings = @"alias Brocade-AS-Stack_int=uban-access-FCX648-stack-initial-configuration-v1_0_2
alias uban-access=uban-access-FCXABC-v1.0.1
alias Brocade-access-uplink-label=uban-access-FCX648-uplink-labels-v1_0_1
alias Brocade-access-vlan=uban-access-FCX648-vlans-v1_0_1".ToConfig();

      Assert.AreEqual("uban-access-FCX648-stack-initial-configuration-v1_0_2", setttings.GetValue("Brocade-AS-Stack_int"));
    }

    [Test]
    public void alias_exec_object_should_return_empty_string_for_uknown_key() {
      var setttings = new AliasSettings();
      setttings.Settings = @"alias Brocade-AS-Stack_int=uban-access-FCX648-stack-initial-configuration-v1_0_2
alias uban-access=uban-access-FCXABC-v1.0.1
alias Brocade-access-uplink-label=uban-access-FCX648-uplink-labels-v1_0_1
alias Brocade-access-vlan=uban-access-FCX648-vlans-v1_0_1".ToConfig();

      Assert.AreEqual(string.Empty, setttings.GetValue("DOES_NOT_EXIST"));
    }
  }
}