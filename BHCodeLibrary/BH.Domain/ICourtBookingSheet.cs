using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a court booking sheet
    /// </summary>
    public interface ICourtBookingSheet : IDbItentity
    {
        /// <summary>
        /// Address Id - identity column
        /// </summary>
        int Id { get; set; }    

        /// <summary>
        /// Court booking start time
        /// </summary>
        int CourtBookingStartTime { get; set; }

        /// <summary>
        /// Court booking end time
        /// </summary>
        int CourtBookingEndTime { get; set; }

        /// <summary>
        /// Court booking date
        /// </summary>
        DateTime CourtBookingDate { get; set; }
    }
}
