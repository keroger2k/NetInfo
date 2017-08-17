using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands
{

    public class ShowInventory : BaseSetting
    {

        public ShowInventory(IEnumerable<string> settings)
        {
            this.Settings = settings;
        }

        public string Model
        {
            get
            {
                if (Settings.Any())
                {
                    var rgx = new Regex(@"^PID:\s+(?<model>[\w-]+(\/\w+)?)\s+,\s+VID:", RegexOptions.IgnoreCase);
                    var failedRegex = new Regex(@"Authorization failed", RegexOptions.IgnoreCase);
                    if (failedRegex.Match(Settings.ElementAt(0)).Success)
                    {
                        return "Autorization Failed.";
                    }
                    var m = rgx.Match(Settings.ElementAt(1));

                    if (m.Success)
                    {
                        return m.Groups["model"].Value;
                    }
                }
                return string.Empty;
            }
        }
    }
}