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
        private readonly SqlConnectionStringBuilder _cfgConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_cfg;User Id=sa;Password=info51987!;");

        private DataAccess _da;

        private Facility _facility;
        private int _currentFacilityId;

        private FacilitySchedule _facilitySchedule;
        private int _currentFacilityScheduleId;

        [SetUp]
        public void SetUp()
        {
            _da = new DataAccess
            {
                CfgConnectionString = _cfgConnection.ConnectionString,
                AccessType = DataAccessType.SqlServer
            };
        }

        [Test]
        public void CreateAndRetrieveFacility()
        {
            var facility = new Facility
            {
                FacilityBookAheadDays = 7
            };

            _da.Facility.Save(facility);

            var facilityList = _da.Facility.GetAll();

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

            _da.FacilitySchedule.Save(facilitySchedule);

            var facilityScheduleList = _da.FacilitySchedule.GetAll();

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
            _da.Facility.Delete(_facility);
            var ex = Assert.Throws<Exception>(() => _da.Facility.GetById(_currentFacilityId));
            Assert.That(ex.Message, Is.EqualTo("Facility Id " + _currentFacilityId + " does not exist in database"));
        }

        [Test]
        public void RemoveFacilityScheduleFromDatabase()
        {
            _da.FacilitySchedule.Delete(_facilitySchedule);
            var ex = Assert.Throws<Exception>(() => _da.FacilitySchedule.GetById(_currentFacilityScheduleId));
            Assert.That(ex.Message, Is.EqualTo("FacilitySchedule Id " + _currentFacilityScheduleId + " does not exist in database"));
        }
    }
}
