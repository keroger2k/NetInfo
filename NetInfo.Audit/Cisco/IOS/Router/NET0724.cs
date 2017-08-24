using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:   Network devices must have TCP Keep-Alives enabled for TCP sessions.
    /// STIG ID:	NET0724     
    /// Rule ID:	SV-5615r3_rule
    /// Vuln ID:	V-5615       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0724 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0724(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.ServiceSettings.TcpKeepalivesIn && _device.ServiceSettings.TcpKeepalivesOut;
        }
    }
}