using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class PKISettingsTests {
    private PKISettings pkiSettings;

    [SetUp]
    public void Init() {
      pkiSettings = new PKISettings();
      pkiSettings.Settings = genericSettings;
    }

    private IEnumerable<string> genericSettings = @"
set pki src-interface ethernet2/1
set pki authority default cert-status revocation-check crl best-effort
set pki authority default scep mode ""auto""
set pki x509 raw-cn enable
set pki x509 default cert-path partial
set pki x509 dn country-name ""US""
set pki x509 dn org-name ""U.S. Government""
set pki x509 dn org-unit-name ""USN,PKI,DoD""
set pki x509 dn name ""AHDSEDSTFW11.navy.mil""
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void can_correctly_parse_source_interface() {
      Assert.AreEqual("ethernet2/1", pkiSettings.SourceInterface);
    }

    [Test]
    public void can_correctly_parse_if_raw_cn_is_enabled() {
      Assert.True(pkiSettings.x509.RawCn);
    }

    [Test]
    public void can_correctly_parse_cert_path() {
      Assert.AreEqual("partial", pkiSettings.x509.CertPath);
    }

    [Test]
    public void can_correctly_parse_country_name() {
      Assert.AreEqual("US", pkiSettings.x509.DN.CountryName);
    }

    [Test]
    public void can_correctly_parse_org_name() {
      Assert.AreEqual("U.S. Government", pkiSettings.x509.DN.OrgName);
    }

    [Test]
    public void can_correctly_parse_org_unit_name() {
      Assert.AreEqual("USN,PKI,DoD", pkiSettings.x509.DN.OrgUnitName);
    }

    [Test]
    public void can_correctly_parse_dn_name() {
      Assert.AreEqual("AHDSEDSTFW11.navy.mil", pkiSettings.x509.DN.Name);
    }
  }
}