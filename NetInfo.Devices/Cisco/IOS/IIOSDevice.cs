using System.Collections.Generic;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Cisco.IOS.Classes;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;

namespace NetInfo.Devices.IOS
{

    public interface IIOSDevice : IDevice
    {

        string Model { get; }

        string Domain { get; }

        string CryptoKeyName { get; }

        string TacacsSourceInterface { get; }

        bool IsManaged { get; }

        bool IsODMNEnabled { get; }

        IOSClock Clock { get; }

        IOSImage Image { get; }

        IOSPassword EnableSecret { get; }

        IEnumerable<IOSLineItem> Lines { get; }

        IEnumerable<IOSRadiusServer> RadiusServers { get; }

        IEnumerable<IOSInterface> Interfaces { get; }

        IEnumerable<Vlan> Vlans { get; }

        IEnumerable<string> Banner { get; }

        SNMPSettings SNMPSettings { get; }

        bool BootNetwork { get; }

        bool ServiceConfig { get; }

        IPSettings IPSettings { get; }

        LoggingSettings SyslogSettings { get; }

        Dot1xSettings Dot1xSettings { get; }

        AliasExecSettings AliasExecSettings { get; }

        ServiceSettings ServiceSettings { get; }

        TacacsSettings TacacsServer { get; }

        NTPSettings NetworkTimeProtocol { get; }

        AAASettings AAA { get; }

        CryptoSettings Crypto { get; }

        UserSettings UserSettings { get; }

        MonitorSettings MonitorSettings { get; }

        SpanningTreeSettings SpanningTree { get; }

        bool VStackEnabled { get; }

        IEnumerable<ExtendedAccessList> ExtendedAccessLists { get; }

        IEnumerable<StandardAccessList> StandardAccessLists { get; }

        IEnumerable<RouteMap> RouteMaps { get; }

        bool IsBGPConfigured { get; }

        bool IsEIGRPConfigured { get; }

        bool IsOSPFConfigured { get; }

        BorderGatewayProtocol BGP { get; }

        OpenShortestPathFirstProtocol OSPF { get; }

        EnhancedInteriorGatewayRoutingProtocol EIGRP { get; }

        ShowInterfaceStatus ShowInterfaceStatus { get; }

        ShowInterface ShowInterface { get; }

        ShowInterfacesTrunk ShowInterfacesTrunk { get; }

        ShowSnmpUser ShowSnmpUser { get; }

        ShowSnmpGroup ShowSnmpGroup { get; }

        ShowSnmp ShowSnmp { get; }

        ShowClock ShowClock { get; }

        ShowIpInterface ShowIpInterface { get; }

        ShowIpRoute ShowIpRoute { get; }

        ShowVersion ShowVersion { get; }

        ShowVtpPassword ShowVtpPassword { get; }

        ShowVtpStatus ShowVtpStatus { get; }

        ShowInventory ShowInventory { get; }

        ShowCdpInterface ShowCdpInterface { get; }

        ShowCdpNeighbor ShowCdpNeighbors { get; }

        DirAllFileSystems DirAllFileSystems { get; }

        WriteMem WriteMem { get; }
    }
}