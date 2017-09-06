using NetInfo.Core.Models;
using System.Collections.Generic;
using System.Net;

namespace NetInfo.Core.Helpers
{

    public class IPManagement : IPHelper
    {
        /// <summary>
        /// Find all the networks that reside between the two IP addresses passed in
        /// </summary>
        /// <param name="ip">The starting IP address of the range</param>
        /// <param name="endip">The ending IP address of the range</param>
        /// <returns>A List<Network> with all the networks that reside between the two IP addresses passed in</returns>
        public List<Network> ip_range_to_prefix(uint ip, uint endip)
        {
            var prefix = new List<Network>();
            while (ip <= endip)
            {   // while the IP address is less than or equal to the ending IP address loop
                int nbits = 0;
                if (ip == 0)
                    nbits = 32;
                else
                    for (uint pointer = 1; (ip & pointer) == 0; pointer <<= 1)
                        nbits++;        // find the number of zero bits at the end of the IP address
                uint bitmask = nbits == 0 ? 0 : 0xFFFFFFFF >> (32 - nbits);  // Create a bit mask to flip those ending zeros into 1s
                uint current = 0;
                do
                {
                    current = ip | bitmask;     // Bitwise OR the IP address and bit mask to get the largest IP address possible with the current IP addres
                    bitmask >>= 1;              // Move the bit mask to one bit smaller
                } while (current > endip);      // Until the current largest IP address possible it more than the ending IP address then loop again
                var row = new Network();
                row.NetworkAddress = IPAddress.Parse(ip_uinttoip(ip)); // convert the IP address to a string value and store it in the datarow
                row.NetworkMask = IPAddress.Parse(ip_cidrtomask((byte)(32 - ip_get_prefix_length(ip, current))));  // Find the largest prefix length that will fit both the IP address and the current largest IP address and convert that to a string value
                prefix.Add(row);           // Add this enry to the datatable
                ip = current + 1;               // Set the IP address to the current largest IP address plus 1
                if (current == 0xffffffff)
                {     // If we have reached the end of the IP address block then exit
                    return prefix;
                }
            } // while (ip <= endip)
            return prefix;
        }

        /// <summary>
        /// Splits a network into two networks (will return null if network mask is /32 or /255.255.255.255)
        /// </summary>
        /// <param name="in_network">String object in the form of IPsubnet/CIDR or IPsubnet/subnetMask</param>
        /// <returns>List of IPM_NetowrkMaster objects of the split networks</returns>
        public List<Network> ip_split_network(string in_network)
        {
            var nets = new List<Network>();
            string[] token = in_network.Split('/');
            byte CIDR = 0;
            if (token[1].Contains("."))
            {
                CIDR = ip_masktocidr(token[1]);
            }
            else
            {
                CIDR = byte.Parse(token[1]);
            }
            if (CIDR == 32)
            {
                return null;
            }
            CIDR++;
            nets.Add(new Network
            {
                NetworkAddress = IPAddress.Parse(token[0]),
                NetworkMask = IPAddress.Parse(ip_cidrtomask(CIDR))
            });
            nets.Add(new Network
            {
                NetworkAddress = IPAddress.Parse(ip_uinttoip(ip_iptouint(token[0]) | (0x80000000 >> (CIDR - 1)))),
                NetworkMask = IPAddress.Parse(ip_cidrtomask(CIDR))
            });
            return nets;
        }

        /// <summary>
        /// Returns all the possible choices of networks of a size from toCIDR included in the largestNetwork but also include the originalNetwork
        /// </summary>
        /// <param name="largestNetwork">The largest possible network</param>
        /// <param name="originalNetwork">The original network the choices should be a part of</param>
        /// <param name="toCIDR">The target CIDR to look for</param>
        /// <returns>DataTable that includes all the choices</returns>
        public List<Network> ip_expandable_to(string largestNetwork, string originalNetwork, byte toCIDR)
        {
            string[] token = originalNetwork.Split('/');
            byte originalCIDR = (token[1].Contains(".") ? ip_masktocidr(token[1]) : (byte.Parse(token[1])));
            var prefix = new List<Network>();
            uint size = ip_size(toCIDR);
            string mask = ip_cidrtomask(toCIDR);
            uint originalIP = ip_iptouint(token[0]);
            if (toCIDR >= originalCIDR)
            {
                uint end = ip_broadcastAddress(originalIP, ip_iptouint(ip_cidrtomask(originalCIDR)));
                do
                {
                    var dr = new Network();
                    dr.NetworkAddress = IPAddress.Parse(ip_uinttoip(originalIP));
                    dr.NetworkMask = IPAddress.Parse(mask);
                    prefix.Add(dr);
                    originalIP += size;
                } while (originalIP < end);
            }
            else
            {
                string[] token1 = largestNetwork.Split('/');
                uint ip = ip_iptouint(token1[0]);
                while (ip <= originalIP)
                    ip += size;
                ip -= size;
                var dr = new Network();
                dr.NetworkAddress = IPAddress.Parse(ip_uinttoip(ip));
                dr.NetworkMask = IPAddress.Parse(mask);
                prefix.Add(dr);
            }
            return prefix;
        }
    }
}