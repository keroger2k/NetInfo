using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Perimeter Router Security Technical Implementation Guide Cisco :: Release: 26 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Each eBGP neighbor must be authenticated with a unique password.
    /// STIG ID:	NET0412     
    /// Rule ID:	SV-15300r2_rule
    /// Vuln ID:	V-14666       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0412 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0412(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.IsBGPConfigured &&
              _device.BGP.Neighbors.Count() == _device.BGP.Neighbors.Select(c => c.Password).Distinct().Count();
        }

        public override string ToString()
        {
            var message = string.Empty;
            if (this.Compliant())
            {
                message = "Passing: All eBGP neighbor relationships are configued with unique MD5 keys.";
            }
            else
            {
                var z = _device.BGP.Neighbors.Where(c => string.IsNullOrEmpty(c.Password));
                message = string.Format("Failing: Neighbors without passwords {0}",
                  string.Join(", ", z.Select(c => c.Address.ToString())));
            }
            return message;
        }
    }
}