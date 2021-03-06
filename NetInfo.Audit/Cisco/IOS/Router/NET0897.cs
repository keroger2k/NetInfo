﻿using System.Linq;
using NetInfo.Devices.IOS;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network device must use its loopback or OOB management interface address as the source address when originating authentication services traffic.
    /// STIG ID:	NET0897     
    /// Rule ID:	SV-15336r3_rule
    /// Vuln ID:	V-14672       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0897 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;
        private Regex _sourceInterfaceRegex = new Regex(@"(?<sourceInterface>[Ll]oopback\d+)");

        public NET0897(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            string loopbackName = _sourceInterfaceRegex.Match(_device.IPSettings.TacacsSourceInterface).Groups["sourceInterface"].ToString();

            var srcInterface = _device.Interfaces.FirstOrDefault(c => c.ShortName.Equals(loopbackName, System.StringComparison.CurrentCultureIgnoreCase));

            return srcInterface != null && srcInterface.Address != null;
        }
    }
}