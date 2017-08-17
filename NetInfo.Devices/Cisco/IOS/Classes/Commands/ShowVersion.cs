using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  /// <summary>
  /// Using the Chain of Responsibility Pattern to help analyze
  /// show version.  Also using Null Object Design Pattern.
  ///
  /// Right now I just need ImageFileName so probably seems like an
  /// overkill.
  ///
  /// </summary>
  public class ShowVersion : BaseSetting {
    private IVersionAnalyzer one = new VersionAnalyzerOne();
    private IVersionAnalyzer nullAnalyzer = new NullVersionAnalyzer();
    private ShowVersionDetails details = new ShowVersionDetails();

    public ShowVersion(IEnumerable<string> settings) {
      this.Settings = settings;
      one.Next(nullAnalyzer);
      details = one.GetDetails(settings);
    }

    public string ImageFileName {
      get {
        return details.ImageName;
      }
    }

    public string Model {
      get {
        return details.Model;
      }
    }

    public class ShowVersionDetails {

      public string ImageName { get; set; }

      public string Model { get; set; }
    }

    public interface IVersionAnalyzer {

      void Next(IVersionAnalyzer next);

      ShowVersionDetails GetDetails(IEnumerable<string> commands);
    }

    public class VersionAnalyzerOne : BaseSetting, IVersionAnalyzer {
      private IVersionAnalyzer _next;

      public void Next(IVersionAnalyzer next) {
        this._next = next;
      }

      public ShowVersionDetails GetDetails(IEnumerable<string> commands) {
        this.Settings = commands;
        var image = GetSetting(new Regex(@"System image file is (""|&quot;)[\w-]+:\/?(?<image>.*)(""|&quot;)", RegexOptions.IgnoreCase));
        var model = GetSetting(new Regex(@"Cisco\s+(?<model>[\w-]+(\/\w+)?)\s+.*with [0-9]+", RegexOptions.IgnoreCase));
        return image != null || model != null
          ? new ShowVersionDetails {
            ImageName = image.Groups["image"].Value,
            Model = model == null ? string.Empty : model.Groups["model"].Value
          } : _next.GetDetails(commands);
      }
    }

    public class NullVersionAnalyzer : IVersionAnalyzer {
      private IVersionAnalyzer _next;

      public void Next(IVersionAnalyzer next) {
        this._next = next;
      }

      public ShowVersionDetails GetDetails(IEnumerable<string> commands) {
        return new ShowVersionDetails { ImageName = "Unable to process this configuration" };
      }
    }
  }
}