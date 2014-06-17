using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.Domain
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
    public interface IBookingRecordRepository : IRepository<IBookingRecord>
    {
        /// <summary>
        /// Retrieves a booking record by the unique Id created when it is saved
        /// </summary>
        /// <param name="BookingRecordUniqueId">Unique Id to query for</param>
        /// <returns>A booking record</returns>
        IBookingRecord GetByBookingRecordUniqueId(int BookingRecordUniqueId);
    }
}
