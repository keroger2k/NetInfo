using System;
using NetInfo.Devices.IPS;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Cisco.IOS {

  public class NMCIIDSBase : IDSDevice, INMCIBaseDevice {

    public NMCIIDSBase(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    public new IHostname Hostname {
      get {
        var host = new IOSHostname(base.Hostname);
        return String.IsNullOrEmpty(base.Hostname) || host.Site == null ? null : host;
      }
    }
  }
}