using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate test scripts
  /// </summary>
  public class RB000 : ISTIGItem {

    public IDevice Device { get; private set; }
    private ICollection<string> deviceCommandsFound;
    private string[] COMMANDS;
    private Regex commandsRegex = new Regex(@"[\w]{4}-.*-.*-\d+.*(?<command>show .*|write mem|no cli session paging enable|END-OF-TEST-SCRIPT)", RegexOptions.IgnoreCase);
   
    public RB000(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (IDevice)Device;
      this.deviceCommandsFound = device.AssetBlob.Configuration
        .Where(c => commandsRegex.Match(c).Success)
        .Select(c => commandsRegex.Match(c).Groups["command"].Value.Trim())
        .OrderBy(c => c).ToList();
      return GetResults(device.Hostname.Substring(9, 2), this.deviceCommandsFound);
    }

    private bool GetResults(string prefix, IEnumerable<string> commands) {
      this.COMMANDS =  (new[] { "WX", "CM", "WC" }.Contains(prefix)) ? 
          COMMAND_LIST_V5_20 : COMMAND_WI_LIST_V5_20;
      return this.COMMANDS.OrderBy(c => c).SequenceEqual(commands);
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
            var missingCommands = this.COMMANDS.Except(this.deviceCommandsFound);
            var extraCommands = this.deviceCommandsFound.Except(this.COMMANDS);
            var dups = this.deviceCommandsFound.GroupBy(c => c).Where(c => c.Count() > 1).Select(c => c.Key).ToList();

            message = string.Format("Failing :: Commands Missing :: {0} :: Unrecognized Commands :: {1}  :: Duplicate Commands :: {2}",
                string.Join(", ", missingCommands),
                string.Join(", ", extraCommands),
                string.Join(", ", dups));

        }
        return message;
    }


    private readonly string[] COMMAND_WI_LIST_V5_20 = new string[] {
"END-OF-TEST-SCRIPT",
"no cli session paging enable",
"show boot",
"show clock",
"show config full",
"show hardware",
"show images",
"show info",
"show in-path neighbor",
"show in-path neighbor peers",
"show interface brief",
"show interface configured",
"show ip in-path route inpath0_0",
"show ip in-path route inpath0_1",
"show ip in-path route inpath1_0",
"show ip in-path route inpath1_1",
"show ip route",
"show licenses",
"show logging",
"show ntp",
"show port-label",
"show redirect peers",
"show running-config",
"show snmp",
"show ssh server",
"show stats alarm",
"show stats cpu",
"show tacacs",
"show version",
"show web",
"write mem"
    };

    private readonly string[] COMMAND_LIST_V5_20 = new string[] {
"END-OF-TEST-SCRIPT",
"no cli session paging enable",
"show boot",
"show clock",
"show config full",
"show connections all brief",
"show datastore",
"show hardware all",
"show images",
"show info",
"show interface brief",
"show interface configured",
"show ip in-path route inpath0_0",
"show ip in-path route inpath0_1",
"show ip in-path route inpath1_0",
"show ip in-path route inpath1_1",
"show ip in-path route inpath2_0",
"show ip route",
"show licenses",
"show logging",
"show ntp",
"show peers",
"show port-label",
"show proc",
"show protocol ssl expiring-certs",
"show protocol ssl peering ca",
"show protocol ssl peering certificate",
"show protocol ssl server",
"show raid diagram",
"show raid info",
"show running-config",
"show service",
"show snmp",
"show ssh server",
"show stats alarm",
"show stats connections month",
"show stats cpu",
"show stats traffic optimized bi-direction month",
"show stats traffic optimized lan-to-wan month",
"show stats traffic optimized wan-to-lan month",
"show stats traffic passthrough month",
"show tacacs",
"show version",
"show web",
"write mem"
};
  }
}