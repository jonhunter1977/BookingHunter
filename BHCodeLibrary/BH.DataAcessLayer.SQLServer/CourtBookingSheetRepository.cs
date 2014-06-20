using System;
using System.Collections.Generic;
using BH.Domain;
using BH.DataAccessLayer;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class CourtBookingSheetRepository : ICourtBookingSheetRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public CourtBookingSheetRepository(string bookingConnectionString)
        {
            if (bookingConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(bookingConnectionString);

            if (_dataEngine == null) throw new Exception("Booking Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Booking Database query engine is not connected");
        }

        public IList<CourtBookingSheet> GetAll()
        {
            var courtBookingSheetList = new List<CourtBookingSheet>();

            _sqlToExecute = "SELECT * FROM [dbo].[CourtBookingSheet]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("CourtBookingSheet - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                CourtBookingSheet courtBookingSheet = CreateCourtBookingSheetRecordFromData();
                courtBookingSheetList.Add(courtBookingSheet);
            }

            return courtBookingSheetList;
        }

        public CourtBookingSheet GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[CourtBookingSheet] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("CourtBookingSheet - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                CourtBookingSheet courtBookingSheet = CreateCourtBookingSheetRecordFromData();
                return courtBookingSheet;
            }
            else
            {
                throw new Exception("CourtBookingSheet Id " + id.ToString() + " does not exist in database");
            }            
        }

        public int Insert(CourtBookingSheet saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@CourtBookingStartTime", saveThis.CourtBookingStartTime.ToString());
            _dataEngine.AddParameter("@CourtBookingEndTime", saveThis.CourtBookingEndTime.ToString());
            _dataEngine.AddParameter("@CourtBookingDate", DataFormatting.FormatDateTime(saveThis.CourtBookingDate));

            _sqlToExecute = "INSERT INTO [dbo].[CourtBookingSheet] ";
            _sqlToExecute += "([CourtBookingStartTime]";
            _sqlToExecute += ",[CourtBookingEndTime]";
            _sqlToExecute += ",[CourtBookingDate]) ";
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("CourtBookingSheet - Save failed");

            return insertedRowId;
        }

        public void Delete(CourtBookingSheet deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[CourtBookingSheet] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("CourtBookingSheet - Delete failed");
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private CourtBookingSheet CreateCourtBookingSheetRecordFromData()
        {
            var courtBookingSheet = new CourtBookingSheet
            {
                CourtBookingStartTime = int.Parse(_dataEngine.Dr["CourtBookingStartTime"].ToString()),
                CourtBookingEndTime = int.Parse(_dataEngine.Dr["CourtBookingEndTime"].ToString()),
                CourtBookingDate = Convert.ToDateTime(_dataEngine.Dr["CourtBookingDate"]),
                Id = int.Parse(_dataEngine.Dr["Id"].ToString())
            };

            return courtBookingSheet;
        }
    }
}
