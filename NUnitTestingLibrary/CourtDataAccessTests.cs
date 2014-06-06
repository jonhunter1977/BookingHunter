using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class CourtDataAccessTests
    {
        private readonly SqlConnectionStringBuilder _cfgConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_cfg;User Id=sa;Password=info51987!;");

        private DataAccess _da;

        private Court _court;
        private int _currentCourtId;

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
        public void CreateAndRetrieveCourt()
        {
            var court = new Court
            {
                CourtDescription = "Court 1"
            };

            _da.Court.Save(court);

            var courtList = _da.Court.GetAll();

            if (courtList.Count == 0)
            {
                Assert.Fail("No court records retrieved from database");
            }
            else
            {
                court = courtList[0];
            }

            _court = court;
            _currentCourtId = _court.Id;

            Assert.AreEqual(_court.CourtDescription, "Court 1");
        }

        [Test]
        public void RemoveCourtFromDatabase()
        {
            _da.Court.Delete(_court);
            var ex = Assert.Throws<Exception>(() => _da.Court.GetById(_currentCourtId));
            Assert.That(ex.Message, Is.EqualTo("Court Id " + _currentCourtId + " does not exist in database"));
        }
    }
}
