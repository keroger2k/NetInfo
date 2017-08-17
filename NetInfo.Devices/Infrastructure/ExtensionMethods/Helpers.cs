using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Infrastructure.Helpers;
using NetInfo.Devices.IOS;

namespace NetInfo.Devices.Infrastructure.ExtensionMethods {

  public static class Helpers {

    public static ICollection<IOSInterface> GetCoveredInterfaces(this IIOSDevice device) {
      var eigrpInterface = new List<IOSInterface>();
      var networks = device.EIGRP.Networks.ToList();

      var ipEnabledInterfaces = device.Interfaces.Where(c => c.Address != null);

      foreach (var net in networks) {
        foreach (var e in ipEnabledInterfaces) {
          if (IPHelper.ip_inside_range(FormatAddress(net), e.Address.NetworkAddress.ToString())) {
            eigrpInterface.Add(e);
          }
        }
      }
      return eigrpInterface
        .GroupBy(c => c.Address.NetworkAddress)
        .Select(c => c.First())
        .ToList();
    }

    private static string FormatAddress(EnhancedInteriorGatewayRoutingProtocol.Network network) {
      return string.Format("{0}/{1}", network.Subnet,
        IPHelper.ip_masktocidr(
          IPHelper.subnet_to_inverse(network.Inverse).ToString()).ToString());
    }
  }
}