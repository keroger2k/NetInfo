using NetInfo.Devices.IOS;
using System;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network device must use SNMP Version 3 Security Model with FIPS 140-2 validated cryptography for any SNMP agent configured on the device.
    /// STIG ID:	NET1660     
    /// Rule ID:	SV-3196r4_rule
    /// Vuln ID:	V-3196       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class NET1660 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;
        private readonly string[] approvedProtocols = new[] { "DES", "3DES", "AES", "AES256", "AES192", "AES128" };

        public NET1660(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            bool result1 = false;
            bool result2 = false;

            if (_device.ShowSnmpUser.UserSettings != null && _device.ShowSnmpUser.UserSettings.Any(c => !string.IsNullOrEmpty(c.PrivacyProtocol)))
            {
                result1 = _device.ShowSnmpUser.UserSettings.All(c => approvedProtocols.Contains(c.PrivacyProtocol));
            }
            else
            {
                result1 =
                  _device.SNMPSettings.Servers.Any() &&
                  _device.SNMPSettings.Servers.All(c => c.VersionKeyword.Equals("priv", System.StringComparison.OrdinalIgnoreCase)) &&
                  _device.SNMPSettings.Groups != null &&
                  _device.SNMPSettings.Groups.All(c => c.VerionKeyword.Equals("priv", System.StringComparison.OrdinalIgnoreCase));
            }

            if (_device.ShowSnmpUser.UserSettings != null)
            {
                result2 = _device.ShowSnmpUser.UserSettings
                  .All(c => c.AuthenticationProtocol != null && (c.AuthenticationProtocol.Equals("sha", StringComparison.OrdinalIgnoreCase) ||
                       c.AuthenticationProtocol.Equals("md5", StringComparison.OrdinalIgnoreCase)));
            }

            return result1 && result2;
        }
    }
}