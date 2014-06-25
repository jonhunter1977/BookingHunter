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
    internal class CourtBookingSheetRepository : ICourtBookingSheetRepository
    {
        private BookingDataContext _db;

        public CourtBookingSheetRepository(string bookingConnectionString)
        {
            if (bookingConnectionString.Equals(string.Empty))
                throw new Exception("Booking Database query engine is not connected");
            else
                _db = new BookingDataContext(bookingConnectionString);
        }

        public IQueryable<CourtBookingSheet> GetAll()
        {
            try
            {
                var a = from b in _db.CourtBookingSheets
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CourtBookingSheet GetById(int id)
        {
            try
            {
                var a = from b in _db.CourtBookingSheets
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(CourtBookingSheet saveThis)
        {
           try
           {
               _db.CourtBookingSheets.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(CourtBookingSheet updateThis)
        {
            try
            {
                _db.CourtBookingSheets.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(CourtBookingSheet deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.CourtBookingSheets.DeleteOnSubmit(deleteThis);
                    _db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
