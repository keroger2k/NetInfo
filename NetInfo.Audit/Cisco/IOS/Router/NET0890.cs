using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must only allow SNMP access from addresses belonging to the management network.
    /// STIG ID:	NET0890     
    /// Rule ID:	SV-3021r3_rule
    /// Vuln ID:	V-3021       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0890 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        private int[] allowedAccessLists = new int[] { 60 };

        public NET0890(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return (_device.SNMPSettings.Groups != null && _device.SNMPSettings.Groups.Any(c => c.AccessGroup != 0) ?
              allowedAccessLists.Union(_device.SNMPSettings.Groups.Select(c => c.AccessGroup)).Count() == allowedAccessLists.Count() :
              _device.ShowSnmpUser.UserSettings != null && _device.ShowSnmpUser.UserSettings.All(c => c.StorageType != null && allowedAccessLists.Contains(c.StorageType.AccessGroup)));
        }
    }
}