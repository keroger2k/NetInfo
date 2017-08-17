using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using System.Collections.Generic;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  ///
  /// Validate test script has been correctly run
  ///
  /// </summary>
  public class BS000 : ISTIGItem {

    public IDevice Device { get; private set; }
    private ICollection<string> deviceCommandsFound;
    private Regex commandsRegex = new Regex(@"SSH@[\w]{4}-.*-.*-\d+#(?<command>show .*|write memory|skip-page-display|!END-OF-TEST-SCRIPT)", RegexOptions.IgnoreCase);

    public BS000(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (IDevice)Device;
      this.deviceCommandsFound = device.AssetBlob.Configuration
        .Where(c => commandsRegex.Match(c).Success)
        .Select(c => commandsRegex.Match(c).Groups["command"].Value.Trim())
        .OrderBy(c => c).ToList();
      return COMMAND_LIST_V1_0_6.OrderBy(c => c).SequenceEqual(this.deviceCommandsFound);
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
            var missingCommands = COMMAND_LIST_V1_0_6.Except(this.deviceCommandsFound);
            var extraCommands = this.deviceCommandsFound.Except(COMMAND_LIST_V1_0_6);
            var dups = this.deviceCommandsFound.GroupBy(c => c).Where(c => c.Count() > 1).Select(c => c.Key).ToList();

            message = string.Format("Failing :: Commands Missing :: {0} :: Unrecognized Commands :: {1}  :: Duplicate Commands :: {2}",
                string.Join(", ", missingCommands),
                string.Join(", ", extraCommands),
                string.Join(", ", dups));
        }
        return message;
    }

    private readonly string[] COMMAND_LIST_V1_0_6 = new string[] {
"!END-OF-TEST-SCRIPT",
"show aaa",
"show arp",
"show auth-mac-address unauthorized-mac",
"show auth-mac-addresses",
"show boot-preference",
"show chassis",
"show clock",
"show config",
"show configuration | include alias",
"show cpu",
"show debug",
"show default value",
"show dot1x",
"show dot1x mac-sessions",
"show files",
"show flash",
"show interface | in line | name",
"show interfaces",
"show interfaces brief",
"show interfaces brief | exclude unassigned",
"show interfaces brief | inc Yes",
"show interfaces stack-ports",
"show ip access-list | include Standard|Extended",
"show ip access-lists",
"show ip interface",
"show ip interface ve 99",
"show ip route",
"show link-aggregate",
"show lldp local-info",
"show lldp neighbors",
"show lldp neighbors detail",
"show logging",
"show mac-address all",
"show memory",
"show memory tcp",
"show module",
"show monitor",
"show port security",
"show port security mac",
"show port security statistics",
"show process cpu",
"show running-config",
"show snmp",
"show snmp group",
"show snmp server",
"show snmp user",
"show sntp association",
"show sntp status",
"show span detail",
"show span detail | include ACTIVE",
"show span detail | include not configured",
"show stack detail",
"show trunk",
"show users",
"show version",
"show vlan",
"show vlan | include Name",
"show vlan | include Tagged",
"show vlan brief",
"skip-page-display",
"write memory"
};
  }
}