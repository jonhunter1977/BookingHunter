using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a booking record
    /// </summary>
    public class BookingRecord : IBookingRecord, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        [Column
            (
                Name = "Id",
                IsPrimaryKey = true,
                IsDbGenerated = true
            )
        ]
        public int Id { get; set; }

        /// <summary>
        /// Time arrived name column
        /// </summary>
        [Column(Name = "TimeArrived")]
        public int TimeArrived { get; set; }

        /// <summary>
        /// Arrival registration method column
        /// </summary>
        [Column(Name = "ArrivalRegistrationMethod")]
        public ArrivalRegistrationMethod ArrivalRegistrationMethod { get; set; }

        /// <summary>
        /// Booking status name column
        /// </summary>
        [Column(Name = "BookingStatus")]
        public BookingStatus BookingStatus { get; set; }

        /// <summary>
        /// Booking record unique Id column
        /// </summary>
        [Column(Name = "BookingRecordUniqueId")]
        public string BookingRecordUniqueId { get; set; }

        /// <summary>
        /// Booking record PIN column
        /// </summary>
        [Column(Name = "BookingRecordPin")]
        public int BookingRecordPin { get; set; }
    }
}
