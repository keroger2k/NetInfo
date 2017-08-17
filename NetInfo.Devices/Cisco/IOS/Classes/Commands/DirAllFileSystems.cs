using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class DirAllFileSystems : BaseSetting {
    private readonly Regex rgxDisk0 = new Regex(@"Directory of disk0:\/", RegexOptions.IgnoreCase);
    private readonly Regex rgxSlot0 = new Regex(@"Directory of Slot0:\/", RegexOptions.IgnoreCase);
    private readonly Regex rgxEnd = new Regex(@"\d+ bytes total", RegexOptions.IgnoreCase);
    private readonly Regex rgxFiles = new Regex(@"^\s+\d+.*\s+(?=\S*$)(?<filename>.*)", RegexOptions.IgnoreCase);

    public DirAllFileSystems(IEnumerable<string> settings) {
      Settings = settings;
    }

    public IEnumerable<File> Slot0 {
      get {
        return GetFiles(rgxSlot0);
      }
    }

    public IEnumerable<File> Disk0 {
      get {
        return GetFiles(rgxDisk0);
      }
    }

    public class File {

      public string Name { get; set; }
    }

    private IEnumerable<File> GetFiles(Regex rgx) {
      List<File> list = null;
      for (int i = 0; i < Settings.Count(); i++) {
        var line = Settings.ElementAt(i);
        var m = rgx.Match(line);
        if (m.Success) {
          list = new List<File>();
          while (!rgxEnd.Match(line).Success) {
            var mFile = rgxFiles.Match(line);
            if (mFile.Success) {
              list.Add(new File { Name = mFile.Groups["filename"].Value });
            }
            line = Settings.ElementAt(i++);
          }
        }
      }
      return list;
    }
  }
}