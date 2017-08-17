using System;
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
  ///   Network 1: 10.16.6.64/27
  ///   Network 2: 10.16.27.32/27
  ///   Network 3: 10.0.18.240/29
  ///   Network 4: 10.0.16.128/27
  ///   Network 5: 10.32.9.224/27
  ///
  /// DMZ:
  ///   Network 1: 10.16.101.15/32
  ///   Network 2: 10.16.101.16/32
  ///   Network 3: 10.7.248.25/32
  ///   Network 4: 10.7.248.26/32
  ///   Network 5: 10.32.251.224/32
  ///   Network 6: 10.32.251.225/32
  /// </summary>
  public class IP022 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<McAfeeDevice.Network> _sshNetworks;

    public IP022(INMCIMcAfeeDevice device, IEnumerable<McAfeeDevice.Network> sshNetworks) {
      this.Device = device;
      this._sshNetworks = sshNetworks;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      List<Tuple<string, byte>> t1 = _sshNetworks.Select(c => Tuple.Create<string, byte>(c.Address.ToString(), c.Netmask)).ToList();
      List<Tuple<string, byte>> t2 = device.SSHAccessControlNetworkList.Select(c => Tuple.Create<string, byte>(c.Address.ToString(), c.Netmask)).ToList();
      return t1.OrderBy(c => c.Item1).ThenBy(c => c.Item2).SequenceEqual(t2.OrderBy(c => c.Item1).ThenBy(c => c.Item2));
    }
  }
}