﻿using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate unset alg rtsp enable is configured
  /// </summary>
  public class VP122 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP122(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = ((INMCIScreenOSDevice)Device);
      return device.AlgSettings.Rtsp;
    }
  }
}