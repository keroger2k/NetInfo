using NetInfo.Devices;

namespace NetInfo.Audit
{
    /// <summary>
    /// Interface that signifies the ability to check for a Security Technical Implementation Guide
    /// </summary>
    public interface ISTIGItem
    {
        bool Compliant();
    }

    /// <summary>
    /// Interface to track if STIG is meant for a Router
    /// </summary>
    public interface IRouterSecurityItem : ISTIGItem
    {

    }

    /// <summary>
    /// Interface to track if Router STIG is meant for a Cisco Device
    /// </summary>
    public interface ICiscoRouterSecurityItem : IRouterSecurityItem
    {
      
    }

    public enum STIGClassification
    {
        Unclass,
        Class
    }

    public enum STIGSeverity
    {
        I,
        II,
        III
    }

}