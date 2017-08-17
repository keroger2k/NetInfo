using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS.Commands {

  public class ShowBoot {
    private readonly IEnumerable<string> _output;

    public ShowBoot(IEnumerable<string> output) {
      this._output = output;
    }

    public IEnumerable<Image> Images {
      get {
        var images = new List<Image>();
        var partitionRgx = new Regex(@"Partition (?<number>\d+):", RegexOptions.IgnoreCase);
        var rawRegex = new Regex(@"^\s+(?<rawImage>rbt_(sh|ib|gw) (?<image>.*)) #.*", RegexOptions.IgnoreCase);
        for (var i = 0; i < _output.Count(); i++) {
          var m = partitionRgx.Match(_output.ElementAt(i));
          if (m.Success) {
            var img = new Image();
            img.Paritition = int.Parse(m.Groups["number"].Value);
            var r = rawRegex.Match(_output.ElementAt(i + 1));
            if (r.Success) {
              img.Name = r.Groups["image"].Value;
              img.RawImageCommand = r.Groups["rawImage"].Value;
            }
            images.Add(img);
          }
        }
        return images;
      }
    }

    public class Image {

      public int Paritition { get; set; }

      public string Name { get; set; }

      public string RawImageCommand { get; set; }
    }
  }
}