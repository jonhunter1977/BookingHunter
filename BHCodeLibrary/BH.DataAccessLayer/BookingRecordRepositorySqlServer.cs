using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class BookingRecordRepositorySqlServer : IBookingRecordRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public BookingRecordRepositorySqlServer(string bookingConnectionString)
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
            _sqlToExecute = "SELECT * FROM [dbo].[BookingRecord] WHERE Id = " + id.ToString();

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

        public void Save(BookingRecord saveThis)
        {
            _sqlToExecute = "INSERT INTO [dbo].[BookingRecord] ";
            _sqlToExecute += "([TimeArrived]";
            _sqlToExecute += ",[ArrivalRegistrationMethod]";
            _sqlToExecute += ",[BookingStatus]";
            _sqlToExecute += ",[BookingRecordUniqueId]";
            _sqlToExecute += ",[BookingRecordPin])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(" + saveThis.TimeArrived;
            _sqlToExecute += "," + (int)saveThis.ArrivalRegistrationMethod;
            _sqlToExecute += "," + (int)saveThis.BookingStatus;
            _sqlToExecute += ",'" + saveThis.BookingRecordUniqueId + "'";
            _sqlToExecute += "," + saveThis.BookingRecordPin + ")";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("BookingRecord - Save failed");
        }

        public void Delete(BookingRecord deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[BookingRecord] WHERE Id = " + deleteThis.Id.ToString();

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
