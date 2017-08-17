using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using System.Collections.Generic;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  ///
  /// Validate test script has been correctly run
  ///
  /// </summary>
  public class IR000 : ISTIGItem {

    public IDevice Device { get; private set; }
    private ICollection<string> deviceCommandsFound;
    private Regex commandsRegex = new Regex(@"[\w]{4}-.*-.*-\d+#(?<command>show .*|write mem|dir all-filesystems|terminal length 0|remote command switch show version|!END-OF-TEST-SCRIPT)", RegexOptions.IgnoreCase);

    public IR000(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (IDevice)Device;
      this.deviceCommandsFound = device.AssetBlob.Configuration
        .Where(c => commandsRegex.Match(c).Success)
        .Select(c => commandsRegex.Match(c).Groups["command"].Value.Trim())
        .OrderBy(c => c).ToList();
      return COMMAND_LIST_V5_23.OrderBy(c => c).SequenceEqual(deviceCommandsFound);
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
            var missingCommands = COMMAND_LIST_V5_23.Except(this.deviceCommandsFound);
            var extraCommands = this.deviceCommandsFound.Except(COMMAND_LIST_V5_23);
            var dups = this.deviceCommandsFound.GroupBy(c => c).Where(c => c.Count() > 1).Select(c => c.Key).ToList();

            message = string.Format("<ul><h4>Commands Missing</h4><li>{0}</li> </ul><ul><h4>Unrecognized Commands</h4><li>{1}</li></ul><ul><h4>Duplicate Commands</h4><li>{2}</li><ul>",
                string.Join("</li><li>", missingCommands),
                string.Join("</li><li>", extraCommands),
                string.Join("</li><li>", dups));
            
        }
        return message;
    }

    private readonly string[] COMMAND_LIST_V5_23 = new string[] {"dir all-filesystems",
"!END-OF-TEST-SCRIPT",
"remote command switch show version",
"show access-list",
"show access-list | include Standard|Extended",
"show alias",
"show arp",
"show atm pvc",
"show atm traffic",
"show authentication session",
"show boot",
"show bootvar",
"show cdp interface",
"show cdp neighbors",
"show cdp neighbors detail",
"show clock",
"show config",
"show controllers",
"show controllers T1",
"show crypto engine connection active",
"show crypto key mypubkey rsa",
"show debugging",
"show diag",
"show diagbus",
"show dot1x all detail",
"show dot1x all summary",
"show env all",
"show environment all",
"show environment power",
"show environment status",
"show etherchannel summary",
"show fabric channel-counters",
"show hardware",
"show idprom all",
"show interface",
"show interface status",
"show interfaces trunk",
"show inventory",
"show inventory raw",
"show ip bgp",
"show ip bgp neighbor",
"show ip bgp summary",
"show ip cef",
"show ip eigrp neighbor",
"show ip eigrp topology",
"show ip interface",
"show ip interface brief | exclude unassigned",
"show ip nat statistics",
"show ip nat translations",
"show ip ospf",
"show ip ospf database",
"show ip ospf interface",
"show ip ospf neighbor",
"show ip ospf statistics",
"show ip route",
"show lacp neighbor",
"show line aux 0",
"show lldp interface",
"show lldp neighbors",
"show lldp neighbors detail",
"show log",
"show mac address-table",
"show mac-address-table",
"show module",
"show monitor session 1",
"show monitor session 2",
"show ntp association",
"show platform",
"show platform hardware capacity",
"show port security",
"show port-security",
"show power",
"show ppp multilink",
"show process cpu",
"show process cpu history",
"show process mem",
"show protocol",
"show radius server-group all",
"show rtr config",
"show running-config",
"show snmp",
"show snmp group",
"show snmp user",
"show spanning-tree active",
"show spanning-tree brief",
"show spanning-tree interface g0/1",
"show spanning-tree interface g0/2",
"show spanning-tree interface g1/1",
"show spanning-tree interface g1/2",
"show spanning-tree summary",
"show standby",
"show standby brief",
"show version",
"show vlan",
"show vlan brief",
"show vlan-switch",
"show vlan-switch brief",
"show vstack",
"show vtp counters",
"show vtp password",
"show vtp status",
"terminal length 0",
"write mem"};
  }
}