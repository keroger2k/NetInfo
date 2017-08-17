namespace NetInfo.Devices {

  /// <summary>
  /// Every asset is a device that has a configuration
  /// </summary>
  /// <remarks>Any device from the asset repository</remarks>
  public interface IDevice {

    IAssetBlob AssetBlob { get; }

    string Hostname { get; }
  }
}