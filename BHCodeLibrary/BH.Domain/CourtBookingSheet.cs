using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a court booking sheet
    /// </summary>
    [Table(Name = "CourtBookingSheet")]
    public class CourtBookingSheet : ICourtBookingSheet, IDbItentity
    {
        /// <summary>
        /// Address Id - identity column
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
        /// Court booking start time
        /// </summary>
        [Column(Name = "CourtBookingStartTime")]
        public int CourtBookingStartTime { get; set; }

        /// <summary>
        /// Court booking end time
        /// </summary>
        [Column(Name = "CourtBookingEndTime")]
        public int CourtBookingEndTime { get; set; }

        /// <summary>
        /// Court booking date
        /// </summary>
        [Column(Name = "CourtBookingDate")]
        public DateTime CourtBookingDate { get; set; }
    }
}
