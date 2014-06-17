using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using BH.Domain;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class MemberDataAccessTests
    {
        private Member _member;
        private int _currentMemberRecordId;

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

            BHDataAccess._da.Member.Save(member);

            var memberList = BHDataAccess._da.Member.GetAll();

            if (memberList.Count == 0)
            {
                Assert.Fail("No member records retrieved from database");
            }
            else
            {
                member = (Member)memberList[0];
            }

            _member = member;
            _currentMemberRecordId = _member.Id;

            Assert.AreEqual(_member.EmailAddress, "jonhunter1977@gmail.com");
        }

        [Test]
        public void RemoveCourtBookingSheetRecordFromDatabase()
        {
            BHDataAccess._da.Member.Delete(_member);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.Member.GetById(_currentMemberRecordId));
            Assert.That(ex.Message, Is.EqualTo("Member Id " + _currentMemberRecordId + " does not exist in database"));
        }
    }
}
