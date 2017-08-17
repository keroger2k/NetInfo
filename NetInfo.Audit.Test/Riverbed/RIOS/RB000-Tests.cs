using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB000_Tests {

    [Test]
    public void RB000_should_return_true_when_test_script_is_applied_correctly_to_WI_this_is_expired() {
      var blob = new AssetBlob {
        Body = @"AGST-U00-WI-01 # no cli session paging enable
AGST-U00-WI-01 # write mem
AGST-U00-WI-01 # show running-config

##
## Other IP configuration
##
   hostname ""AGST-U00-WI-01""

##
## Logging configuration
##
   logging local ""info""

AGST-U00-WI-01 # show config full
AGST-U00-WI-01 # show images
AGST-U00-WI-01 # show port-label
AGST-U00-WI-01 # show version
AGST-U00-WI-01 # show clock
AGST-U00-WI-01 # show boot
AGST-U00-WI-01 # show interface configured
AGST-U00-WI-01 # show interface brief
AGST-U00-WI-01 # show ip route
AGST-U00-WI-01 # show in-path neighbor
AGST-U00-WI-01 # show in-path neighbor peers
AGST-U00-WI-01 # show ip in-path route inpath0_0
AGST-U00-WI-01 # show ip in-path route inpath0_1
AGST-U00-WI-01 # show ip in-path route inpath1_0
AGST-U00-WI-01 # show stats alarm
AGST-U00-WI-01 # show stats cpu
AGST-U00-WI-01 # show snmp
AGST-U00-WI-01 # show ssh server
AGST-U00-WI-01 # show ntp
AGST-U00-WI-01 # show redirect peers
AGST-U00-WI-01 # show tacacs
AGST-U00-WI-01 # show logging
AGST-U00-WI-01 # show web
AGST-U00-WI-01 # show hardware
AGST-U00-WI-01 # show info
AGST-U00-WI-01 # show licenses
AGST-U00-WI-01 # #END-OF-TEST-SCRIPT"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB000(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB000_should_return_true_when_test_script_is_applied_correctly_to_WI_until_march() {
      var blob = new AssetBlob {
        Body = @"AGST-U00-WI-01 # no cli session paging enable
AGST-U00-WI-01 # write mem
AGST-U00-WI-01 # show running-config

##
## Other IP configuration
##
   hostname ""AGST-U00-WI-01""

##
## Logging configuration
##
   logging local ""info""

AGST-U00-WI-01 # show config full
AGST-U00-WI-01 # show images
AGST-U00-WI-01 # show port-label
AGST-U00-WI-01 # show version
AGST-U00-WI-01 # show clock
AGST-U00-WI-01 # show boot
AGST-U00-WI-01 # show interface configured
AGST-U00-WI-01 # show interface brief
AGST-U00-WI-01 # show ip route
AGST-U00-WI-01 # show in-path neighbor
AGST-U00-WI-01 # show in-path neighbor peers
AGST-U00-WI-01 # show ip in-path route inpath0_0
AGST-U00-WI-01 # show ip in-path route inpath0_1
AGST-U00-WI-01 # show ip in-path route inpath1_0
AGST-U00-WI-01 # show ip in-path route inpath1_1
AGST-U00-WI-01 # show stats alarm
AGST-U00-WI-01 # show stats cpu
AGST-U00-WI-01 # show snmp
AGST-U00-WI-01 # show ssh server
AGST-U00-WI-01 # show ntp
AGST-U00-WI-01 # show redirect peers
AGST-U00-WI-01 # show tacacs
AGST-U00-WI-01 # show logging
AGST-U00-WI-01 # show web
AGST-U00-WI-01 # show hardware
AGST-U00-WI-01 # show info
AGST-U00-WI-01 # show licenses
AGST-U00-WI-01 # #END-OF-TEST-SCRIPT"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB000_should_return_false_when_test_script_is_applied_incorrectly_to_WI() {
      var blob = new AssetBlob {
        Body = @"AGST-U00-WI-01 # no cli session paging enable
AGST-U00-WI-01 # write mem FAIL
AGST-U00-WI-01 # show running-config

##
## Other IP configuration
##
   hostname ""AGST-U00-WI-01""

##
## Logging configuration
##
   logging local ""info""

AGST-U00-WI-01 # show config full
AGST-U00-WI-01 # show images
AGST-U00-WI-01 # show port-label
AGST-U00-WI-01 # show version
AGST-U00-WI-01 # show clock
AGST-U00-WI-01 # show boot
AGST-U00-WI-01 # show interface configured
AGST-U00-WI-01 # show interface brief
AGST-U00-WI-01 # show ip route
AGST-U00-WI-01 # show ip in-path route inpath0_0
AGST-U00-WI-01 # show ip in-path route inpath0_1
AGST-U00-WI-01 # show ip in-path route inpath1_0
AGST-U00-WI-01 # show ip in-path route inpath1_1
AGST-U00-WI-01 # show ip in-path route inpath2_0
AGST-U00-WI-01 # show stats alarm
AGST-U00-WI-01 # show connections all brief
AGST-U00-WI-01 # show stats connections month
AGST-U00-WI-01 # show stats traffic passthrough month
AGST-U00-WI-01 # show stats traffic optimized bi-direction month
AGST-U00-WI-01 # show stats traffic optimized lan-to-wan month
AGST-U00-WI-01 # show stats traffic optimized wan-to-lan month
AGST-U00-WI-01 # show stats cpu
AGST-U00-WI-01 # show snmp
AGST-U00-WI-01 # show peers
AGST-U00-WI-01 # show ssh server
AGST-U00-WI-01 # show tacacs
AGST-U00-WI-01 # show logging
AGST-U00-WI-01 # show proc
AGST-U00-WI-01 # show datastore
AGST-U00-WI-01 # show web
AGST-U00-WI-01 # show hardware all
AGST-U00-WI-01 # show raid info
AGST-U00-WI-01 # show raid diagram
AGST-U00-WI-01 # show info
AGST-U00-WI-01 # show licenses
AGST-U00-WI-01 # show service
AGST-U00-WI-01 # show protocol ssl peering certificate
AGST-U00-WI-01 # show protocol ssl peering ca
AGST-U00-WI-01 # show protocol ssl server
AGST-U00-WI-01 # show protocol ssl expiring-certs
AGST-U00-WI-01 # #END-OF-TEST-SCRIPT"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB000(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB000_should_return_true_when_test_script_is_applied_correctly_to_WX_CM_WC() {
      var blob = new AssetBlob {
        Body = @"AGST-U00-WX-01 # no cli session paging enable
AGST-U00-WX-01 # write mem
AGST-U00-WX-01 # show running-config

##
## Other IP configuration
##
   hostname ""AGST-U00-WX-01""

##
## Logging configuration
##
   logging local ""info""

AGST-U00-WX-01 # show connections all brief
AGST-U00-WX-01 # show datastore
AGST-U00-WX-01 # show hardware all
AGST-U00-WX-01 # show images
AGST-U00-WX-01 # show ip in-path route inpath1_1
AGST-U00-WX-01 # show ip in-path route inpath2_0
AGST-U00-WX-01 # show peers
AGST-U00-WX-01 # show proc
AGST-U00-WX-01 # show protocol ssl expiring-certs
AGST-U00-WX-01 # show protocol ssl peering ca
AGST-U00-WX-01 # show protocol ssl peering certificate
AGST-U00-WX-01 # show protocol ssl server
AGST-U00-WX-01 # show raid diagram
AGST-U00-WX-01 # show raid info
AGST-U00-WX-01 # show service
AGST-U00-WX-01 # show stats connections month
AGST-U00-WX-01 # show stats traffic optimized bi-direction month
AGST-U00-WX-01 # show stats traffic optimized lan-to-wan month
AGST-U00-WX-01 # show stats traffic optimized wan-to-lan month
AGST-U00-WX-01 # show stats traffic passthrough month
AGST-U00-WX-01 # show config full
AGST-U00-WX-01 # show port-label
AGST-U00-WX-01 # show version
AGST-U00-WX-01 # show clock
AGST-U00-WX-01 # show boot
AGST-U00-WX-01 # show interface configured
AGST-U00-WX-01 # show interface brief
AGST-U00-WX-01 # show ip route
AGST-U00-WX-01 # show ip in-path route inpath0_0
AGST-U00-WX-01 # show ip in-path route inpath0_1
AGST-U00-WX-01 # show ip in-path route inpath1_0
AGST-U00-WX-01 # show stats alarm
AGST-U00-WX-01 # show stats cpu
AGST-U00-WX-01 # show snmp
AGST-U00-WX-01 # show ssh server
AGST-U00-WX-01 # show ntp
AGST-U00-WX-01 # show tacacs
AGST-U00-WX-01 # show logging
AGST-U00-WX-01 # show web
AGST-U00-WX-01 # show info
AGST-U00-WX-01 # show licenses
AGST-U00-WX-01 # #END-OF-TEST-SCRIPT"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB000_should_return_false_when_test_script_is_applied_incorrectly_to_WX_CM_WC() {
      var blob = new AssetBlob {
        Body = @"AGST-U00-WX-01 # no cli session paging enable
AGST-U00-WX-01 # write mem FAIL
AGST-U00-WX-01 # show running-config

##
## Other IP configuration
##
   hostname ""AGST-U00-WX-01""

##
## Logging configuration
##
   logging local ""info""

AGST-U00-WX-01 # show config full
AGST-U00-WX-01 # show images
AGST-U00-WX-01 # show port-label
AGST-U00-WX-01 # show version
AGST-U00-WX-01 # show clock
AGST-U00-WX-01 # show boot
AGST-U00-WX-01 # show interface configured
AGST-U00-WX-01 # show interface brief
AGST-U00-WX-01 # show ip route
AGST-U00-WX-01 # show ip in-path route inpath0_0
AGST-U00-WX-01 # show ip in-path route inpath0_1
AGST-U00-WX-01 # show ip in-path route inpath1_0
AGST-U00-WX-01 # show ip in-path route inpath1_1
AGST-U00-WX-01 # show ip in-path route inpath2_0
AGST-U00-WX-01 # show stats alarm
AGST-U00-WX-01 # show connections all brief
AGST-U00-WX-01 # show stats connections month
AGST-U00-WX-01 # show stats traffic passthrough month
AGST-U00-WX-01 # show stats traffic optimized bi-direction month
AGST-U00-WX-01 # show stats traffic optimized lan-to-wan month
AGST-U00-WX-01 # show stats traffic optimized wan-to-lan month
AGST-U00-WX-01 # show stats cpu
AGST-U00-WX-01 # show snmp
AGST-U00-WX-01 # show peers
AGST-U00-WX-01 # show ssh server
AGST-U00-WX-01 # show tacacs
AGST-U00-WX-01 # show logging
AGST-U00-WX-01 # show proc
AGST-U00-WX-01 # show datastore
AGST-U00-WX-01 # show web
AGST-U00-WX-01 # show hardware all
AGST-U00-WX-01 # show raid info
AGST-U00-WX-01 # show raid diagram
AGST-U00-WX-01 # show info
AGST-U00-WX-01 # show licenses
AGST-U00-WX-01 # show service
AGST-U00-WX-01 # show protocol ssl peering certificate
AGST-U00-WX-01 # show protocol ssl peering ca
AGST-U00-WX-01 # show protocol ssl server
AGST-U00-WX-01 # show protocol ssl expiring-certs
AGST-U00-WX-01 # #END-OF-TEST-SCRIPT"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB000(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}