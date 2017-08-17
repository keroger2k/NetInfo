using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class JobSettings : BaseSetting, IConfigSetting {

    public IEnumerable<Job> Jobs {
      get {
        var jobs = new List<Job>();
        var jobRgx = new Regex(@"^\s*job (?<number>\d+).*$", RegexOptions.IgnoreCase);
        var jobNumberMatches = Settings.Where(c => jobRgx.Match(c).Success);
        var jobNumbers = jobNumberMatches.Select(c => int.Parse(jobRgx.Match(c).Groups["number"].Value)).Distinct();

        foreach (var jobNumber in jobNumbers) {
          var job = new Job { Number = jobNumber };
          var commentRgx = new Regex(string.Format(@"^\s*job (?<number>{0}) comment ""(?<comment>.*)""$", jobNumber), RegexOptions.IgnoreCase);
          var nameRgx = new Regex(string.Format(@"^\s*job (?<number>{0}) name ""(?<name>.*)""$", jobNumber), RegexOptions.IgnoreCase);
          var comment = GetSetting(commentRgx);
          var name = GetSetting(nameRgx);
          job.Comment = comment == null ? string.Empty : comment.Groups["comment"].Value;
          job.Name = name == null ? string.Empty : name.Groups["name"].Value;
          jobs.Add(job);
        }

        return jobs;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^\s*job (\d+).*$", RegexOptions.IgnoreCase); }
    }

    public class Job {

      public int Number { get; set; }

      public string Comment { get; set; }

      public string Name { get; set; }
    }
  }
}