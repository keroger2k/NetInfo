using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class FlowSettingsTests {
    private FlowSettings flowSettings;

    [SetUp]
    public void Init() {
      flowSettings = new FlowSettings();
      flowSettings.Settings = genericSettings;
    }

    private IEnumerable<string> genericSettings = @"
set flow path-mtu
set flow no-tcp-seq-check
set flow tcp-syn-check
set flow route tunnel prefer-reverse-route
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void can_correctly_parse_path_mtu() {
      Assert.True(flowSettings.PathMTUEnabled);
    }

    [Test]
    public void can_correctly_parse_no_tcp_seq_check() {
      Assert.True(flowSettings.NoTCPSequenceCheckEnabled);
    }

    [Test]
    public void can_correctly_parse_tcp_syn_check() {
      Assert.True(flowSettings.TCPSynCheckEnabled);
    }

    [Test]
    public void can_correctly_parse_prefer_reverse_route() {
      Assert.True(flowSettings.PreferReverseRouteEnabled);
    }
  }
}