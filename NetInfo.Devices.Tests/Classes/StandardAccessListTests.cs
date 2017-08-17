using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using NetInfo.Devices.Classes;

namespace NetInfo.Devices.Tests.Classes {

  public class StandardAccessList {
    private IEnumerable<string> _lines;

    public StandardAccessList(IEnumerable<string> lines) {
      this._lines = lines;
    }

    public int GetNumber() {
      int aclNumber = -1;
      if (_lines.Any()) {
        var aclNumbers = _lines
          .Where(c => new Regex(Generic.STD_ACL_ENTRY, RegexOptions.IgnoreCase).IsMatch(c))
          .Select(c => int.Parse(new Regex(Generic.STD_ACL_ENTRY, RegexOptions.IgnoreCase).Match(c).Groups[1].Value));
        if (aclNumbers.Distinct().Count() != 1) {
          throw new ArgumentOutOfRangeException("Access lists lines were incorrectly parsed");
        }
        aclNumber = aclNumbers.First();
      }
      return aclNumber;
    }

    public IEnumerable<Rule> GetRules() {
      return new List<Rule>();
    }

    public class DenyRule : Rule {

      private DenyRule() {
      }
    }

    public class AllowRule : Rule {

      private AllowRule() {
      }
    }

    public abstract class Rule {

      public IPAddress SourceAddress { get; set; }

      public IPAddress SourceMask { get; set; }
    }
  }

  //[TestFixture]
  //public class StandardAccessListTests {
  //  IEnumerable<string> ACL;

  //  [SetUp]
  //  public void Init() {
  //    this.ACL = new string[] {
  //      "access-list 99 remark Norfolk NOC",
  //      "access-list 99 permit 10.16.27.32 0.0.0.31",
  //      "access-list 99 deny any log"
  //    };
  //  }

  //  [Test]
  //  public void creating_a_new_access_list_with_an_empty_array_returns_negative_one_for_the_number() {
  //    var acl = new StandardAccessList(new string[] { });
  //    Assert.AreEqual(-1, acl.GetNumber());
  //  }

  //  [Test]
  //  public void creating_a_new_access_list_with_an_empty_array_returns_zero_rules() {
  //    var acl = new StandardAccessList(new string[] { });
  //    Assert.AreEqual(0, acl.GetRules().Count());
  //  }

  //  [Test]
  //  public void creating_a_new_access_list_correctly_parses_the_acl_number() {
  //    var acl = new StandardAccessList(this.ACL);
  //    Assert.AreEqual(99, acl.GetNumber());
  //  }

  //  [Test]
  //  public void creating_a_new_access_list_correctly_parses_the_the_permit_statements() {
  //    var acl = new StandardAccessList(this.ACL);
  //    Assert.AreEqual(1, acl.GetRules()
  //      .Where(c => c.GetType() == typeof(NetInfo.Devices.Tests.Classes.StandardAccessList.AllowRule)).Count());
  //  }

  //  [Test]
  //  public void creating_a_new_access_list_correctly_parses_the_the_deny_statements() {
  //    var acl = new StandardAccessList(this.ACL);
  //    Assert.AreEqual(1, acl.GetRules()
  //      .Where(c => c.GetType() == typeof(NetInfo.Devices.Tests.Classes.StandardAccessList.DenyRule)).Count());
  //  }

  //}
}