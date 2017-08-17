using System;
using System.Collections;
using System.Linq;
using System.Net;

namespace NetInfo.Devices.Infrastructure.Helpers {

  public class IPHelper {

    /// <summary>
    /// Verifies IP Address is Valid
    /// </summary>
    public static bool ip_inside_range(string subnet, string ipAddress) {
      string[] token = subnet.Split('/');
      string mask = token[1].Contains(".") ? token[1] : ip_cidrtomask(byte.Parse(token[1]));
      uint networkAddress = ip_iptouint(ip_networkAddress(token[0], mask));
      uint broadcastAddress = ip_iptouint(ip_broadcastAddress(token[0], mask));
      uint ip = ip_iptouint(ipAddress);
      return (networkAddress < ip && ip < broadcastAddress);
    }

    /// <summary>
    /// Verifies IP Address is Valid
    /// </summary>
    public static bool valid_ip(string address) {
      try {
        IPAddress test = IPAddress.Parse(address.Trim());
        return true;
      } catch (FormatException) {
        return false;
      }
    }

    /// <summary>
    /// Takes an IP address string value in dotted decial format and convert it to a uint value
    /// </summary>
    /// <param name="ip">String value of the IP address in dotted decimal format (i.e. 192.168.10.1)</param>
    /// <returns>Unsigned integer value that represents the IP address passed in</returns>
    public static uint ip_iptouint(string ip) {
      IPAddress i = IPAddress.Parse(ip.Trim());
      byte[] ipByteArray = i.GetAddressBytes();

      uint ipUint = (uint)ipByteArray[0] << 24;
      ipUint += (uint)ipByteArray[1] << 16;
      ipUint += (uint)ipByteArray[2] << 8;
      ipUint += (uint)ipByteArray[3];

      return ipUint;
    }

    /// <summary>
    /// Convert a uint IP address value to a string value in dotted decimal format
    /// </summary>
    /// <param name="ip">Unsigned integer value of IP address</param>
    /// <returns>String value of the IP address in dotted decimal format</returns>
    public static string ip_uinttoip(uint ip) {
      IPAddress i = new IPAddress(ip);
      string[] ipArray = i.ToString().Split('.');

      return ipArray[3] + "." + ipArray[2] + "." + ipArray[1] + "." + ipArray[0];
    }

    /// <summary>
    /// Converts the subnet mask into CIDR notation
    /// </summary>
    /// <param name="Mask">string value of network mask in dotted decimal notation (i.e. 255.255.255.0)</param>
    /// <returns>Byte value of the CIDR</returns>
    public static byte ip_masktocidr(string Mask) {
      uint mask = ip_iptouint(Mask);
      byte bits = 0;
      for (uint pointer = 0x80000000; (mask & pointer) != 0; pointer >>= 1) {
        bits++;
      }
      return bits;
    }

    /// <summary>
    /// Convert a subnetmask in CIDR notation to an unsigned integer value
    /// </summary>
    /// <param name="CIDR">Subnet mask in CIDR notation</param>
    /// <returns>Unsigned integer that represents the subnet mask</returns>
    public static uint ip_cidrtouintmask(byte CIDR) {
      return 0xFFFFFFFF << (32 - CIDR);
    }

    /// <summary>
    /// Convert a subnet mask in CIDR notation to a dotted decimal string value
    /// </summary>
    /// <param name="CIDR">Subnet mask in CIDR notation</param>
    /// <returns>String value of the subnet mask in dotted decimal notation</returns>
    public static string ip_cidrtomask(byte CIDR) {
      return ip_uinttoip(ip_cidrtouintmask(CIDR));
    }

    /// <summary>
    /// Checks to ensure the network address passed in is a valid subnet address
    /// and validates the network address is in the proper format
    /// </summary>
    /// <param name="Network">Network address in the format x.x.x.x/CIDR, x.x.x.x/x.x.x.x or x.x.x.x</param>
    /// <returns>True if it is a valid network address and false if it is not</returns>
    public static bool ip_is_valid(string Network) {
      if (Network.Contains("/")) {
        string[] token = Network.Split('/');
        if (token.Length != 2) return false;
        if (!(ip_is_valid(token[0]))) return false;
        if (token[1].Contains(".")) {
          if (!(ip_is_valid(token[1]))) return false;
        } else {
          byte fred = 0;
          if (!(byte.TryParse(token[1], out fred))) return false;
          if (fred > 32) return false;
        }
        uint ipAddr = ip_iptouint(token[0]);
        uint Mask = token[1].Contains(".") ? ip_iptouint(token[1]) : ip_cidrtouintmask(byte.Parse(token[1]));

        if (token[1].Contains(".")) {
          var tmp1 = String.Join("", token[1].Split('.')
            .Select(c => Convert.ToString(Int32.Parse(c), 2)));
          if (tmp1.Contains("01")) return false;
        }

        return ((ipAddr & Mask) == ipAddr);
      } else {
        string[] token = Network.Split('.');
        if (token.Length != 4) return false;
        byte fred = 0;
        foreach (string octet in token) {
          if (!(byte.TryParse(octet, out fred)))
            return false;
        }
        return true;
      }
    }

