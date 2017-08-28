using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must log all messages except debugging and send all log data to a syslog server.
    /// STIG ID:	NET1021     
    /// Rule ID:	SV-15476r2_rule
    /// Vuln ID:	V-4584       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET1021 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1021(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            if (!_device.SyslogSettings.isLoggingTrapEnabled) return false;
            if (!_device.SyslogSettings.Servers.Any()) return false;
            return new string[] { "informational" }.Contains(_device.SyslogSettings.TrapLevel);
        }
    }
}