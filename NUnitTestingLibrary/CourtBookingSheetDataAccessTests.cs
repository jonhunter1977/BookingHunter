using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class CourtBookingSheetAccessTests
    {
        private readonly SqlConnectionStringBuilder _bookingConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_booking;User Id=sa;Password=info51987!;");

        private DataAccess _da;

        private CourtBookingSheet _courtBookingSheet;
        private int _currentCourtBookingSheetRecordId;

        [SetUp]
        public void SetUp()
        {
            _da = new DataAccess
            {
                BookingConnectionString = _bookingConnection.ConnectionString,
                AccessType = DataAccessType.SqlServer
            };
        }

        [Test]
        public void CreateAndRetrieveCourtBookingSheet()
        {
            var courtBookingSheet = new CourtBookingSheet
            {
                CourtBookingStartTime = 40,
                CourtBookingEndTime = 80,
                CourtBookingDate = new DateTime(2014, 7, 31)
            };

            _da.CourtBookingSheet.Save(courtBookingSheet);

            var courtBookingSheetList = _da.CourtBookingSheet.GetAll();

            if (courtBookingSheetList.Count == 0)
            {
                Assert.Fail("No court booking sheet records retrieved from database");
            }
            else
            {
                courtBookingSheet = courtBookingSheetList[0];
            }

            _courtBookingSheet = courtBookingSheet;
            _currentCourtBookingSheetRecordId = _courtBookingSheet.Id;

            Assert.AreEqual(_courtBookingSheet.CourtBookingDate.ToShortDateString(), "31/07/2014");
        }

        [Test]
        public void RemoveCourtBookingSheetRecordFromDatabase()
        {
            _da.CourtBookingSheet.Delete(_courtBookingSheet);
            var ex = Assert.Throws<Exception>(() => _da.CourtBookingSheet.GetById(_currentCourtBookingSheetRecordId));
            Assert.That(ex.Message, Is.EqualTo("CourtBookingSheet Id " + _currentCourtBookingSheetRecordId + " does not exist in database"));
        }
    }
}
