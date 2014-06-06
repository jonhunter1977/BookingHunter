using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting facility data from the database
    /// </summary>
    internal class FacilityRepositorySqlServer : IFacilityRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public FacilityRepositorySqlServer(string cfgConnectionString)
        {
            if (cfgConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(cfgConnectionString);

            if (_dataEngine == null) throw new Exception("Cfg Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Cfg Database query engine is not connected");
        }

        public IList<Facility> GetAll()
        {
            var facilityList = new List<Facility>();

            _sqlToExecute = "SELECT * FROM [dbo].[Facility]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Facility - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                var facility = new Facility
                {
                    Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                    FacilityBookAheadDays = int.Parse(_dataEngine.Dr["FacilityBookAheadDays"].ToString())
                };

                facilityList.Add(facility);
            }

            return facilityList;
        }

        public Facility GetById(int id)
        {
            _sqlToExecute = "SELECT * FROM [dbo].[Facility] WHERE Id = " + id.ToString();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Facility - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                var facility = new Facility
                {
                    FacilityBookAheadDays = int.Parse(_dataEngine.Dr["FacilityBookAheadDays"].ToString()),
                    Id = int.Parse(_dataEngine.Dr["Id"].ToString())
                };

                return facility;
            }
            else
            {
                throw new Exception("Facility Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(Facility saveThis)
        {
            _sqlToExecute = "INSERT INTO [dbo].[Facility] ";
            _sqlToExecute += "([FacilityBookAheadDays])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(" + saveThis.FacilityBookAheadDays + ")";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Facility - Save failed");
        }

        public void Delete(Facility deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[Facility] WHERE Id = " + deleteThis.Id.ToString();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Facility - Delete failed");
        }
    }
}
