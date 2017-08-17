using System.Net;

namespace NetInfo.Devices.Cisco.IOS {

  public class IOSRadiusServer {

    public IPAddress Host { get; set; }

    public int AuthPort { get; set; }

    public int AcctPort { get; set; }

    public IOSPassword Key { get; set; }
  }
}