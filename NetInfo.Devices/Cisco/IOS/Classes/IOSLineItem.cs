using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.Cisco.IOS.Enums;

namespace NetInfo.Devices.Cisco.IOS {

  public class IOSLineItem {

    public string Name { get; set; }

    public LineType Type { get; set; }

    public IOSPassword Password { get; set; }

    public int AccessClass {
      get {
        var rgx = new Regex(@"\s+access-class (?<number>\d+)", RegexOptions.IgnoreCase);
        var r = Commands.FirstOrDefault(c => rgx.Match(c).Success);
        return r == null ? default(int) : int.Parse(rgx.Match(r).Groups["number"].Value);
      }
    }

    public IList<string> Commands { get; set; }
  }
}