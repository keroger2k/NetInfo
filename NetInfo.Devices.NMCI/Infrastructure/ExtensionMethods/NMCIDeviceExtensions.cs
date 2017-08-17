using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.Enums;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Devices.NMCI.Infrastructure.ExtensionMethods {

  public static class NMCIDeviceExtensions {
    private static readonly Dictionary<string, string> typeMap;

    static NMCIDeviceExtensions() {
      typeMap = new Dictionary<string, string>();
      typeMap["AG"] = "Inner";
      typeMap["AR"] = "Inner";
      typeMap["AS"] = "Inner";
      typeMap["BA"] = "Inner";
      typeMap["BE"] = "Outer";
      typeMap["BI"] = "Inner";
      typeMap["BO"] = "Outer";
      typeMap["CO"] = "Inner";
      typeMap["CR"] = "Inner";
      typeMap["CS"] = "Inner";
      typeMap["DA"] = "Inner";
      typeMap["DH"] = "Inner";
      typeMap["DM"] = "Inner";
      typeMap["DP"] = "Inner";
      typeMap["DR"] = "Inner";
      typeMap["DS"] = "Inner";
      typeMap["DZ"] = "Inner";
      typeMap["EA"] = "Inner";
      typeMap["EI"] = "Inner";
      typeMap["EO"] = "Outer";
      typeMap["ES"] = "Inner";
      typeMap["FW"] = "Inner";
      typeMap["GR"] = "Outer";
      typeMap["GW"] = "Inner";
      typeMap["IR"] = "Inner";
      typeMap["IR"] = "Inner";
      typeMap["IS"] = "Inner";
      typeMap["LC"] = "Inner";
      typeMap["LG"] = "Inner";
      typeMap["LM"] = "Inner";
      typeMap["LR"] = "Inner";
      typeMap["MS"] = "Inner";
      typeMap["OR"] = "Outer";
      typeMap["OS"] = "Outer";
      typeMap["PR"] = "Outer";
      typeMap["SA"] = "Outer";
      typeMap["SD"] = "Inner";
      typeMap["SL"] = "Inner";
      typeMap["SS"] = "Inner";
      typeMap["TA"] = "Other";
      typeMap["TE"] = "Inner";
      typeMap["TL"] = "Inner";
      typeMap["TR"] = "Inner";
      typeMap["VS"] = "Inner";
      typeMap["WB"] = "Inner";
      typeMap["WBRVP"] = "Inner";
      typeMap["WH"] = "Inner";
      typeMap["WI"] = "Inner";
      typeMap["WP"] = "Inner";
      typeMap["WR"] = "Inner";
      typeMap["WS"] = "Inner";
      typeMap["WX"] = "Inner";
    }

    /// <summary>
    /// Determine the zone based on the dictionary created in the static
    /// constructor.  Results could possibly be queried, but right
    /// now just look at the device and determine vlans being utilized.
    /// </summary>
    /// <param name="device">Cisco Type Device.</param>
    /// <returns></returns>
    public static ZoneType DetermineZoneForDevice(this INMCIIOSDevice device) {
      string zoneType = string.Empty;
      if (!typeMap.TryGetValue(device.Hostname.DeviceType, out zoneType)) {
        return ZoneType.Unknown;
      }

      if (new[] { "EDST" }.Contains(device.Hostname.Site) && !device.IsODMNEnabled) {
        return ZoneType.DMZ;
      }

      if (new[] { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "DL" }.Contains(device.Hostname.Zone)) {
        return ZoneType.DMZ;
      }

      if (zoneType.Equals("inner", StringComparison.OrdinalIgnoreCase)) {
        return ZoneType.Inner;
      }

      if (zoneType.Equals("other", StringComparison.OrdinalIgnoreCase) ||
          zoneType.Equals("outer", StringComparison.OrdinalIgnoreCase)) {
        if (device.Interfaces.Any(c => new int[] { 99, 30 }.Contains(c.Vlan) && c.Address != null)) {
          return ZoneType.Inner;
        }

        if (device.IsODMNEnabled) {
          return ZoneType.ODMN;
        }
      }
      return ZoneType.Outer;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="device">Cisco Type Device.</param>
    /// <returns></returns>
    public static PasswordZone DeterminePasswordZone(this INMCIIOSDevice device) {
      var zoneType = device.DetermineZoneForDevice();
      PasswordZone result = PasswordZone.Other;

      switch (zoneType) {
        case ZoneType.Inner:
          result = PasswordZone.Inner;
          break;

        case ZoneType.Outer:
          result = PasswordZone.Outer;
          break;

        case ZoneType.ODMN:
          result = PasswordZone.ODMN;
          break;

        case ZoneType.DMZ:
          result = PasswordZone.AHF_DMZ;
          break;

        case ZoneType.Unknown:
          result = PasswordZone.Other;
          break;

        default:
          result = PasswordZone.Other;
          break;
      }
      return result;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="device">Brocade Type Device</param>
    /// <returns></returns>
    public static ZoneType DetermineZoneForDevice(this INMCIBOSDevice device) {
      string type = string.Empty;
      if (typeMap.TryGetValue(device.Hostname.DeviceType, out type)) {
        return type.Equals("inner", StringComparison.OrdinalIgnoreCase) ? ZoneType.Inner : ZoneType.Outer;
      }
      return ZoneType.Unknown; ;
    }

    public static Regex SourceInterfaceRegex(this INMCIIOSDevice device) {
      IEnumerable<string> dslDevices = new string[] { "DA", "DH", "DM", "DP", "WR" };
      Regex sourceInterfaceRegex = null;
      var configuredInterfaces = device.Interfaces.ToList();

      var loopback = configuredInterfaces.FirstOrDefault(c => c.Type.Equals("loopback", StringComparison.OrdinalIgnoreCase));
      var vlan99 = configuredInterfaces.FirstOrDefault(c => c.Type.Equals("Vlan", StringComparison.OrdinalIgnoreCase) && c.Vlan == 99);
      var vlan30 = configuredInterfaces.FirstOrDefault(c => c.Type.Equals("Vlan", StringComparison.OrdinalIgnoreCase) && c.Vlan == 30);

      if (loopback != null && !dslDevices.Contains(device.Hostname.DeviceType)) {
        if (loopback.Address == null || loopback.Shutdown) {
          return null;
        } else {
          sourceInterfaceRegex = new Regex(@"Loopback0", RegexOptions.IgnoreCase);
        }
      } else if (vlan99 != null && vlan99.Address != null && !vlan99.Shutdown && vlan99.BridgeGroup == -1) {
        sourceInterfaceRegex = new Regex(@"Vlan99", RegexOptions.IgnoreCase);
      } else if (vlan30 != null && !dslDevices.Contains(device.Hostname.DeviceType)) {
        if (!vlan30.Shutdown && vlan30.Address == null) {
          return null;
        } else if (!vlan30.Shutdown) {
          sourceInterfaceRegex = new Regex(@"Vlan30", RegexOptions.IgnoreCase);
        }
      }

      if (sourceInterfaceRegex == null && dslDevices.Contains(device.Hostname.DeviceType)) {
        var bviInterfaces = configuredInterfaces.Where(c => new Regex(@"BVI\d+", RegexOptions.IgnoreCase).Match(c.ShortName).Success);
        var bviRegexString = string.Join("|", bviInterfaces.Where(d => !d.Shutdown && d.Address != null).Select(c => Regex.Escape(c.ShortName)));
        sourceInterfaceRegex = new Regex(bviRegexString, RegexOptions.IgnoreCase);
      } else if (sourceInterfaceRegex == null) {
        var anyInterfaceRegexString = string.Join("|", configuredInterfaces.Where(d => !d.Shutdown && d.Address != null).Select(c => Regex.Escape(c.ShortName)));
        if (!string.IsNullOrEmpty(anyInterfaceRegexString)) {
          sourceInterfaceRegex = new Regex(anyInterfaceRegexString, RegexOptions.IgnoreCase);
        }
      }
      return sourceInterfaceRegex;
    }

    public static IEnumerable<string> ToConfig(this string line) {
      return line.Split('\n')
        .Select(c => c.TrimEnd('\r', '\n'))
        .ToList();
    }
  }
}