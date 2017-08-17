using System.Linq;
using NetInfo.Devices.Riverbed.RIOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS.Classes {

  [TestFixture]
  public class UserSettingsTests {

    [Test]
    public void can_correct_parse_user_information() {
      var job = new UserSettings();
      job.Settings = @"no pm process xinetd launch enable

##
## Network management configuration
##
   username ""admin"" password 7 $1$rZ9uHfXv$/TgCD4zHXf.sMOCinXQfP/
   username ""monitor"" password 7 $1$SA8lfuVu$JBKiO5v6lO1QTH9QEaGvV/
   banner login ""

You are accessing a U.S. Government (USG) Information System (IS) that is

""
   cli default auto-logout 3.0
no cmc appliance ""A42YQ0005B7EF"" auto-configuration enable".ToConfig();

      Assert.AreEqual(2, job.Users.Count());
    }
  }
}