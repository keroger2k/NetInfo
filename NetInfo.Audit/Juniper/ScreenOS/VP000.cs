using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using System.Collections.Generic;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate test scripts
  /// </summary>
  public class VP000 : ISTIGItem {

    public IDevice Device { get; private set; }
    private ICollection<string> deviceCommandsFound;
    private Regex commandsRegex = new Regex(@"->\s+(?<command>get license|get nsrp|get pki x509 list cert|get route|get config|set console page 20|set console page 0|get system|get chassis|set console timeout 3|save)|(?<command>^END-OF-TEST-SCRIPT)|(->\s+(?<command>END-OF-TEST-SCRIPT))", RegexOptions.IgnoreCase);

    public VP000(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      string[] tmpCommandList = null;
      bool isUsmc = false;

      this.deviceCommandsFound = device.AssetBlob.Configuration
        .Where(c => commandsRegex.Match(c).Success)
        .Select(c => commandsRegex.Match(c).Groups["command"].Value.Trim())
        .OrderBy(c => c).ToList();
      if (device.Hostname != null) {
        isUsmc = device.Hostname.Name.Substring(0, 2).Equals("MC");
        tmpCommandList = COMMAND_LIST_V1_4.Where(c => !c.Equals("set console timeout 3")).ToArray();
      }

      if (isUsmc) {
          return COMMAND_LIST_V1_4.OrderBy(c => c).SequenceEqual(this.deviceCommandsFound) ? true :
          tmpCommandList.OrderBy(c => c).SequenceEqual(this.deviceCommandsFound);
      }

      return COMMAND_LIST_V1_4.OrderBy(c => c).SequenceEqual(this.deviceCommandsFound);
    }

    public override string ToString()
    {
        string message = string.Empty;
        if (this.Compliant())
        {
            message = "Passing:  All required commands have been found.";
        }
        else
        {
            var missingCommands = COMMAND_LIST_V1_4.Except(this.deviceCommandsFound);
            var extraCommands = this.deviceCommandsFound.Except(COMMAND_LIST_V1_4);
            var dups = this.deviceCommandsFound.GroupBy(c => c).Where(c => c.Count() > 1).Select(c => c.Key).ToList();
                
            message = string.Format("Failing :: Commands Missing :: {0} :: Unrecognized Commands :: {1}  :: Duplicate Commands :: {2}", 
                string.Join(", ", missingCommands),
                string.Join(", ", extraCommands),
                string.Join(", ", dups));
        }
        return message;
    }


    private readonly string[] COMMAND_LIST_V1_4 = new string[] {
"END-OF-TEST-SCRIPT",
"get chassis",
"get config",
"get license",
"get nsrp",
"get pki x509 list cert",
"get route",
"get system",
"save",
"set console page 0",
"set console timeout 3",
"set console page 20"
};
  }
}