﻿using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  IP source routing must be disabled.
    /// STIG ID:	NET0770   
    /// Rule ID:	SV-3081r3_rule
    /// Vuln ID:	V-3081       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0770 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0770(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.SourceRoute;
        }
    }
}