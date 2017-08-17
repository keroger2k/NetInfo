using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices {

  public interface IConfigSetting {

    IEnumerable<string> Settings { get; set; }

    Regex GenericRegex { get; }
  }
}