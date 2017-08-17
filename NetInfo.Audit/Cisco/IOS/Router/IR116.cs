using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate a VTP domain and a VTP password are configured
  ///
  /// View the show vtp status command output to determine if the VTP domain name is configured,
  /// View the show vtp password command output to see the VTP password
  ///
  /// Note: the show vtp password command is only valid on devices where the VTP perameters can be configured in configuration mode.
  /// Exceptions: GW (voice gateway) device template does not configure VTP."
  /// </summary>
  public class IR116 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR116(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Hostname.DeviceType.Equals("GW") ? true :
              device.ShowVtpPassword.IsPasswordCommandSupported ?
                !string.IsNullOrEmpty(device.ShowVtpStatus.DomainName) && !string.IsNullOrEmpty(device.ShowVtpPassword.Password) :
                !string.IsNullOrEmpty(device.ShowVtpStatus.DomainName);
    }
  }
}