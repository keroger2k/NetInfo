using System;
using System.Collections.Generic;
using NetInfo.Devices.Infrastructure.Enums;

namespace NetInfo.Devices
{

    public interface IAsset
    {
        IEnumerable<string> Configuration { get; }

        string Body { get; set; }
    }

    public interface IAssetBlob : IAsset
    {

        //long Id { get; set; }

        //long AssetId { get; set; }

        //DateTime Imported { get; set; }

        //string Host { get; set; }

        //HeaderDeviceTypes DeviceType { get; }
    }
}