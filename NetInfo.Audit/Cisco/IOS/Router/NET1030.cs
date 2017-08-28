using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The running configuration must be synchronized with the startup configuration after 
    ///              changes have been made and implemented.
    /// STIG ID:	NET1030     
    /// Rule ID:	SV-3072r3_rule
    /// Vuln ID:	V-3072       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET1030 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;


        public NET1030(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.WriteMem.Success;
        }
    }
}