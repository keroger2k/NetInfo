using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB020_Tests {

    [Test]
    public void RB020_should_return_true_when_latest_version_of_nms_script_is_applied() {
      var blob = new AssetBlob {
        Body = @"no email notify failures enable
   job 1 comment ""v3-3_WANX_uB1_RAS_AUGMENTATION_LB""
   job 1 date-time 00:00:00 1970/01/01
no job 1 fail-continue
   job 1 name ""Template""
   job 1 recurring ""0""
   job 2 comment ""uNavy_HS_WX_IN_v21_0_0""
   job 2 date-time 00:00:00 1970/01/01
no job 2 fail-continue
   job 2 name ""Hardening""
   job 2 recurring ""0""
   job 3 comment ""nms_uNRFK-INSIDE-WANX-v24_0_0""
   job 3 date-time 00:00:00 1970/01/01
no job 3 fail-continue
   job 3 name ""NMS""
   job 3 recurring ""0""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB020(device, nmsMajorVersion: 24);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB020_should_return_false_when_latest_version_of_nms_script_is_not_applied() {
      var blob = new AssetBlob {
        Body = @"no email notify failures enable
   job 1 comment ""v3-3_WANX_uB1_RAS_AUGMENTATION_LB""
   job 1 date-time 00:00:00 1970/01/01
no job 1 fail-continue
   job 1 name ""Template""
   job 1 recurring ""0""
   job 2 comment ""uNavy_HS_WX_IN_v21_0_0""
   job 2 date-time 00:00:00 1970/01/01
no job 2 fail-continue
   job 2 name ""Hardening""
   job 2 recurring ""0""
   job 3 comment ""nms_uNRFK-INSIDE-WANX-v23_0_0""
   job 3 date-time 00:00:00 1970/01/01
no job 3 fail-continue
   job 3 name ""NMS""
   job 3 recurring ""0""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB020(device, nmsMajorVersion: 24);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}