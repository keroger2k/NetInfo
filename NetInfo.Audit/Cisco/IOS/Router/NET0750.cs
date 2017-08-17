using System;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: BOOTP services must be disabled.    /// STIG ID:	NET0750     
    /// Rule ID:	SV-3086r3_rule
    /// Vuln ID:	V-3086       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0750 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0750(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.BootPServer;
        }
    }
}