    /// <summary>
    /// Returns the number of IP address a given mask will support
    /// </summary>
    /// <param name="Mask">String value of the mask to use in dotted decimal format</param>
    /// <returns>Unsigned integer containing the number of IP addresses the passed in mask will support</returns>
    public static uint ip_size(string Mask) {
      return (uint)Math.Pow(2, 32 - (double)ip_masktocidr(Mask));
    }

    /// <summary>
    /// Returns the number of IP address a given CIDR will support
    /// </summary>
    /// <param name="CIDR">Byte value of the CIDR to be used</param>
    /// <returns>Unsigned integer containing the number of IP addresses the passed in CIDR will support</returns>
    public static uint ip_size(byte CIDR) {
      return (uint)Math.Pow(2, 32 - (double)CIDR);
    }

    /// <summary>
    /// Find the shortest prefix that fit both the first and second IP addresses passed in
    /// </summary>
    /// <param name="ip1">IP address in uint format of the first IP address</param>
    /// <param name="ip2">IP address in uint format of the second IP address</param>
    /// <returns>The shortest prefix length that will accomodate the two IP addresses passed in</returns>
    public static byte ip_get_prefix_length(uint ip1, uint ip2) {
      byte i = 0;
      for (uint pointer = 1; pointer != 0; pointer <<= 1) {
        if ((ip1 & pointer) == (ip2 & pointer)) {
          return i;
        } else {
          i++;
        }
      }
      return 32;
    }

    /// <summary>
    /// Returns the broadcast address of the network and subnet sent to it
    /// </summary>
    /// <param name="Network">An unsigned integer value of the network</param>
    /// <param name="Subnet">An unsigned integer value of the subnet mask</param>
    /// <returns>The unsigned IP address of the broadcast address for this subnet</returns>
    public static uint ip_broadcastAddress(uint Network, uint Subnet) {
      return (Network | (0xFFFFFFFF ^ Subnet));
    }

    /// <summary>
    /// Returns the broadcast address of the network and subnet sent to it
    /// </summary>
    /// <param name="Network">string value of the network</param>
    /// <param name="Subnet">string value of the subnet mask</param>
    /// <returns>String of the IP address of the broadcast address for this subnet</returns>
    public static string ip_broadcastAddress(string Network, string Subnet) {
      return ip_uinttoip(ip_broadcastAddress(ip_iptouint(Network), ip_iptouint(Subnet)));
    }

    /// <summary>
    /// Returns the Network Address as unsigned int
    /// </summary>
    /// <param name="Network">Address as unsigned int</param>
    /// <param name="Subnet">Subnet mask as unsigned int</param>
    /// <returns>Returns unsigned int of broadcast address</returns>
    public static uint ip_networkAddress(uint Network, uint Subnet) {
      return (Subnet & Network);
    }

    /// <summary>
    /// Returns the Network Address as a string
    /// </summary>
    /// <param name="Network">Address as a string</param>
    /// <param name="Subnet">Subnet mask as a string</param>
    /// <returns>Returns unsigned int of broadcast address</returns>
    public static string ip_networkAddress(string Network, string Subnet) {
      return ip_uinttoip(ip_networkAddress(ip_iptouint(Network), ip_iptouint(Subnet)));
    }

    /// <summary>
    /// Returns the starting and ending host addresses in the network provided
    /// </summary>
    /// <param name="Network"></param>
    /// <returns></returns>
    public static ArrayList ip_range(string Network) {
      ArrayList retValue = new ArrayList();
      string[] token = Network.Split('/');
      uint ipAddr = ip_iptouint(token[0]);
      retValue.Add(ip_uinttoip(ipAddr + 1));
      if (token[1].Contains(".")) {
        retValue.Add(ip_uinttoip(ipAddr + ip_size(token[1]) - 1));
      } else {
        retValue.Add(ip_uinttoip(ipAddr + ip_size(byte.Parse(token[1])) - 1));
      }
      return (retValue);
    }

    public static IPAddress subnet_to_inverse(IPAddress subnet) {
      return IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}",
        255 - subnet.GetAddressBytes()[0],
        255 - subnet.GetAddressBytes()[1],
        255 - subnet.GetAddressBytes()[2],
        255 - subnet.GetAddressBytes()[3]));
    }
  }
}