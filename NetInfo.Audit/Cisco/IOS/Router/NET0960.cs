using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  TCP intercept features must be provided by the network device by implementing a filter to rate 
    ///              limit and protect publicly accessible servers from any TCP SYN flood attacks from an outside network.
    /// STIG ID:	NET0960     
    /// Rule ID:	SV-3165r4_rule
    /// Vuln ID:	V-3165       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0960 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0960(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.IPSettings.TCP.InterceptionConnectionTimeout == 60 && _device.IPSettings.TCP.InterceptionWatchTimeout == 10;
        }
    }
}