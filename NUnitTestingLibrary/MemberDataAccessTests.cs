using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class MemberDataAccessTests
    {
        private readonly SqlConnectionStringBuilder _memberConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_member;User Id=sa;Password=info51987!;");

        private DataAccess _da;

        private Member _member;
        private int _currentMemberRecordId;

        [SetUp]
        public void SetUp()
        {
            _da = new DataAccess
            {
                MemberConnectionString = _memberConnection.ConnectionString,
                AccessType = DataAccessType.SqlServer           
            };
        }

        [Test]
        public void CreateAndRetrieveMember()
        {
            var member = new Member
            {
                FirstName = "Jon",
                LastName = "Hunter",
                EmailAddress = "jonhunter1977@gmail.com",
                MobileNumber = "07443573404",
                MembershipNumber = "123456789"
            };

            _da.Member.Save(member);

            var memberList = _da.Member.GetAll();

            if (memberList.Count == 0)
            {
                Assert.Fail("No member records retrieved from database");
            }
            else
            {
                member = memberList[0];
            }

            _member = member;
            _currentMemberRecordId = _member.Id;

            Assert.AreEqual(_member.EmailAddress, "jonhunter1977@gmail.com");
        }

        [Test]
        public void RemoveCourtBookingSheetRecordFromDatabase()
        {
            _da.Member.Delete(_member);
            var ex = Assert.Throws<Exception>(() => _da.Member.GetById(_currentMemberRecordId));
            Assert.That(ex.Message, Is.EqualTo("Member Id " + _currentMemberRecordId + " does not exist in database"));
        }
    }
}
