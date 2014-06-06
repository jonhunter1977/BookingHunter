using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    class LocationDataAccessTests
    {
        private readonly SqlConnectionStringBuilder _cfgConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_cfg;User Id=sa;Password=info51987!;");

        private DataAccess _da;

        private Location _location;
        private int _currentLocationId;

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
        public void CreateAndRetrieveLocation()
        {
            var location = new Location
            {
                LocationDescription = "Neston Squash Club"
            };

            _da.Location.Save(location);

            var locationList = _da.Location.GetAll();

            if (locationList.Count == 0)
            {
                Assert.Fail("No location records retrieved from database");
            }
            else
            {
                location = locationList[0];
            }

            _location = location;
            _currentLocationId = _location.Id;

            Assert.AreEqual(_location.LocationDescription, "Neston Squash Club");
        }

        [Test]
        public void RemoveLocationFromDatabase()
        {
            _da.Location.Delete(_location);
            var ex = Assert.Throws<Exception>(() => _da.Location.GetById(_currentLocationId));
            Assert.That(ex.Message, Is.EqualTo("Location Id " + _currentLocationId + " does not exist in database"));
        }
    }
}
