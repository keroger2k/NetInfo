using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network device must drop half-open TCP connections through filtering thresholds or timeout periods.
    /// STIG ID:	NET0965     
    /// Rule ID:	SV-15435r4_rule
    /// Vuln ID:	V-5645       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0965 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0965(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.IPSettings.TCP.SynWaitTime == 10;
        }
    }
}