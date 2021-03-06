﻿using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR014_Tests {

    [Test]
    public void IR014_should_return_true_when_all_trunking_vlans_are_assigned_native_vlan1() {
      var blob = new AssetBlob {
        Body = @"DANI-U01-DH-01#!
DANI-U01-DH-01#show interfaces trunk

Port      Mode         Encapsulation  Status        Native vlan
Fa0       on           802.1q         trunking      1

Port      Vlans allowed on trunk
Fa0       1,99-1005

Port      Vlans allowed and active in management domain
Fa0       1,99,210,500

Port      Vlans in spanning tree forwarding state and not pruned
Fa0       1,99,210,500
DANI-U01-DH-01#!
DANI-U01-DH-01#"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NETVLAN008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    //    [Test]
    //    public void IR014_should_return_false_when_not_all_trunking_vlans_are_assigned_native_vlan1() {
    //      var blob = new AssetBlob {
    //        Body = @"PRLH-U00-IR-01#show interfaces trunk
    //
    //Port          Mode         Encapsulation  Status        Native vlan
    //Gi2/1         desirable    802.1q         trunking      1
    //Gi2/2         desirable    802.1q         trunking      2
    //Gi2/6         on           802.1q         trunking      1
    //Gi3/1         desirable    802.1q         trunking      1
    //Gi3/3         on           802.1q         trunking      1
    //Po10          on           802.1q         trunking      1
    //Po112         desirable    802.1q         trunking      1
    //Po191         on           802.1q         trunking      1
    //Po192         on           802.1q         trunking      1
    //
    //Port          Vlans allowed on trunk
    //Gi2/1         92,99
    //Gi2/2         92,99
    //Gi2/6         90,99
    //Gi3/1         25,27,30,33,75,86,99,800-849
    //Gi3/3         33,37,47,99,129
    //Po10          90,99
    //Po112         25,27,30,33,36-37,47,86,92,94,99-1005
    //Po191         90,99,800-849
    //Po192         91,99,800-849
    //
    //Port          Vlans allowed and active in management domain
    //Gi2/1         92,99
    //Gi2/2         92,99
    //Gi2/6         90,99
    //Gi3/1         25,27,30,33,86,99,800-849
    //Gi3/3         33,37,47,99,129
    //Po10          90,99
    //Po112         25,27,30,33,36-37,47,86,92,94,99-100,129,142,800-849,914
    //Po191         90,99,800-849
    //Po192         91,99,800-849
    //
    //Port          Vlans in spanning tree forwarding state and not pruned
    //Gi2/1         92,99
    //Gi2/2         92,99
    //Gi2/6         90,99
    //Gi3/1         25,27,30,33,86,99,800-849
    //Gi3/3         33,37,47,99,129
    //Po10          90,99
    //Po112         25,27,30,33,36-37,47,86,92,94,99-100,129,142,800-849,914
    //Po191         90,99,800-849
    //Po192         91,99,800-849
    //PRLH-U00-IR-01#!"
    //      };

    //      INMCIIOSDevice device = new NMCIIOSDevice(blob);
    //      INECCItem item = new IR014(device);

    //      var result = item.Compliant();

    //      Assert.False(result);

    //    }
  }
}