using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a booking record
    /// </summary>
    public struct BookingRecord : IBookingRecord, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Time arrived name column
        /// </summary>
        public int TimeArrived { get; set; }

        /// <summary>
        /// Arrival registration method column
        /// </summary>
        public ArrivalRegistrationMethod ArrivalRegistrationMethod { get; set; }

        /// <summary>
        /// Booking status name column
        /// </summary>
        public BookingStatus BookingStatus { get; set; }

        /// <summary>
        /// Booking record unique Id column
        /// </summary>
        public string BookingRecordUniqueId { get; set; }

        /// <summary>
        /// Booking record PIN column
        /// </summary>
        public int BookingRecordPin { get; set; }
    }
}
