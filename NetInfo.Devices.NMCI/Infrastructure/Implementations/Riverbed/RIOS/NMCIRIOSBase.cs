using System;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;
using NetInfo.Devices.Riverbed.RIOS;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Riverbed.RIOS {

  public class NMCIRIOSBase : RIOSDevice, INMCIBaseDevice {

    public NMCIRIOSBase(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    public new IHostname Hostname {
      get {
        var host = new RIOSHostname(base.Hostname);
        return String.IsNullOrEmpty(base.Hostname) || host.Site == null ? null : host;
      }
    }
  }
}