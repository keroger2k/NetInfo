using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:   In the event the authentication server is unavailable, the network device must have a single local account of last resort defined.
    /// STIG ID:	NET0440     
    /// Rule ID:	SV-15469r6_rule
    /// Vuln ID:	V-3966       
    /// Severity:	CAT II  Class:	Unclass
    /// 
    /// 
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:   Unauthorized accounts must not be configured for access to the network device.
    /// STIG ID:	NET0470     
    /// Rule ID:	SV-3058r5_rule
    /// Vuln ID:	V-3058      
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>    
    public class NET0440 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0440(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var distinctUsers = _device.UserSettings.Users.GroupBy(c => c.Username).Select(c => c.First());
            return distinctUsers.Count() <= 1;
        }
    }
}