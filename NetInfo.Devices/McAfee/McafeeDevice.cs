using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.McAfee {

  public class McAfeeDevice : Device, IMcafeeDevice {

    public McAfeeDevice(IAssetBlob AssetBlob) :
      base(AssetBlob) {
    }

    public string Hostname {
      get {
        var r = GetConfigurationSetting(new Regex(@"System\s+Name\s+:\s+(?<name>.*)", RegexOptions.IgnoreCase));
        return r == null ? string.Empty : r.Groups["name"].Value;
      }
    }

    public string MgmtLinkStatus {
      get {
        var r = GetConfigurationSetting(new Regex(@"MGMT\s+port\s+Link\s+Status\s+:\s+(.*,\s+)?(?<linkstatus>link ok|link up)", RegexOptions.IgnoreCase));
        return r == null ? string.Empty : r.Groups["linkstatus"].Value;
      }
    }

    public int ConsoleTimeout {
      get {
        var r = GetConfigurationSetting(new Regex(@"Console\s+timeout\s+:\s+(\d+) mins", RegexOptions.IgnoreCase));
        return r == null ? -1 : int.Parse(r.Groups[1].Value);
      }
    }

    public bool SSHAccessControl {
      get {
        var r = GetConfigurationSetting(new Regex(@"\[SSH AccessControl is Enabled\]", RegexOptions.IgnoreCase));
        return r != null;
      }
    }

    public bool AuxPortEnabled {
      get {
        var r = GetConfigurationSetting(new Regex(@"Aux\s+Port\s+:\s+Disabled", RegexOptions.IgnoreCase));
        return r == null;
      }
    }

    public bool AuditLoggingEnabled {
      get {
        var r = GetConfigurationSetting(new Regex(@"Audit\s+Logging\s+:\s+Enabled", RegexOptions.IgnoreCase));
        return r != null;
      }
    }

    public int SSHInactiveTimeout {
      get {
        var r = GetConfigurationSetting(new Regex(@"SSH\s+inactive\s+timeout\s+:\s+(\d+) sec", RegexOptions.IgnoreCase));
        return r == null ? -1 : int.Parse(r.Groups[1].Value);
      }
    }

    public IEnumerable<Network> SSHAccessControlNetworkList {
      get {
        var r = GetConfigurationMatches(new Regex(@"Network\s+(?<number>\d+):\s+(?<address>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\/(?<mask>\d+)", RegexOptions.IgnoreCase));
        return r == null ? new List<Network>() : r.Select(c => new Network {
          Number = int.Parse(c.Groups["number"].Value),
          Address = IPAddress.Parse(c.Groups["address"].Value),
          Netmask = byte.Parse(c.Groups["mask"].Value),
        });
      }
    }

    public ManagerConfig ManagerConfig {
      get {
        return ParseSettings<ManagerConfig>();
      }
    }

    public PeerManagerConfig PeerManagerConfig {
      get {
        return ParseSettings<PeerManagerConfig>();
      }
    }

    public SensorNetworkConfig SensorNetworkConfig {
      get {
        return ParseSettings<SensorNetworkConfig>();
      }
    }

    public SensorInfoConfig SensorInfoConfig {
      get {
        return ParseSettings<SensorInfoConfig>();
      }
    }

    protected override TResult ParseSettings<TResult>() {
      var settings = new List<string>();
      var obj = new TResult();

      for (int i = 0; i < configLength; i++) {
        if (obj.GenericRegex.Match(config.ElementAt(i)).Success) {
          while (!string.IsNullOrEmpty(config.ElementAt(i++))) {
            settings.Add(config.ElementAt(i));
          }
          break;
        }
      }

      obj.Settings = settings.Where(c => !string.IsNullOrEmpty(c));
      return obj;
    }

    public class Network : IEquatable<Network> {

      public int Number { get; set; }

      public IPAddress Address { get; set; }

      public byte Netmask { get; set; }

      public bool Equals(Network other) {
        return
          this.Address.Equals(other.Address) &&
          this.Netmask.Equals(other.Netmask);
      }
    }
  }
}