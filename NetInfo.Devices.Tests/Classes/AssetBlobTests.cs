//using NetInfo.Devices.Infrastructure.Enums;
//using NUnit.Framework;

//namespace NetInfo.Devices.Tests.Classes {

//  [TestFixture]
//  public class AssetBlobTests {

//    [Test]
//    public void ensure_asset_blob_without_a_header_with_be_identified_as_unknown() {
//      var blob = new AssetBlob {
//        Body = @""
//      };

//      Assert.AreEqual(HeaderDeviceTypes.Unknown, blob.DeviceType);
//    }

//    [Test]
//    public void ensure_asset_blob_can_identify_cisco_device() {
//      var blob = new AssetBlob {
//        Body = @"ABQQ-U01-AS-01#! !#**************************************************************************************#
//ABQQ-U01-AS-01#! !#***  Version: Cisco IOS Test Script Version 5.23
//ABQQ-U01-AS-01#! !#**************************************************************************************#
//ABQQ-U01-AS-01#! !#***  Device:  Use on any Cisco device running IOS
//ABQQ-U01-AS-01#! !#**************************************************************************************#
//ABQQ-U01-AS-01#! !#***  Purpose: Use this script to gather data for further analysis
//ABQQ-U01-AS-01#! !#**************************************************************************************#
//ABQQ-U01-AS-01#! !#    NOTE: Ignore any errors due to syntax or missing hardware
//ABQQ-U01-AS-01#! !#**************************************************************************************#
//ABQQ-U01-AS-01#!
//ABQQ-U01-AS-01#!
//ABQQ-U01-AS-01#!"
//      };

//      Assert.AreEqual(HeaderDeviceTypes.Cisco, blob.DeviceType);
//    }

//    [Test]
//    public void ensure_asset_blob_can_identify_brocade_device() {
//      var blob = new AssetBlob {
//        Body = @"SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !#***  Version: Brocade Layer-2 Test Script Version 1.0.6
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !#***  Device:  Use on any Brocade device running as Layer-2 only
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !#***  Purpose: Use this script to gather data for further analysis
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !#    NOTE: Ignore any errors due to syntax or missing hardware
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !#
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !#    NOTE: Due to buffer issues with some terminal software when connected via the
//SSH@PRLH-U08-AS-16#! !#          console port, it is STRONGLY recommended that this script be run via
//SSH@PRLH-U08-AS-16#! !#          a remote terminal (VTY, SSH, Telnet) session.
//SSH@PRLH-U08-AS-16#! !#
//SSH@PRLH-U08-AS-16#! !#          If the console port must be used, increase the buffer size and 'line send
//SSH@PRLH-U08-AS-16#! !#          delay' (SecureCRT).  3000ms (max) for SecureCRT.
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !#
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#! !# Must be in the privileged level global EXEC (enable)
//SSH@PRLH-U08-AS-16#! !#************************************************************************************
//SSH@PRLH-U08-AS-16#
//SSH@PRLH-U08-AS-16#
//SSH@PRLH-U08-AS-16#
//SSH@PRLH-U08-AS-16#"
//      };

//      Assert.AreEqual(HeaderDeviceTypes.Brocade, blob.DeviceType);
//    }

//    [Test]
//    public void ensure_asset_blob_can_identify_riverbed_device() {
//      var blob = new AssetBlob {
//        Body = @"QUAN-U00-CM-01 # ## **************************************************************************************#
//QUAN-U00-CM-01 # ## ***  Version:     WANX Test Script Version 5.19                                       #
//QUAN-U00-CM-01 # ## **************************************************************************************#
//QUAN-U00-CM-01 # ## ***  Device:  Use on any Riverbed WAN Accelerator                                     #
//QUAN-U00-CM-01 # ## **************************************************************************************#
//QUAN-U00-CM-01 # ## ***  Purpose: Use this script to gather data for further analysis                     #
//QUAN-U00-CM-01 # ## **************************************************************************************#
//QUAN-U00-CM-01 # ## ***   NOTE: Ignore any errors due to syntax or missing hardware                       #
//QUAN-U00-CM-01 # ## ***   NOTE: Set columns to 400 to prevent output from being distorted                 #
//QUAN-U00-CM-01 # ## **************************************************************************************#
//QUAN-U00-CM-01 # #
//QUAN-U00-CM-01 # #
//QUAN-U00-CM-01 # #
//QUAN-U00-CM-01 # #"
//      };

//      Assert.AreEqual(HeaderDeviceTypes.Riverbed, blob.DeviceType);
//    }

//    [Test]
//    public void ensure_asset_blob_can_identify_mcafee_device() {
//      var blob = new AssetBlob {
//        Body = @"!#****************************************************************************************#
//!#***  Version: McAfee IPS ST&E Test Script 2.0 - NMCI   				   #
//!#****************************************************************************************#
//!#***  Devices:  McAfee IPS devices							   #
//!#****************************************************************************************#
//!#***  Purpose: Use this script to pull ST&E results 					   #
//!#****************************************************************************************#
//!#***  Note: Ignore any Invalid Syntax Errors						   #
//!#****************************************************************************************#
//!#     Result File to be posted:							   #
//!#     	                 							   #
//!#     Hostname Date Time.Txt                                                             #
//!#****************************************************************************************#
//intruShell@NMCIGRLKSN43>
//[Sensor Info]
//System Name             : NMCIGRLKSN43"
//      };

//      Assert.AreEqual(HeaderDeviceTypes.McAfee, blob.DeviceType);
//    }

//    [Test]
//    public void ensure_asset_blob_can_identify_netscreen_device() {
//      var blob = new AssetBlob {
//        Body = @"!#***************************************************************************************
//!#***  Version: Netscreen Test Script Version 1.4
//!#***************************************************************************************
//!#***  Device:  Use on any Netscreen
//!#***************************************************************************************
//!#***  Purpose: Use this script to gather data for further analysis
//!#***************************************************************************************
//!#    NOTE: Ignore any errors due to syntax or missing hardware
//!#***************************************************************************************
//HA:AHDSEDSTFW11z(B)-> set console page 0"
//      };

//      Assert.AreEqual(HeaderDeviceTypes.Netscreen, blob.DeviceType);
//    }
//  }
//}