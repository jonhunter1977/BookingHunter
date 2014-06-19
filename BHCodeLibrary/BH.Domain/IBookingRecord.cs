using System;

namespace BH.Domain
{

    /// <summary>
    /// Data for a booking record
    /// </summary>
    public interface IBookingRecord : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Time arrived name column
        /// </summary>
        int TimeArrived { get; set; }

        /// <summary>
        /// Arrival registration method column
        /// </summary>
        ArrivalRegistrationMethod ArrivalRegistrationMethod { get; set; }

        /// <summary>
        /// Booking status name column
        /// </summary>
        BookingStatus BookingStatus { get; set; }

        /// <summary>
        /// Booking record unique Id column
        /// </summary>
        string BookingRecordUniqueId { get; set; }

        /// <summary>
        /// Booking record PIN column
        /// </summary>
        int BookingRecordPin { get; set; }
    }
}
