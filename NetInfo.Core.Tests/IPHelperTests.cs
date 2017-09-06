using System;
using System.Collections;
using NetInfo.Core.Helpers;
using NUnit.Framework;

namespace NetInfo.Core.Tests {

  [TestFixture]
  public class IPHelperTests {

    [TestCase("10.0.0.0/24", "10.0.0.27")]
    [TestCase("10.0.0.0/255.255.254.0", "10.0.1.27")]
    public void validates_valid_addresses_inside_given_range_returns_true(string range, string address) {
      var ipHelp = new IPHelper();

      bool result = ipHelp.ip_inside_range(range, address);

      Assert.IsTrue(result);
    }

    [TestCase("10.0.0.0/23", "10.0.3.27")]
    [TestCase("10.0.0.0/255.255.254.0", "10.0.3.27")]
    public void validates_invalid_addresses_outside_given_range_returns_false(string range, string address) {
      var ipHelp = new IPHelper();

      bool result = ipHelp.ip_inside_range(range, address);

      Assert.IsFalse(result);
    }

    [TestCase("10.0.1.0")]
    [TestCase("10.0.1.0/24")]
    [TestCase("10.0.1.0/255.255.255.0")]
    public void validates_ip_is_valid_given_valid_ip_addresses_returns_true(string address) {
      var ipHelp = new IPHelper();

      bool result = ipHelp.ip_is_valid(address);

      Assert.IsTrue(result);
    }

    [TestCase("10.0.1.0 ")]
    public void validates_ip_is_valid_given_valid_ip_addresses_with_extra_space_at_end_returns_true(string address) {
      var ipHelp = new IPHelper();

      bool result = ipHelp.ip_is_valid(address);

      Assert.IsTrue(result);
    }

    [TestCase("10.0.257.0")]
    [TestCase("10.0.1.0/23")]
    [TestCase("10.0.1.0/255.0.255.0")]
    [TestCase("10.0.0.0/255.257.255.0")]
    public void validates_ip_is_valid_given_invalid_ip_addresses_returns_false(string address) {
      var ipHelp = new IPHelper();

      bool result = ipHelp.ip_is_valid(address);

      Assert.IsFalse(result);
    }

    [Test]
    public void validate_ip_cidrtounintmask() {
      var ipHelp = new IPHelper();

      uint result = ipHelp.ip_cidrtouintmask(24);

      Assert.AreEqual(4294967040, result);
    }

    [Test]
    public void validate_ip_cidrtomask() {
      var ipHelp = new IPHelper();

      string result = ipHelp.ip_cidrtomask(24);

      Assert.AreEqual("255.255.255.0", result);
    }

    [Test]
    public void validate_ip_uinttoip() {
      var ipHelp = new IPHelper();

      string result = ipHelp.ip_uinttoip(4294967040);

      Assert.AreEqual("255.255.255.0", result);
    }

    [Test]
    public void validate_ip_iptouint() {
      var ipHelp = new IPHelper();

      uint result = ipHelp.ip_iptouint("10.0.0.0");

      Assert.AreEqual(Convert.ToUInt32(167772160), result);
    }

    [Test]
    public void validate_ip_masktocidr() {
      var ipHelp = new IPHelper();

      byte result = ipHelp.ip_masktocidr("255.255.255.0");

      Assert.AreEqual(Convert.ToByte(24), result);
    }

    [Test]
    public void validate_ip_size() {
      var ipHelp = new IPHelper();

      uint result1 = ipHelp.ip_size("255.255.0.0");
      uint result2 = ipHelp.ip_size(15);

      Assert.AreEqual(Convert.ToUInt32(65536), result1);
      Assert.AreEqual(Convert.ToUInt32(131072), result2);
    }

    [Test]
    public void validate_ip_get_prefix_length() {
      var ipHelp = new IPHelper();
      var ip1 = ipHelp.ip_iptouint("192.168.0.0");
      var ip2 = ipHelp.ip_iptouint("192.168.0.127");
      byte result = ipHelp.ip_get_prefix_length(ip1, ip2);

      Assert.AreEqual(Convert.ToByte(7), result);
    }

    [Test]
    public void validate_ip_broadcastAddress() {
      var ipHelp = new IPHelper();
      var ip1 = 3232235520; //192.168.0.0
      var ip2 = 4294966784; //255.255.254.0
      uint result_uint = ipHelp.ip_broadcastAddress(ip1, ip2);
      string result_string = ipHelp.ip_broadcastAddress("192.168.0.0", "255.255.254.0");

      Assert.AreEqual(Convert.ToUInt32(3232236031), result_uint);
      Assert.AreEqual("192.168.1.255", result_string);
    }

    [Test]
    public void validate_ip_networkAddress() {
      var ipHelp = new IPHelper();
      var ip1 = 3232235520; //192.168.0.0
      var ip2 = 4294966784; //255.255.254.0
      uint result_uint = ipHelp.ip_networkAddress(ip1, ip2);
      string result_string = ipHelp.ip_networkAddress("192.168.1.0", "255.255.254.0");

      Assert.AreEqual(Convert.ToUInt32(3232235520), result_uint);
      Assert.AreEqual("192.168.0.0", result_string);
    }

    [Test]
    public void validate_ip_range() {
      var ipHelp = new IPHelper();
      ArrayList result1 = ipHelp.ip_range("192.168.0.0/22");
      ArrayList result2 = ipHelp.ip_range("192.168.0.2/27");

      Assert.AreEqual("192.168.0.1", result1[0]);
      Assert.AreEqual("192.168.3.255", result1[1]);

      Assert.AreEqual("192.168.0.3", result2[0]);
      Assert.AreEqual("192.168.0.33", result2[1]);
    }

    [TestCase("0.0.0.0")]
    [TestCase("10.0.1.0")]
    [TestCase("10.0.1.0")]
    [TestCase("255.255.255.255")]
    public void validate_ip_is_valid_correctly_identifies_valid_addresses(string address) {
      var ipHelp = new IPHelper();

      bool result = ipHelp.valid_ip(address);

      Assert.IsTrue(result);
    }

    [TestCase("256.0.0.0")]
    [TestCase("1.256.0.0")]
    [TestCase("1.1.256.0")]
    [TestCase("1.1.1.256")]
    public void validate_ip_is_valid_correctly_identifies_invalid_addresses(string address) {
      var ipHelp = new IPHelper();

      bool result = ipHelp.valid_ip(address);

      Assert.IsFalse(result);
    }
  }
}