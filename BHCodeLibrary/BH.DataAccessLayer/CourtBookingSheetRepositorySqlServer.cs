using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class CourtBookingSheetRepositorySqlServer : ICourtBookingSheetRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public CourtBookingSheetRepositorySqlServer(string bookingConnectionString)
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
            _sqlToExecute = "SELECT * FROM [dbo].[CourtBookingSheet] WHERE Id = " + id.ToString();

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

        public void Save(CourtBookingSheet saveThis)
        {
            _sqlToExecute = "INSERT INTO [dbo].[CourtBookingSheet] ";
            _sqlToExecute += "([CourtBookingStartTime]";
            _sqlToExecute += ",[CourtBookingEndTime]";
            _sqlToExecute += ",[CourtBookingDate])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(" + saveThis.CourtBookingStartTime;
            _sqlToExecute += "," + saveThis.CourtBookingEndTime;
            _sqlToExecute += ",'" + DataFormatting.FormatDateTime(saveThis.CourtBookingDate) + "')";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("CourtBookingSheet - Save failed");
        }

        public void Delete(CourtBookingSheet deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[CourtBookingSheet] WHERE Id = " + deleteThis.Id.ToString();

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
