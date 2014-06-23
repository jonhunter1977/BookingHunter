using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a court booking sheet
    /// </summary>
    public class CourtBookingSheet : ICourtBookingSheet, IDbItentity
    {
        /// <summary>
        /// Address Id - identity column
        /// </summary>
        public int Id { get; set; }    

        /// <summary>
        /// Court booking start time
        /// </summary>
        public int CourtBookingStartTime { get; set; }

        /// <summary>
        /// Court booking end time
        /// </summary>
        public int CourtBookingEndTime { get; set; }

        /// <summary>
        /// Court booking date
        /// </summary>
        public DateTime CourtBookingDate { get; set; }
    }
}
