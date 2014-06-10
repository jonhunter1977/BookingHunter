using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class BookingRecordDataAccessTests
    {
        private readonly SqlConnectionStringBuilder _bookingConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_booking;User Id=sa;Password=info51987!;");

        private DataAccess _da;

        private BookingRecord _bookingRecord;
        private int _currentBookingRecordId;

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
        public void CreateAndRetrieveBookingRecord()
        {
            var bookingRecord = new BookingRecord
            {
                TimeArrived = 0,
                ArrivalRegistrationMethod = ArrivalRegistrationMethod.NotArrived,
                BookingStatus = BookingStatus.Draft,
                BookingRecordUniqueId = "JHTEST123",
                BookingRecordPin = 1234
            };

            _da.BookingRecord.Save(bookingRecord);

            var bookingRecordList = _da.BookingRecord.GetAll();

            if (bookingRecordList.Count == 0)
            {
                Assert.Fail("No booking records retrieved from database");
            }
            else
            {
                bookingRecord = bookingRecordList[0];
            }

            _bookingRecord = bookingRecord;
            _currentBookingRecordId = _bookingRecord.Id;

            Assert.AreEqual(_bookingRecord.BookingRecordUniqueId, "JHTEST123");
        }

        [Test]
        public void RemoveBookingRecordFromDatabase()
        {
            _da.BookingRecord.Delete(_bookingRecord);
            var ex = Assert.Throws<Exception>(() => _da.BookingRecord.GetById(_currentBookingRecordId));
            Assert.That(ex.Message, Is.EqualTo("BookingRecord Id " + _currentBookingRecordId + " does not exist in database"));
        }
    }
}
