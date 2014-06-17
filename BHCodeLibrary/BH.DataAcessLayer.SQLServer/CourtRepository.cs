using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class CourtRepository : ICourtRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public CourtRepository(string cfgConnectionString)
        {
            if (cfgConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(cfgConnectionString);

            if (_dataEngine == null) throw new Exception("Cfg Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Cfg Database query engine is not connected");
        }

        public IList<ICourt> GetAll()
        {
            var courtList = new List<ICourt>();

            _sqlToExecute = "SELECT * FROM [dbo].[Court]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Court - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                ICourt court = CreateCourtFromData();
                courtList.Add(court);
            }

            return courtList;
        }

        public ICourt GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[Court] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Court - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                ICourt court = CreateCourtFromData();
                return court;
            }
            else
            {
                throw new Exception("Court Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(ICourt saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@CourtDescription", saveThis.CourtDescription);

            _sqlToExecute = "INSERT INTO [dbo].[Court] ";
            _sqlToExecute += "([CourtDescription]) ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Court - Save failed"); 
        }

        public void Delete(ICourt deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[Court] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Court - Delete failed");  
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private ICourt CreateCourtFromData()
        {
            var court = new Court
            {
                Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                CourtDescription = _dataEngine.Dr["CourtDescription"].ToString()
            };

            return court;
        }
    }
}
