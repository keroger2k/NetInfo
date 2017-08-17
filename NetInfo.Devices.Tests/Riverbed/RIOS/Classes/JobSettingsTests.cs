using System.Linq;
using NetInfo.Devices.Riverbed.RIOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS.Classes {

  [TestFixture]
  public class JobSettingsTests {

    [Test]
    public void can_correct_parse_job_information() {
      var job = new JobSettings();
      job.Settings = @"no email notify failures enable
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
   job 3 date-time 00:00:00 1970/01/01
no job 3 fail-continue
   job 3 name ""NMS""
   job 3 recurring ""0""
   license install LK1-SH10BASE-0000-0000-1-95A9-5F61-2D3D".ToConfig();

      Assert.AreEqual(3, job.Jobs.Count());
      Assert.AreEqual("Template", job.Jobs.ElementAt(0).Name);
      Assert.AreEqual("v3-6_WANX_SSNR", job.Jobs.ElementAt(0).Comment);
      Assert.AreEqual("Hardening", job.Jobs.ElementAt(1).Name);
      Assert.AreEqual("uNavy_HS_WX_IN_v20_0_1", job.Jobs.ElementAt(1).Comment);
      Assert.AreEqual("NMS", job.Jobs.ElementAt(2).Name);
      Assert.AreEqual(string.Empty, job.Jobs.ElementAt(2).Comment);
    }
  }
}