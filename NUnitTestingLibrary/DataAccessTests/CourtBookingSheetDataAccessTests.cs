using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using BH.Domain;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class CourtBookingSheetDataAccessTests
    {
        private CourtBookingSheet _courtBookingSheet;
        private int _currentCourtBookingSheetRecordId;

        [Test]
        public void CreateAndRetrieveCourtBookingSheet()
        {
            var courtBookingSheet = new CourtBookingSheet
            {
                CourtBookingStartTime = 40,
                CourtBookingEndTime = 80,
                CourtBookingDate = new DateTime(2014, 7, 31)
            };

            BHDataAccess._da.CourtBookingSheet.Save(courtBookingSheet);

            var courtBookingSheetList = BHDataAccess._da.CourtBookingSheet.GetAll();

            if (courtBookingSheetList.Count == 0)
            {
                Assert.Fail("No court booking sheet records retrieved from database");
            }
            else
            {
                courtBookingSheet = (CourtBookingSheet)courtBookingSheetList[0];
            }

            _courtBookingSheet = courtBookingSheet;
            _currentCourtBookingSheetRecordId = _courtBookingSheet.Id;

            Assert.AreEqual(_courtBookingSheet.CourtBookingDate.ToShortDateString(), "31/07/2014");
        }

        [Test]
        public void RemoveCourtBookingSheetRecordFromDatabase()
        {
            BHDataAccess._da.CourtBookingSheet.Delete(_courtBookingSheet);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.CourtBookingSheet.GetById(_currentCourtBookingSheetRecordId));
            Assert.That(ex.Message, Is.EqualTo("CourtBookingSheet Id " + _currentCourtBookingSheetRecordId + " does not exist in database"));
        }
    }
}
