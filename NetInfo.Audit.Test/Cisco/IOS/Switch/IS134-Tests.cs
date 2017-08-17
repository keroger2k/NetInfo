//using NetInfo.Audit.Cisco.IOS.Switch;
//using NetInfo.Audit.NMCI.Services;
//using NetInfo.Devices;
//using NetInfo.Devices.NMCI.Cisco.IOS;
//using NUnit.Framework;

//namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

//  [TestFixture]
//  public class IS134_Tests {
//    //private AccessListFactory aclFactory;

//    [SetUp]
//    public void Init() {
//      //this.aclFactory = new AccessListFactory(null);
//    }

//    [Test]
//    public void IS134_should_return_true_when_acl_68_matches_the_sti_site() {
//      var blob = new AssetBlob {
//        Body = @"logging 172.19.0.203
//access-list 68 remark Norfolk NOC
//access-list 68 permit 172.18.1.0 0.0.0.31
//access-list 68 remark San Diego NOC
//access-list 68 permit 172.16.0.0 0.0.0.31
//access-list 68 remark Pearl Harbor NOC
//access-list 68 permit 172.19.0.192 0.0.0.31
//access-list 68 deny   any log
//!
//snmp-server engineID remote 172.16.0.12 8000000B7F80E3DBEA515C41E69D2F0022F21D94FA "
//      };
//      INMCIIOSDevice device = new NMCIIOSDevice(blob);
//      INECCItem item = new IS134(device, aclFactory.GetNavyAcl68());

//      var result = item.Compliant();

//      Assert.True(result);
//    }

//    [Test]
//    public void IS134_should_return_false_when_acl_68_dooes_not_match_the_sti_site() {
//      var blob = new AssetBlob {
//        Body = @"logging 172.19.0.203
//access-list 68 remark Norfolk NOC
//access-list 68 permit 0.0.0.0 0.0.0.0
//access-list 68 remark San Diego NOC
//access-list 68 permit 172.16.0.0 0.0.0.31
//access-list 68 remark Pearl Harbor NOC
//access-list 68 permit 172.19.0.192 0.0.0.31
//access-list 68 deny   any log
//!
//snmp-server engineID remote 172.16.0.12 8000000B7F80E3DBEA515C41E69D2F0022F21D94FA "
//      };
//      INMCIIOSDevice device = new NMCIIOSDevice(blob);
//      INECCItem item = new IS134(device, aclFactory.GetNavyAcl68());

//      var result = item.Compliant();

//      Assert.False(result);
//    }
//  }
//}