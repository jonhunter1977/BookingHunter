using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using BH.Domain;
using BH.DataAccessLayer;

namespace BH.DataAccessLayer.LinqToSql
{
    /// <summary>
    /// Class for getting address data from the database
    /// </summary>
    internal class BookingRecordRepository : IBookingRecordRepository
    {
        private BookingDataContext _db;

        public BookingRecordRepository(string bookingConnectionString)
        {
            if (bookingConnectionString.Equals(string.Empty))
                throw new Exception("Booking Database query engine is not connected");
            else
                _db = new BookingDataContext(bookingConnectionString);
        }

        public IQueryable<BookingRecord> GetAll()
        {
            try
            {
                var a = from b in _db.Bookings
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BookingRecord GetById(int id)
        {
            try
            {
                var a = from b in _db.Bookings
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(BookingRecord saveThis)
        {
           try
           {
               _db.Bookings.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(BookingRecord updateThis)
        {
            try
            {
                _db.Bookings.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(BookingRecord deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Bookings.DeleteOnSubmit(deleteThis);
                    _db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BookingRecord GetByBookingRecordUniqueId(string BookingRecordUniqueId)
        {
            try
            {
                var a = from b in _db.Bookings
                        where b.BookingRecordUniqueId == BookingRecordUniqueId
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
