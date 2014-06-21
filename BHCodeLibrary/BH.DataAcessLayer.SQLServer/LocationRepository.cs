using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting location data from the database
    /// </summary>
    internal class LocationRepository : ILocationRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public LocationRepository(string cfgConnectionString)
        {
            if (cfgConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(cfgConnectionString);

            if (_dataEngine == null) throw new Exception("Cfg Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Cfg Database query engine is not connected");
        }

        public IList<Location> GetAll()
        {
            var locationList = new List<Location>();

            _sqlToExecute = "SELECT * FROM [dbo].[Location]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Location - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                var location = CreateLocationFromData();
                locationList.Add(location);
            }

            return locationList;
        }

        public Location GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[Customer] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Location - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                var location = CreateLocationFromData();
                return location;
            }
            else
            {
                throw new Exception("Location Id " + id.ToString() + " does not exist in database");
            }            
        }

        public int Insert(Location saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@LocationDescription", saveThis.LocationDescription);

            _sqlToExecute = "INSERT INTO [dbo].[Location] ";
            _sqlToExecute += "([LocationDescription]) ";
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("Location - Save failed");

            return insertedRowId;
        }

        public void Update(Location saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@LocationDescription", saveThis.LocationDescription);

            _sqlToExecute = "UPDATE [dbo].[Location] SET ";
            _sqlToExecute += "([LocationDescription] = @LocationDescription) ";
            _sqlToExecute += "WHERE [Id] = " + saveThis.Id;

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Location - Update failed");
        }

        public void Delete(Location deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[Location] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Location - Delete failed");
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private Location CreateLocationFromData()
        {
            var location = new Location
            {
                Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                LocationDescription = _dataEngine.Dr["LocationDescription"].ToString()
            };

            return location;
        }
    }
}
