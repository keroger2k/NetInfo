using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.IOS;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element’s auxiliary port must be disabled unless it is connected to a secured modem providing encryption and authentication.
    /// STIG ID:	NET1629     
    /// Rule ID:	SV-15446r2_rule
    /// Vuln ID:	V-7011       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET1629 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        private string[] requiredCommands = new string[] {
      " no exec",
      " transport output none",
    };

        public NET1629(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var auxLines = _device.Lines.SingleOrDefault(c => c.Type == LineType.AUX);
            return (auxLines != null) ?
              requiredCommands.Union(auxLines.Commands).Count() == auxLines.Commands.Count() &&
              !auxLines.Commands.Any(c => new Regex(@"transport input", RegexOptions.IgnoreCase).Match(c).Success) : true;
        }
    }
}