namespace NetInfo.Devices.Brocade.BOS {

  public class Password {

    public Password() {
      this.Type = -1;
      this.Value = string.Empty;
    }

    public int Type { get; set; }

    public string Value { get; set; }
  }
}