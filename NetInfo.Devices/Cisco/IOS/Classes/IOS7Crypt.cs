using System;
using System.Text;

namespace NetInfo.Devices.Cisco.IOS {

  public static class IOS7Crypt {

    private static byte[] xlatPrime = new byte[] {
                0x64, 0x73, 0x66, 0x64, 0x3b, 0x6b, 0x66, 0x6f,
                0x41, 0x2c, 0x2e, 0x69, 0x79, 0x65, 0x77, 0x72,
                0x6b, 0x6c, 0x64, 0x4a, 0x4b, 0x44, 0x48, 0x53,
                0x55, 0x42, 0x73, 0x67, 0x76, 0x63, 0x61, 0x36,
	            0x39, 0x38, 0x33, 0x34, 0x6e, 0x63, 0x78, 0x76,
	            0x39, 0x38, 0x37, 0x33, 0x32, 0x35, 0x34, 0x6b,
	            0x3b, 0x66, 0x67, 0x38, 0x37
        };

    public static string Decrypt(string hash) {
      if (hash.Length < 4) {
        throw new ArgumentException("Type 7 Hashes must be longer than 4 characters");
      }

      int seed = -1;

      if (int.TryParse(hash.Substring(0, 2), out seed)) {
        int c = 2;
        var result = new StringBuilder();
        while (c < hash.Length) {
          string t = hash.Substring(c, 2);
          int h = int.Parse(t, System.Globalization.NumberStyles.HexNumber);
          int v = h ^ xlatPrime[seed++];
          result.Append((char)v);
          c += 2;
          seed %= 53;
        }
        return result.ToString();
      }
      return string.Empty;
    }
  }
}