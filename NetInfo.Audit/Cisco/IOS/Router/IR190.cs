﻿using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  
    /// STIG ID:	     
    /// Rule ID:	SV-
    /// Vuln ID:	V-       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class IR190 : ISTIGItem
    {
        private IIOSDevice _device;

        public IR190(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.SecureHttpServer;
        }
    }
}