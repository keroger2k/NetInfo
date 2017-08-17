using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS015_Tests {

    [Test]
    public void BS015_should_return_true_when_all_disabled_ports_are_assigned_to_vlan_2() {
      var blob = new AssetBlob {
        Body = @"
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#show interfaces brief

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
1/1/1   Up      Forward Full 1G    None  Yes N/A  0   748e.f82e.30c0  U00_IR01
1/1/2   Up      Forward Full 1G    None  Yes N/A  0   748e.f82e.30c0  U00_IR02
1/1/3   Disable None    None None  None  No  2    0   748e.f82e.30c2  DISABLED
1/1/4   Up      Forward Half 100M  None  No  217  0   748e.f82e.30c3  NC-XXXXX
1/1/5   Down    None    None None  None  No  500  0   748e.f82e.30c4  NP-XXXXX
1/1/6   Up      Forward Full 1G    None  No  217  0   748e.f82e.30c5  NC-XXXXX
1/1/7   Down    None    None None  None  No  217  0   748e.f82e.30c6  NC-XXXXX
1/1/8   Disable None    None None  None  No  2    0   748e.f82e.30c7  DISABLED
1/1/9   Down    None    None None  None  No  217  0   748e.f82e.30c8  NC-XXXXX
1/1/10  Disable None    None None  None  No  2    0   748e.f82e.30c9  DISABLED
1/1/11  Disable None    None None  None  No  2    0   748e.f82e.30ca  DISABLED
1/1/48  Disable None    None None  None  No  2    0   748e.f82e.30ef  DISABLED
1/2/1   Disable None    None None  None  No  2    0   748e.f82e.30f1
1/2/2   Disable None    None None  None  No  2    0   748e.f82e.30f2
mgmt1   Disable None    None None  None  No  None 0   748e.f82e.30c0

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
ve99    Up      N/A     N/A  N/A   None  N/A N/A  N/A 748e.f82e.30c0  <== Mana
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS015(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS015_should_return_false_when_not_all_disabled_ports_are_assigned_to_vlan_2() {
      var blob = new AssetBlob {
        Body = @"
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#show interfaces brief

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
1/1/1   Up      Forward Full 1G    None  Yes N/A  0   748e.f82e.30c0  U00_IR01
1/1/2   Up      Forward Full 1G    None  Yes N/A  0   748e.f82e.30c0  U00_IR02
1/1/3   Disable None    None None  None  No  2    0   748e.f82e.30c2  DISABLED
1/1/4   Up      Forward Half 100M  None  No  217  0   748e.f82e.30c3  NC-XXXXX
1/1/5   Down    None    None None  None  No  500  0   748e.f82e.30c4  NP-XXXXX
1/1/6   Up      Forward Full 1G    None  No  217  0   748e.f82e.30c5  NC-XXXXX
1/1/7   Down    None    None None  None  No  217  0   748e.f82e.30c6  NC-XXXXX
1/1/8   Disable None    None None  None  No  217  0   748e.f82e.30c7  DISABLED
1/1/9   Down    None    None None  None  No  217  0   748e.f82e.30c8  NC-XXXXX
1/1/10  Disable None    None None  None  No  2    0   748e.f82e.30c9  DISABLED
1/1/11  Disable None    None None  None  No  2    0   748e.f82e.30ca  DISABLED
1/1/48  Disable None    None None  None  No  2    0   748e.f82e.30ef  DISABLED
1/2/1   Disable None    None None  None  No  2    0   748e.f82e.30f1
1/2/2   Disable None    None None  None  No  2    0   748e.f82e.30f2
mgmt1   Disable None    None None  None  No  None 0   748e.f82e.30c0

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
ve99    Up      N/A     N/A  N/A   None  N/A N/A  N/A 748e.f82e.30c0  <== Mana
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS015(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS015_does_not_include_ERR_DIS_ports() {
      var blob = new AssetBlob {
        Body = @"SSH@SDNI-U06-AS-23#
SSH@SDNI-U06-AS-23#show interfaces brief

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
1/1/1   Up      Forward Full 1G    None  Yes N/A  0   748e.f896.3040  U06_DR01
1/1/3   Disable None    None None  None  No  2    0   748e.f82e.30c2  DISABLED
1/1/40  Up      Forward Full 1G    None  No  940  0   748e.f896.3067  C27INFMA
1/1/41  ERR-DIS None    None None  None  No  701  0   748e.f896.3068  PACIFICA
1/1/42  ERR-DIS None    None None  None  No  701  0   748e.f896.3069  PACIFICA
1/1/43  Up      Forward Full 1G    None  No  260  0   748e.f896.306a  C27SAMEC
1/1/44  Up      Forward Full 1G    None  No  260  0   748e.f896.306b  C27SAMEC
5/2/1   Up      Forward Full 16G   None  No  N/A  0   748e.f896.0c71
5/2/2   Up      Forward Full 16G   None  No  N/A  0   748e.f896.0c72
mgmt1   Disable None    None None  None  No  None 0   748e.f896.3040

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
ve99    Up      N/A     N/A  N/A   None  N/A N/A  N/A 748e.f896.3040  <== Mana
SSH@SDNI-U06-AS-23#"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS015(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS015_does_not_get_effected_by_pound_signs_at_the_end_of_lines_in_show_statements() {
      var blob = new AssetBlob {
        Body = @"
hostname DLGR-U04-AS-06
!
SSH@DLGR-U04-AS-06#show interfaces brief

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
1/1/1   Down    None    None None  None  Yes N/A  0   0024.38bb.4b40  U04_DR01
1/1/4   Down    None    None None  None  No  241  0   0024.38bb.4b43  OPEN#
2/1/48  Up      Forward Full 100M  None  No  245  0   748e.f896.072f  ENCLAVE
2/2/1   Up      Forward Full 16G   None  No  N/A  0   748e.f896.0731
2/2/2   Up      Forward Full 16G   None  No  N/A  0   748e.f896.0732
mgmt1   Disable None    None None  None  No  None 0   0024.38bb.4b40

Port    Link    State   Dupl Speed Trunk Tag Pvid Pri MAC            Name
ve99    Up      N/A     N/A  N/A   None  N/A N/A  N/A 0024.38bb.4b40  <== Mana
SSH@DLGR-U04-AS-06#
SSH@DLGR-U04-AS-06#"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS015(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}