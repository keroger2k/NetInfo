using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public class AdminSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^set admin .*$", RegexOptions.IgnoreCase); }
    }

    public string Format {
      get {
        var result = GetSetting(new Regex(@"^set admin format (\w+)$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public string Name {
      get {
        var result = GetSetting(new Regex(@"^set admin name ""([\w-]+)""$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public string Password {
      get {
        var result = GetSetting(new Regex(@"^set admin password ""?(.*[^""])""?$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public IEnumerable<User> Users {
      get {
        var result = GetSettings(new Regex(@"^set admin user ""(?<user>[\w-]+)"" password ""(?<password>.*?)""(\s+privilege\s+""(?<mode>[\w-]+)"")?$", RegexOptions.IgnoreCase));
        return result != null ? result.Select(c => new User {
          Name = c.Groups["user"].Value,
          Password = c.Groups["password"].Value,
          Privilege = c.Groups["mode"].Value
        }) : new List<User>();
      }
    }

    public IEnumerable<ManagerAddress> ManagerAddresses {
      get {
        var result = GetSettings(new Regex(@"^set admin manager-ip (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$", RegexOptions.IgnoreCase));
        return result != null ? result.Select(c => new ManagerAddress {
          Network = IPAddress.Parse(c.Groups[1].Value),
          Subnet = IPAddress.Parse(c.Groups[2].Value),
        }) : new List<ManagerAddress>();
      }
    }

    public string SysLocation {
      get {
        var result = GetSetting(new Regex(@"^set admin sys-location ""(\w+)""$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public IPAddress MailServerName {
      get {
        var result = GetSetting(new Regex(@"^set admin mail server-name ""(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})""$", RegexOptions.IgnoreCase));
        return result != null ? IPAddress.Parse(result.Groups[1].Value) : IPAddress.Parse("0.0.0.0");
      }
    }

    public int AuthTimeout {
      get {
        var result = GetSetting(new Regex(@"^set admin auth(\sweb)? timeout (\d+)$", RegexOptions.IgnoreCase));
        return result != null ? int.Parse(result.Groups[2].Value) : default(int);
      }
    }

    public string AuthServer {
      get {
        var result = GetSetting(new Regex(@"^set admin auth server ""(\w+)""$", RegexOptions.IgnoreCase));
        return result != null ? result.Groups[1].Value : string.Empty;
      }
    }

    public class User {

      public string Name { get; set; }

      public string Password { get; set; }

      public string Privilege { get; set; }
    }

    public class ManagerAddress : IEquatable<ManagerAddress> {

      public IPAddress Network { get; set; }

      public IPAddress Subnet { get; set; }

      public bool Equals(ManagerAddress other) {
        return other.Network.ToString() == this.Network.ToString() &&
          other.Subnet.ToString() == this.Subnet.ToString();
      }
    }
  }
}