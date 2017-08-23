using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:   Network devices must use two or more authentication servers for the purpose of granting administrative access.
    /// STIG ID:    NET0433     
    /// Rule ID:    SV-16259r4_rule
    /// Vuln ID:    V-15432       
    /// Severity:   CAT II  Class:   Unclass
    /// </summary>
    public class NET0433 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0433(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.TacacsServer.Hosts.Count() > 1;
        }
    }
}