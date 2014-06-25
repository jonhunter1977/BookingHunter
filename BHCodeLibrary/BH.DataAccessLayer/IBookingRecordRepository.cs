using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Interface for interacting with the booking records in the database
    /// </summary>
    public interface IBookingRecordRepository : IRepository<BookingRecord>
    {
        /// <summary>
        /// Retrieves a booking record by the unique Id created when it is saved
        /// </summary>
        /// <param name="BookingRecordUniqueId">Unique Id to query for</param>
        /// <returns>A booking record</returns>
        BookingRecord GetByBookingRecordUniqueId(string BookingRecordUniqueId);
    }
}
