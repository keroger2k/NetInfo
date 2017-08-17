using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.Infrastructure.Enums;

namespace NetInfo.Devices {

  public class AssetBlob : IAssetBlob {
    private Regex deviceTypeRegex = new Regex(@"Devices?.*(?<type>McAfee|Cisco|Riverbed|Brocade|Netscreen).*", RegexOptions.IgnoreCase);

    //public long Id { get; set; }

    //public long AssetId { get; set; }

    //public DateTime Imported { get; set; }

    //public string Host { get; set; }

    public string Body { get; set; }

    public IEnumerable<string> Configuration {
      get {
        if (string.IsNullOrEmpty(this.Body)) {
          return new List<string>();
        }
        return this.Body
        .Split('\n')
        .Select(c => c.TrimEnd('\r', '\n'))
        .ToList();
      }
    }

    //private HeaderDeviceTypes _deviceType;

    //public HeaderDeviceTypes DeviceType {
    //  get {
    //    if (_deviceType == HeaderDeviceTypes.Unknown) {
    //      foreach (var line in Configuration) {
    //        var match = deviceTypeRegex.Match(line);
    //        if (match.Success) {
    //          _deviceType = (HeaderDeviceTypes)Enum.Parse(typeof(HeaderDeviceTypes), match.Groups["type"].Value);
    //          break;
    //        }
    //      }
    //    }
    //    return _deviceType;
    //  }
    //}
  }
}