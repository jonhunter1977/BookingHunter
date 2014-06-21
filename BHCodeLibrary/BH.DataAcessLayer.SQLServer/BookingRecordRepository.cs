using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class BookingRecordRepository : IBookingRecordRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public BookingRecordRepository(string bookingConnectionString)
        {
            if (bookingConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(bookingConnectionString);

            if (_dataEngine == null) throw new Exception("Booking Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Booking Database query engine is not connected");
        }

        public IList<BookingRecord> GetAll()
        {
            var bookingRecordList = new List<BookingRecord>();

            _sqlToExecute = "SELECT * FROM [dbo].[BookingRecord]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("BookingRecord - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                BookingRecord bookingRecord = CreateBookingRecordFromData();
                bookingRecordList.Add(bookingRecord);
            }

            return bookingRecordList;
        }

        public BookingRecord GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[BookingRecord] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("BookingRecord - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                BookingRecord bookingRecord = CreateBookingRecordFromData();
                return bookingRecord;
            }
            else
            {
                throw new Exception("BookingRecord Id " + id.ToString() + " does not exist in database");
            }            
        }

        public int Insert(BookingRecord saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@TimeArrived", saveThis.TimeArrived.ToString());
            _dataEngine.AddParameter("@ArrivalRegistrationMethod", ((int)saveThis.ArrivalRegistrationMethod).ToString());
            _dataEngine.AddParameter("@BookingStatus", ((int)saveThis.BookingStatus).ToString());
            _dataEngine.AddParameter("@BookingRecordUniqueId", saveThis.BookingRecordUniqueId.ToString());
            _dataEngine.AddParameter("@BookingRecordPin", saveThis.BookingRecordPin.ToString());

            _sqlToExecute = "INSERT INTO [dbo].[BookingRecord] ";
            _sqlToExecute += "([TimeArrived]";
            _sqlToExecute += ",[ArrivalRegistrationMethod]";
            _sqlToExecute += ",[BookingStatus]";
            _sqlToExecute += ",[BookingRecordUniqueId]";
            _sqlToExecute += ",[BookingRecordPin]) ";
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("BookingRecord - Save failed");

            return insertedRowId; 
        }

        public void Update(BookingRecord saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@TimeArrived", saveThis.TimeArrived.ToString());
            _dataEngine.AddParameter("@ArrivalRegistrationMethod", ((int)saveThis.ArrivalRegistrationMethod).ToString());
            _dataEngine.AddParameter("@BookingStatus", ((int)saveThis.BookingStatus).ToString());
            _dataEngine.AddParameter("@BookingRecordUniqueId", saveThis.BookingRecordUniqueId.ToString());
            _dataEngine.AddParameter("@BookingRecordPin", saveThis.BookingRecordPin.ToString());

            _sqlToExecute = "UPDATE [dbo].[BookingRecord] SET ";
            _sqlToExecute += "[TimeArrived] = @TimeArrived";
            _sqlToExecute += ",[ArrivalRegistrationMethod] = @ArrivalRegistrationMethod";
            _sqlToExecute += ",[BookingStatus] = @BookingStatus";
            _sqlToExecute += ",[BookingRecordUniqueId] = @BookingRecordUniqueId";
            _sqlToExecute += ",[BookingRecordPin] = @BookingRecordPin ";
            _sqlToExecute += "WHERE [Id] = " + saveThis.Id;

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("BookingRecord - Update failed");
        }

        public void Delete(BookingRecord deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[BookingRecord] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("BookingRecord - Delete failed"); 
        }

        public BookingRecord GetByBookingRecordUniqueId(int BookingRecordUniqueId)
        {
            _sqlToExecute = "SELECT * FROM [dbo].[BookingRecord] WHERE BookingRecordUniqueId = '" + BookingRecordUniqueId.ToString() + "'";

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("GetByBookingRecordUniqueId - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                BookingRecord bookingRecord = CreateBookingRecordFromData();
                return bookingRecord;
            }
            else
            {
                throw new Exception("BookingRecordUniqueId Id " + BookingRecordUniqueId.ToString() + " does not exist in database");
            }
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private BookingRecord CreateBookingRecordFromData()
        {
            var bookingRecord = new BookingRecord
            {
                TimeArrived = int.Parse(_dataEngine.Dr["TimeArrived"].ToString()),
                ArrivalRegistrationMethod = (ArrivalRegistrationMethod)int.Parse(_dataEngine.Dr["ArrivalRegistrationMethod"].ToString()),
                BookingStatus = (BookingStatus)int.Parse(_dataEngine.Dr["BookingStatus"].ToString()),
                BookingRecordUniqueId = _dataEngine.Dr["BookingRecordUniqueId"].ToString(),
                BookingRecordPin = int.Parse(_dataEngine.Dr["BookingRecordPin"].ToString()),
                Id = int.Parse(_dataEngine.Dr["Id"].ToString())
            };

            return bookingRecord;
        }
    }
}
