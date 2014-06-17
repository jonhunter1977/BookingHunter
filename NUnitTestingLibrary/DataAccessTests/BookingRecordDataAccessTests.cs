using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using BH.Domain;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class BookingRecordDataAccessTests
    {
        private BookingRecord _bookingRecord;
        private int _currentBookingRecordId;

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

            BHDataAccess._da.BookingRecord.Save(bookingRecord);

            var bookingRecordList = BHDataAccess._da.BookingRecord.GetAll();

            if (bookingRecordList.Count == 0)
            {
                Assert.Fail("No booking records retrieved from database");
            }
            else
            {
                bookingRecord = (BookingRecord)bookingRecordList[0];
            }

            _bookingRecord = bookingRecord;
            _currentBookingRecordId = _bookingRecord.Id;

            Assert.AreEqual(_bookingRecord.BookingRecordUniqueId, "JHTEST123");
        }

        [Test]
        public void RemoveBookingRecordFromDatabase()
        {
            BHDataAccess._da.BookingRecord.Delete(_bookingRecord);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.BookingRecord.GetById(_currentBookingRecordId));
            Assert.That(ex.Message, Is.EqualTo("BookingRecord Id " + _currentBookingRecordId + " does not exist in database"));
        }
    }
}
