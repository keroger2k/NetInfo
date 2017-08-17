using System;
using NetInfo.Devices.McAfee;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.McAfee {

  public class NMCIMcAfeeBase : McAfeeDevice, INMCIBaseDevice {

    public NMCIMcAfeeBase(IAssetBlob AssetBlob)
      : base(AssetBlob) {
    }

    public new IHostname Hostname {
      get {
        var host = new McAfeeHostname(base.Hostname);
        return String.IsNullOrEmpty(base.Hostname) || host.Site == null ? null : host;
      }
    }
  }
}