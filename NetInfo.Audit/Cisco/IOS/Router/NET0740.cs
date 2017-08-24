using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: The network element must have HTTP service for administrative access disabled.
    /// STIG ID:	NET0740     
    /// Rule ID:	SV-41467r1_rule
    /// Vuln ID:	V-3085       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0740 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0740(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.HttpServer;
        }
    }
}