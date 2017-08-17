using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowInterfacesTrunk : BaseSetting {

    public enum InterfaceMode {
      on,
      desirable
    }

    public enum InterfaceEncapsulation {
      dot1q
    }

    public enum InterfaceStatus {
      trunking
    }

    public static readonly Regex ShowInterfaceStatusRegx =
      new Regex(@"(?<name>.*)\s+(?<mode>on|desirable)\s+(?<encapsulation>802.1q)\s+(?<status>trunking)\s+(?<nativeVlan>\d+)$", RegexOptions.IgnoreCase);

    public ShowInterfacesTrunk(IEnumerable<string> settings) {
      this.Settings = settings;
    }

    public IEnumerable<InterfacesTrunkingResult> Interfaces {
      get {
        return Settings
          .Where(c => ShowInterfaceStatusRegx.Match(c).Success)
          .Select(c => {
            var result = ShowInterfaceStatusRegx.Match(c);
            return new InterfacesTrunkingResult {
              Name = result.Groups["name"].Value.Trim(),
              Mode = (InterfaceMode)Enum.Parse(typeof(InterfaceMode), result.Groups["mode"].Value),
              Status = (InterfaceStatus)Enum.Parse(typeof(InterfaceStatus), result.Groups["status"].Value),
              NativeVlan = int.Parse(result.Groups["nativeVlan"].Value),
            };
          });
      }
    }

    public class InterfacesTrunkingResult {

      public string Name { get; set; }

      public InterfaceMode Mode { get; set; }

      public InterfaceEncapsulation Encapsulation { get; set; }

      public InterfaceStatus Status { get; set; }

      public int NativeVlan { get; set; }
    }
  }
}