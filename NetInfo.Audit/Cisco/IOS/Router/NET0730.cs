using NetInfo.Devices.IOS;
using System;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: The network device must log all access control lists (ACL) deny statements.
    /// STIG ID:	NET0730     
    /// Rule ID:	SV-15305r2_rule
    /// Vuln ID:	V-3078=9       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0730 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;


        public NET0730(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.Finger;
        }
    }
}