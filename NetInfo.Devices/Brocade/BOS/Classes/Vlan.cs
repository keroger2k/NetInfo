using System.Collections.Generic;

namespace NetInfo.Devices.Brocade.BOS {

  public class Vlan {

    public Vlan() {
      this.Commands = new List<string>();
    }

    public int Number { get; set; }

    public string Name { get; set; }

    public IList<string> Commands { get; set; }
  }
}