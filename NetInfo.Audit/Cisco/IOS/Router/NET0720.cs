using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: Network devices must have TCP and UDP small servers disabled.
    /// STIG ID:	NET0720     
    /// Rule ID:	SV-3078r3_rule
    /// Vuln ID:	V-3078       
    /// Severity:	CAT III  
    /// Class:	Unclass
    /// </summary>
    public class NET0720 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0720(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !(_device.ServiceSettings.TcpSmallServers || _device.ServiceSettings.UdpSmallServers);
        }

    }
}