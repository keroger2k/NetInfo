using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: The emergency administration account must be set to an appropriate authorization level to perform necessary administrative functions when the authentication server is not online.
    /// STIG ID:	NET0441     
    /// Rule ID:	SV-16261r5_rule
    /// Vuln ID:	V-15434       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class NET0441 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0441(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var distinctUsers = _device.UserSettings.Users.GroupBy(c => c.Username).Select(c => c.First()).FirstOrDefault();
            return distinctUsers != null && distinctUsers.PrivilegeLevel == 0;
        }
    }
}