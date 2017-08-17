using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes {

  public class IOSImage {
    private readonly Regex rgxImageName = new Regex(@".*(-mz.*|-universalk9.*)\.(?<major>\d\d)(?<minor>\d+)(-(?<release>\d+))?(?<letter>\w+)?.*\.", RegexOptions.IgnoreCase);
    private readonly Match bootMatch;
    private readonly string bootStatement;
    private bool parseSuccess;

    public IOSImage(string bootStatement) {
      this.bootStatement = bootStatement;
      this.bootMatch = rgxImageName.Match(bootStatement);
      this.parseSuccess = bootMatch.Success;
    }

    public bool K2Enabled {
      get {
        return this.bootStatement.Contains("k2");
      }
    }

    public bool K9Enabled {
      get {
        return this.bootStatement.Contains("k9");
      }
    }

    public int Major {
      get {
        return (parseSuccess) ? int.Parse(bootMatch.Groups["major"].Value) : default(int);
      }
    }

    public int Minor {
      get {
        return (parseSuccess) ? int.Parse(bootMatch.Groups["minor"].Value) : default(int);
      }
    }

    public int Release {
      get {
        string release = string.IsNullOrEmpty(bootMatch.Groups["release"].Value) ? "0" : bootMatch.Groups["release"].Value;
        return (parseSuccess) ? int.Parse(release) : default(int);
      }
    }

    public string Letter {
      get {
        return bootMatch.Groups["letter"].Value;
      }
    }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Release);
        }
    }
}