using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS
{

    public class CryptoSettings : BaseSetting, IConfigSetting
    {

        public PKISettings PKI
        {
            get
            {
                var pki = new PKISettings();
                pki.Settings = Settings.Where(c => pki.GenericRegex.Match(c).Success);
                return pki;
            }
        }

        public Regex GenericRegex
        {
            get { return new Regex(@"^crypto .*", RegexOptions.IgnoreCase); }
        }

        public class PKISettings : BaseSetting, IConfigSetting
        {

            public string TrustPoint
            {
                get
                {
                    var r = GetSetting(new Regex(@"crypto pki trustpoint (.*)", RegexOptions.IgnoreCase));
                    return r == null ? string.Empty : r.Groups[1].Value;
                }
            }

            public string CertificateChain
            {
                get
                {
                    var r = GetSetting(new Regex(@"crypto pki certificate chain (.*)", RegexOptions.IgnoreCase));
                    return r == null ? string.Empty : r.Groups[1].Value;
                }
            }

            public Regex GenericRegex
            {
                get { return new Regex(@"^crypto pki .*", RegexOptions.IgnoreCase); }
            }
        }
    }
}