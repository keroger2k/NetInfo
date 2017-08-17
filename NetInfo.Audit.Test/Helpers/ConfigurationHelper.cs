using System.Collections.Generic;
using System.Linq;

namespace NetInfo.Audit.Tests.Helpers {

  public static class ConfigurationHelper {

    public static IEnumerable<string> ToConfig(this string line) {
      return line.Split('\n')
        .Select(c => c.TrimEnd('\r', '\n'))
        .ToList();
    }
  }
}