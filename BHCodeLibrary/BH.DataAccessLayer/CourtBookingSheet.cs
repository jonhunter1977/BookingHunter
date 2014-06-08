using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a court booking sheet
    /// </summary>
    public struct CourtBookingSheet : IDbItentity
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
