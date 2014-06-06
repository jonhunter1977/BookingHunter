using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a facility
    /// </summary>
    public struct Facility : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FacilityBookAheadDays name column
        /// </summary>
        public int FacilityBookAheadDays { get; set; }
    }
}
