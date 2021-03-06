﻿using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router
{

    [TestFixture]
    public class IR175_Tests
    {
        private AssetBlob blob;

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void ir175_should_return_true_for_complaint_device()
        {
            blob = new AssetBlob
            {
                Body = @"no logging console
logging monitor informational
enable secret 5 GOOD_MD5
!
alias exec harden uNAVY-ODMN-IOSSWT-v25_0_0
!
"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0230(device);

            var result = item.Compliant();

            Assert.True(result);
        }

    }
}