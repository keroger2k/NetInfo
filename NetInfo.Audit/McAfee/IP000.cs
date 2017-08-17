using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using System.Collections.Generic;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate test scripts
  /// </summary>
  public class IP000 : ISTIGItem {

    public IDevice Device { get; private set; }
    private ICollection<string> deviceCommandsFound;
    private Regex commandsRegex = new Regex(@">\s+(?<command>show$|show .*|status|watchdog status|downloadstatus|guest-portal status)|(?<command>END-OF-TEST-SCRIPT)", RegexOptions.IgnoreCase);

    public IP000(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (IDevice)Device;
      this.deviceCommandsFound = device.AssetBlob.Configuration
        .Where(c => commandsRegex.Match(c).Success)
        .Select(c => commandsRegex.Match(c).Groups["command"].Value.Trim())
        .OrderBy(c => c).ToList();
      return COMMAND_LIST_V2_0.OrderBy(c => c).SequenceEqual(this.deviceCommandsFound);
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
            var missingCommands = COMMAND_LIST_V2_0.Except(this.deviceCommandsFound);
            var extraCommands = this.deviceCommandsFound.Except(COMMAND_LIST_V2_0);
            var dups = this.deviceCommandsFound.GroupBy(c => c).Where(c => c.Count() > 1).Select(c => c.Key).ToList();

            message = string.Format("Failing :: Commands Missing :: {0} :: Unrecognized Commands :: {1}  :: Duplicate Commands :: {2}",
                string.Join(", ", missingCommands),
                string.Join(", ", extraCommands),
                string.Join(", ", dups));
        }
        return message;
    }

    private readonly string[] COMMAND_LIST_V2_0 = new string[] {
"downloadstatus",
"END-OF-TEST-SCRIPT",
"guest-portal status",
"show",
"show acl stats",
"show arp spoof status",
"show auditlog status",
"show auxport status",
"show console timeout",
"show mem-usage",
"show mgmtport",
"show netstat",
"show sensordroppktevent status",
"show sshaccesscontrol status",
"show sshinactivetimeout",
"show ssl config",
"show ssl stats",
"show tacacs",
"show tcpipstats",
"show userconfigvolumedosthreshold tcp-fin inbound",
"show userconfigvolumedosthreshold tcp-rst inbound",
"show userconfigvolumedosthreshold tcp-syn inbound",
"show userconfigvolumedosthreshold tcp-syn-ack inbound",
"status",
"watchdog status"
};
  }
}