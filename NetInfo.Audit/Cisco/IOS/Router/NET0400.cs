using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: The network element must authenticate all IGP peers.
    /// STIG ID:	NET0400  
    /// Rule ID:	SV-15290r2_rule
    /// Vuln ID:	V-3034       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0400 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;
        public NET0400(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            bool result = true;
            if(_device.IsOSPFConfigured)
            {
                result = _device.OSPF.AuthenticaionDigestAreas.Any(c => c == 0);
            }
            return result;
        }
    }
}