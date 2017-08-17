using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS.Classes.Commands {

  public sealed class ShowInterface : BaseSetting {

    public ShowInterface(IEnumerable<string> settings) {
      this.Settings = settings;
    }

    public IEnumerable<Interface> Interfaces {
      get {
        var interfaceList = new List<Interface>();
        int settingCount = Settings.Count();
        for (int i = 0; i < settingCount; i++) {
          var m = Interface.InterfaceRgx.Match(Settings.ElementAt(i));
          if (m.Success) {
            var list = new List<string>();
            list.Add(Settings.ElementAt(i++));
            while (!Interface.InterfaceRgx.Match(Settings.ElementAt(i)).Success) {
              list.Add(Settings.ElementAt(i++));
              if (i == Settings.Count()) { break; }
            }
            interfaceList.Add(new Interface(list));
            i--;
          }
        }
        return interfaceList;
      }
    }

    public class Interface : BaseSetting {
      public static Regex InterfaceRgx = new Regex(@"^(?<interfaceName>GigabitEthernet|GigEthernetmgmt|16GigabitEthernet)(?<interfaceNumber>.*)\s+is (administratively )?(?<interfaceStatus>disabled|down|up), line protocol is (?<protocolStatus>up|down)", RegexOptions.IgnoreCase);

      public enum PMode {
        dual,
        tagged,
        untagged,
        UNKNOWN
      }

      public enum PState {
        BLOCKING,
        FORWARDING,
        DISABLED,
        UNKNOWN
      }

      public enum PType {
        Access,
        Trunk,
        Stack,
        UNKNOWN
      }

      public enum ProtocolStatus {
        up,
        down,
        disabled
      }

      public Interface(IEnumerable<string> settings) {
        this.Settings = settings;
        var m = InterfaceRgx.Match(settings.First());
        if (m.Success) {
          this.Name = m.Groups["interfaceName"].Value;
          this.Number = m.Groups["interfaceNumber"].Value;
          this.Enabled = m.Groups["interfaceStatus"].Value.Equals("up", StringComparison.OrdinalIgnoreCase);
          this.Protocol = (ProtocolStatus)Enum.Parse(typeof(ProtocolStatus), m.Groups["protocolStatus"].Value);
        }
      }

      public PortInfo PortInformation {
        get {
          if (Settings.FirstOrDefault(c => TrunkPort.PortMatchRegex.Match(c).Success) != null) {
            return new TrunkPort(Settings.FirstOrDefault(c => TrunkPort.PortMatchRegex.Match(c).Success));
          } else if (Settings.FirstOrDefault(c => AccessPort.PortMatchRegex.Match(c).Success) != null) {
            return new AccessPort(Settings.FirstOrDefault(c => AccessPort.PortMatchRegex.Match(c).Success));
          } else if (Settings.FirstOrDefault(c => StackPort.PortMatchRegex.Match(c).Success) != null) {
            return new StackPort(Settings.FirstOrDefault(c => StackPort.PortMatchRegex.Match(c).Success));
          } else {
            return new UnknownPort();
          }
        }
      }

      public string Name { get; private set; }

      public bool Enabled { get; private set; }

      public string Number { get; private set; }

      public ProtocolStatus Protocol { get; private set; }

      public abstract class PortInfo {
        protected readonly string _setting;

        public PortInfo(string setting) {
          this._setting = setting;
        }

        public abstract Regex GetRegex();

        public PState PortState {
          get {
            var ps = GetRegex().Match(_setting);
            return (ps == null) ? PState.UNKNOWN : (PState)Enum.Parse(typeof(PState), ps.Groups["state"].Value);
          }
        }

        public abstract PType PortType { get; }

        public PMode PortMode {
          get {
            var ps = GetRegex().Match(_setting);
            var isPortModeFound = string.IsNullOrEmpty(ps.Groups["portMode"].Value);
            if (!isPortModeFound) {
              if (ps.Groups["portMode"].Value.Contains(@"dual")) {
                return PMode.dual;
              } else if (ps.Groups["portMode"].Value.Equals(@"tagged")) {
                return PMode.tagged;
              } else if (ps.Groups["portMode"].Value.Equals(@"untagged")) {
                return PMode.untagged;
              }
            }
            return PMode.UNKNOWN;
          }
        }
      }

      public class TrunkPort : PortInfo {

        public TrunkPort(string setting)
          : base(setting) {
        }

        public static Regex PortMatchRegex {
          get {
            return new Regex(@"Member of (?<portType>\d+ L2 VLANs), port is (?<portMode>dual mode in Vlan \d+|untagged|tagged), port state is (?<state>\w+)", RegexOptions.IgnoreCase);
          }
        }

        public int NativeVlan {
          get {
            if (this.PortMode == PMode.dual) {
              var native = new Regex(@"Member of \d+ L2 VLANs, port is dual mode in Vlan (?<nativeVlan>\d+)", RegexOptions.IgnoreCase).Match(base._setting);
              if (native.Success) {
                return int.Parse(native.Groups["nativeVlan"].Value);
              }
            }
            return default(int);
          }
        }

        public override Regex GetRegex() {
          return PortMatchRegex;
        }

        public override PType PortType { get { return PType.Trunk; } }
      }

      public class AccessPort : PortInfo {

        public AccessPort(string setting)
          : base(setting) {
        }

        public static Regex PortMatchRegex {
          get {
            return new Regex(@"Member of (?<portType>L2 VLAN ID \d+), port is (?<portMode>dual mode in Vlan \d+|untagged|tagged), port state is (?<state>\w+)", RegexOptions.IgnoreCase);
          }
        }

        public override PType PortType { get { return PType.Access; } }

        public override Regex GetRegex() {
          return PortMatchRegex;
        }
      }

      public class StackPort : PortInfo {

        public StackPort(string setting)
          : base(setting) {
        }

        public static Regex PortMatchRegex {
          get {
            return new Regex(@"Stacking Port, port state is (?<state>\w+)", RegexOptions.IgnoreCase);
          }
        }

        public override PType PortType { get { return PType.Stack; } }

        public override Regex GetRegex() {
          return PortMatchRegex;
        }
      }

      public class UnknownPort : PortInfo {

        public UnknownPort()
          : base(string.Empty) {
        }

        public static Regex PortMatchRegex {
          get {
            return new Regex(@"--UNKOWN--", RegexOptions.IgnoreCase);
          }
        }

        public override PType PortType { get { return PType.UNKNOWN; } }

        public override Regex GetRegex() {
          return PortMatchRegex;
        }
      }
    }
  }
}