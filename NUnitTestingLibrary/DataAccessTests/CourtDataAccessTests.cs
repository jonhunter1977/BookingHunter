using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using System.Data.Common;
using BH.DataAccessLayer;
using BH.Domain;

namespace NUnitTestingLibrary
{
    class CourtDataAccessTests
    {
        private Court _court;
        private int _currentCourtId;

        [Test]
        public void CreateAndRetrieveCourt()
        {
            var court = new Court
            {
                CourtDescription = "Court 1"
            };

            BHDataAccess._da.Court.Save(court);

            var courtList = BHDataAccess._da.Court.GetAll();

            if (courtList.Count == 0)
            {
                Assert.Fail("No court records retrieved from database");
            }
            else
            {
                court = (Court)courtList[0];
            }

            _court = court;
            _currentCourtId = _court.Id;

            Assert.AreEqual(_court.CourtDescription, "Court 1");
        }

        [Test]
        public void RemoveCourtFromDatabase()
        {
            BHDataAccess._da.Court.Delete(_court);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.Court.GetById(_currentCourtId));
            Assert.That(ex.Message, Is.EqualTo("Court Id " + _currentCourtId + " does not exist in database"));
        }
    }
}
