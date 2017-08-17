namespace NetInfo.Devices.NMCI.Infrastructure.Interfaces {

  /// <summary>
  /// Common properties shared for all NMCI device hostnames.
  /// </summary>
  public interface IHostname {

    string Name { get; }

    string Site { get; }
  }
}