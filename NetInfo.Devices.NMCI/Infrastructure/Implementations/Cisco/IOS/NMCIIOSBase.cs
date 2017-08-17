using System;
using NetInfo.Devices.IOS;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Cisco.IOS {

  public class NMCIIOSBase : IOSDevice, INMCIBaseDevice {

    public NMCIIOSBase(IAssetBlob AssetBlob)
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