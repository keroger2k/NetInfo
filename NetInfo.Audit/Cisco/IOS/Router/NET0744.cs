using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must be password protected.
    /// STIG ID:	NET0744     
    /// Rule ID:	SV-15313r3_rule
    /// Vuln ID:	V-14669       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0744 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0744(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.RCMD.IsRCPEnabled && !_device.IPSettings.RCMD.IsRSHEnabled;
        }

        public override string ToString()
        {
            string message = string.Empty;
            if (this.Compliant())
            {
                message = "Passing";
            }
            else
            {
                message = string.Join(", ", _device.IPSettings.RCMD.Settings);
            }
            return message;
        }
    }
}