using System.Net;

namespace NetInfo.Audit.Models {

  public class CircuitInformation {

    public string Site { get; set; }

    public string CircuitId { get; set; }

    public string Bandwidth { get; set; }

    public int? DeliveryTypeId { get; set; }

    private IPAddress _siteAddress;

    public IPAddress SiteAddress {
      get {
        return _siteAddress == null ? IPAddress.Parse("0.0.0.0") : _siteAddress;
      }
      set {
        if (value != null) {
          _siteAddress = value;
        } else {
          _siteAddress = IPAddress.Parse("0.0.0.0");
        }
      }
    }
  }
}