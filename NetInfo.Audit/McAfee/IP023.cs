using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.McAfee;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate SSH access control listed devices
  ///
  /// Validate that the devices listed in the access control list are as follows:
  ///   Network 1: 138.156.126.32/27
  ///   Network 2: 138.156.125.224/28
  ///   Network 3: 205.110.246.32/27
  /// </summary>
  public class IP023 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<McAfeeDevice.Network> _sshNetworks;

    public IP023(INMCIMcAfeeDevice device, IEnumerable<McAfeeDevice.Network> sshNetworks) {
      this.Device = device;
      this._sshNetworks = sshNetworks;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return device.SSHAccessControlNetworkList.SequenceEqual(_sshNetworks);
    }
  }
}