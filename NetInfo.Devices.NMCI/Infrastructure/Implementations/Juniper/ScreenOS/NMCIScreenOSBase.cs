using System;
using NetInfo.Devices.Juniper.ScreenOS;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Juniper.ScreenOS {

  public class NMCIScreenOSBase : ScreenOSDevice, INMCIBaseDevice {

    public NMCIScreenOSBase(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    public new IHostname Hostname {
      get {
        var host = new ScreenOSHostname(base.Hostname);
        return String.IsNullOrEmpty(base.Hostname) || host.Site == null ? null : host;
      }
    }
  }
}