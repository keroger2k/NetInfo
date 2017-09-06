using System.Net;

namespace NetInfo.Core.Models
{
    public class Network
    {
        public IPAddress NetworkAddress { get; set; }
        public IPAddress NetworkMask { get; set; }
    }
}
