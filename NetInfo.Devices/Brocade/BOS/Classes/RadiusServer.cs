using System.Net;

namespace NetInfo.Devices.Brocade.BOS {

  public class RadiusServer {

    public IPAddress Host { get; set; }

    public int AuthPort { get; set; }

    public int AcctPort { get; set; }

    public Password Key { get; set; }
  }
}