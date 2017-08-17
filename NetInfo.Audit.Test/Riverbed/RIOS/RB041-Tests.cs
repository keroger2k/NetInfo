using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB041_Tests {

    [Test]
    public void RB041_should_return_true_when_the_nms_hardening_script_major_version_number_matches_the_approved_major_version() {
      var blob = new AssetBlob {
        Body = @"no email notify events enable
no email notify failures enable
   job 1 comment ""v3-6_WANX_SSNR""
   job 1 date-time 00:00:00 1970/01/01
no job 1 fail-continue
   job 1 name ""Template""
   job 1 recurring ""0""
   job 2 comment ""uNavy_HS_WX_IN_v20_0_1""
   job 2 date-time 00:00:00 1970/01/01
no job 2 fail-continue
   job 2 name ""Hardening""
   job 2 recurring ""0""
   job 3 comment ""nms_uNRFK-INSIDE-WANX-v24_0_0""
   job 3 date-time 00:00:00 1970/01/01
no job 3 fail-continue
   job 3 name ""NMS""
   job 3 recurring ""0""
   license install LK1-SH10BASE-0000-0000-1-95A9-5F61-2D3D
   license install LK1-SH10CIFS-0000-0000-1-3D48-F18B-8993"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB041(device, 20);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB041_should_return_false_when_the_nms_hardening_script_major_version_number_does_not_match_the_approved_major_version() {
      var blob = new AssetBlob {
        Body = @"no email notify events enable
no email notify failures enable
   job 1 comment ""v3-6_WANX_SSNR""
   job 1 date-time 00:00:00 1970/01/01
no job 1 fail-continue
   job 1 name ""Template""
   job 1 recurring ""0""
   job 2 comment ""uNavy_HS_WX_IN_v19_0_1""
   job 2 date-time 00:00:00 1970/01/01
no job 2 fail-continue
   job 2 name ""Hardening""
   job 2 recurring ""0""
   job 3 comment ""nms_uNRFK-INSIDE-WANX-v24_0_0""
   job 3 date-time 00:00:00 1970/01/01
no job 3 fail-continue
   job 3 name ""NMS""
   job 3 recurring ""0""
   license install LK1-SH10BASE-0000-0000-1-95A9-5F61-2D3D
   license install LK1-SH10CIFS-0000-0000-1-3D48-F18B-8993"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB041(device, 20);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB041_should_return_false_when_a_hardening_script_version_can_not_be_found() {
      var blob = new AssetBlob {
        Body = @"no email notify events enable
no email notify failures enable
   job 1 comment ""v3-6_WANX_SSNR""
   job 1 date-time 00:00:00 1970/01/01
no job 1 fail-continue
   job 1 name ""Template""
   job 1 recurring ""0""
   job 2 comment ""nms_uNRFK-INSIDE-WANX-v24_0_0""
   job 2 date-time 00:00:00 1970/01/01
no job 2 fail-continue
   job 2 name ""NMS""
   job 2 recurring ""0""
   license install LK1-SH10BASE-0000-0000-1-95A9-5F61-2D3D
   license install LK1-SH10CIFS-0000-0000-1-3D48-F18B-8993"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB041(device, 20);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}