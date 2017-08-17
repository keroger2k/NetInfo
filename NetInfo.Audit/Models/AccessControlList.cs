using System.Text.RegularExpressions;

namespace NetInfo.Audit.Models {

  public class AccessControlList {
    public static Regex AccessControlListRegex = new Regex(@"(?<name>.*)_V(?<majorVersion>\d+)(-(?<minorVersion>\d+))?", RegexOptions.IgnoreCase);

    public string FullName { get; set; }

    public string ShortName { get; set; }

    public int MajorVersion { get; set; }

    public int MinorVersion { get; set; }
  }
}