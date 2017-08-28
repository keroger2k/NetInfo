using NetInfo.Devices.IOS;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The router must use its loopback or OOB management interface address as the source address when originating SNMP traffic.
    /// STIG ID:	NET0900     
    /// Rule ID:	SV-15346r2_rule
    /// Vuln ID:	V-14675       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0900 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;
        private Regex _sourceInterfaceRegex = new Regex(@"(?<sourceInterface>[Ll]oopback\d+)");

        public NET0900(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {

            string loopbackName = _sourceInterfaceRegex.Match(_device.SNMPSettings.TrapSource).Groups["sourceInterface"].ToString();

            var srcInterface = _device.Interfaces.FirstOrDefault(c => c.ShortName.Equals(loopbackName, System.StringComparison.CurrentCultureIgnoreCase));

            return srcInterface != null && srcInterface.Address != null;
        }
    }
}