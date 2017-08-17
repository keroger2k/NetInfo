using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices {

  public abstract class Device {

    public IAssetBlob AssetBlob { get; private set; }

    protected readonly IEnumerable<string> config;
    protected readonly int configLength;

    public Device(IAssetBlob AssetBlob) {
      this.AssetBlob = AssetBlob;
      this.config = AssetBlob.Configuration;
      this.configLength = config.Count();
    }

    protected virtual Match GetConfigurationSetting(Regex regex) {
      return config.Where(c => regex.Match(c).Success).Select(c => regex.Match(c)).FirstOrDefault();
    }

    protected virtual IEnumerable<Match> GetConfigurationMatches(Regex regex) {
      return config.Where(c => regex.Match(c).Success).Select(c => regex.Match(c));
    }

    protected virtual string GetConfigurationSetting(string line) {
      return config.FirstOrDefault(c => c.Equals(line, System.StringComparison.InvariantCultureIgnoreCase));
    }

    protected virtual TResult ParseSettings<TResult>() where TResult : IConfigSetting, new() {
      var settings = new List<string>();
      var obj = new TResult();

      for (int i = 0; i < configLength; i++) {
        if (obj.GenericRegex.Match(config.ElementAt(i)).Success) {
          settings.Add(config.ElementAt(i));
        }
      }

      obj.Settings = settings;
      return obj;
    }

    protected virtual IEnumerable<string> GetShowCommand(string command) {
      var commandOutput = new List<string>();
      for (var i = 0; i < configLength; i++) {
        var line = config.ElementAt(i);
        if (line.Contains(command)) {
          while (!config.ElementAt(i).Contains("!")) {
            commandOutput.Add(config.ElementAt(++i));
          }
          break;
        }
      }
      return commandOutput.Take(commandOutput.Count() - 1);
    }
  }
}