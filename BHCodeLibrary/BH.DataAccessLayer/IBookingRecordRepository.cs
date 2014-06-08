using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// The different ways a customer can update a booking to say they have arrived
    /// </summary>
    public enum ArrivalRegistrationMethod
    {
        NotArrived = 0,
        BookingReference = 1,
        BookingPIN = 2,
        QRCode = 3
    }

    /// <summary>
    /// The different statuses of a booking
    /// </summary>
    public enum BookingStatus
    {
        Draft = 1,
        Live = 2,
        Cancelled = 3
    }

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
        BookingRecord GetByBookingRecordUniqueId(int BookingRecordUniqueId);
    }
}
