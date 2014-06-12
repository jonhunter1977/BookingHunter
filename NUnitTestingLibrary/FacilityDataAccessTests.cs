using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class FacilityDataAccessTests
    {
        private Facility _facility;
        private int _currentFacilityId;

        private FacilitySchedule _facilitySchedule;
        private int _currentFacilityScheduleId;

        [SetUp]
        public void SetUp()
        {
            BHDataAccess.InitialiseDataAccess();
        }

        [Test]
        public void CreateAndRetrieveFacility()
        {
            var facility = new Facility
            {
                FacilityBookAheadDays = 7
            };

            BHDataAccess._da.Facility.Save(facility);

            var facilityList = BHDataAccess._da.Facility.GetAll();

            if (facilityList.Count == 0)
            {
                Assert.Fail("No facility records retrieved from database");
            }
            else
            {
                facility = facilityList[0];
            }

            _facility = facility;
            _currentFacilityId = _facility.Id;

            Assert.AreEqual(_facility.FacilityBookAheadDays, 7);
        }

        [Test]
        public void CreateAndRetrieveFacilitySchedule()
        {
            var facilitySchedule = new FacilitySchedule
            {
                FacilityScheduleDescription = "Squash Courts",
                StartMinuteMonday = 0,
                EndMinuteMonday = 1,
                MondayFacilityBookLength = 2,
                StartMinuteTuesday = 3,
                EndMinuteTuesday = 4,
                TuesdayFacilityBookLength = 5,
                StartMinuteWednesday = 6,
                EndMinuteWednesday = 7,
                WednesdayFacilityBookLength = 8,
                StartMinuteThursday = 9,
                EndMinuteThursday = 10,
                ThursdayFacilityBookLength = 11,
                StartMinuteFriday = 12,
                EndMinuteFriday = 13,
                FridayFacilityBookLength = 14,
                StartMinuteSaturday = 15,
                EndMinuteSaturday = 16,
                SaturdayFacilityBookLength = 17,
                StartMinuteSunday = 0,
                EndMinuteSunday = 0,
                SundayFacilityBookLength = 0
            };

            BHDataAccess._da.FacilitySchedule.Save(facilitySchedule);

            var facilityScheduleList = BHDataAccess._da.FacilitySchedule.GetAll();

            if (facilityScheduleList.Count == 0)
            {
                Assert.Fail("No facility schedule records retrieved from database");
            }
            else
            {
                facilitySchedule = facilityScheduleList[0];
            }

            _facilitySchedule = facilitySchedule;
            _currentFacilityScheduleId = _facilitySchedule.Id;

            Assert.AreEqual(_facilitySchedule.FacilityScheduleDescription, "Squash Courts");
        }

        [Test]
        public void RemoveFacilityFromDatabase()
        {
            BHDataAccess._da.Facility.Delete(_facility);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.Facility.GetById(_currentFacilityId));
            Assert.That(ex.Message, Is.EqualTo("Facility Id " + _currentFacilityId + " does not exist in database"));
        }

        [Test]
        public void RemoveFacilityScheduleFromDatabase()
        {
            BHDataAccess._da.FacilitySchedule.Delete(_facilitySchedule);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.FacilitySchedule.GetById(_currentFacilityScheduleId));
            Assert.That(ex.Message, Is.EqualTo("FacilitySchedule Id " + _currentFacilityScheduleId + " does not exist in database"));
        }
    }
}
