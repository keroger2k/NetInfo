using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure all disabled ports are assigned to VLAN2
  ///
  /// All disabled switchports must be assigned to vlan2. There are no exceptions.  See "show interfaces brief" output.
  /// </summary>
  public class BS015 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string[] flaggedInterfaces = new string[] { "mgmt1", "ve99" };

    public BS015(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var a = device.ShowInterfaceBrief.Interfaces.Where(c => c.Link == ShowInterfaceBrief.LinkStatus.Disable);
      var b = a.Where(c => !flaggedInterfaces.Contains(c.Port));
      return b.All(c => c.Pvid.Equals("2"));
    }
  }
}