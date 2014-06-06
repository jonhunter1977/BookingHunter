using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting location data from the database
    /// </summary>
    internal class LocationRepositorySqlServer : ILocationRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public LocationRepositorySqlServer(string cfgConnectionString)
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
                var location = new Location
                {
                    Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                    LocationDescription = _dataEngine.Dr["LocationDescription"].ToString()                        
                };

                locationList.Add(location);
            }

            return locationList;
        }

        public Location GetById(int id)
        {
            _sqlToExecute = "SELECT * FROM [dbo].[Customer] WHERE Id = " + id.ToString();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Location - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                var location = new Location
                {
                    Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                    LocationDescription = _dataEngine.Dr["LocationDescription"].ToString()                    
                };

                return location;
            }
            else
            {
                throw new Exception("Location Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(Location saveThis)
        {          
            _sqlToExecute = "INSERT INTO [dbo].[Location] ";
            _sqlToExecute += "([LocationDescription])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "('" + saveThis.LocationDescription + "')";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Location - Save failed");
        }

        public void Delete(Location deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[Location] WHERE Id = " + deleteThis.Id.ToString();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Location - Delete failed");
        }
    }
}
