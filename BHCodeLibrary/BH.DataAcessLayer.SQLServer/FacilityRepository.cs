﻿using System;
using System.Collections.Generic;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;

namespace BH.DataAccessLayer.ADONet
{
    /// <summary>
    /// Class for getting facility data from the database
    /// </summary>
    internal class FacilityRepository : IFacilityRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public FacilityRepository(string cfgConnectionString)
        {
            if (cfgConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(cfgConnectionString);

            if (_dataEngine == null) throw new Exception("Cfg Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Cfg Database query engine is not connected");
        }

        public IQueryable<Facility> GetAll()
        {
            var facilityList = new List<Facility>();

            _sqlToExecute = "SELECT * FROM [dbo].[Facility]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Facility - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                var facility = CreateFacilityFromData();
                facilityList.Add(facility);
            }

            return facilityList.AsQueryable();
        }

        public Facility GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[Facility] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Facility - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                var facility = CreateFacilityFromData();
                return facility;
            }
            else
            {
                throw new Exception("Facility Id " + id.ToString() + " does not exist in database");
            }            
        }

        public int Insert(Facility saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@FacilityBookAheadDays", saveThis.FacilityBookAheadDays.ToString());

            _sqlToExecute = "INSERT INTO [dbo].[Facility] ";
            _sqlToExecute += "([FacilityBookAheadDays]) ";
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("Facility - Save failed");

            return insertedRowId;
        }

        public void Update(Facility saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@FacilityBookAheadDays", saveThis.FacilityBookAheadDays.ToString());

            _sqlToExecute = "UPDATE [dbo].[Facility] SET ";
            _sqlToExecute += "[FacilityBookAheadDays] = @FacilityBookAheadDays ";
            _sqlToExecute += "WHERE [Id] = " + saveThis.Id;

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Facility - Update failed");
        }

        public void Delete(Facility deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[Facility] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Facility - Delete failed");
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private Facility CreateFacilityFromData()
        {
            var facility = new Facility
            {
                Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                FacilityBookAheadDays = int.Parse(_dataEngine.Dr["FacilityBookAheadDays"].ToString())
            };

            return facility;
        }
    }
}
