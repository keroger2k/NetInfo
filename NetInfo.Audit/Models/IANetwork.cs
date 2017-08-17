using System.Net;

namespace NetInfo.Audit.NMCI.Models {

  public class IANetwork {

    public string Site { get; set; }

    public Subnet IASubnet { get; set; }
  }

  public class Subnet {

    public IPAddress NetworkAddress { get; set; }

    public IPAddress NetworkMask { get; set; }
  }
}