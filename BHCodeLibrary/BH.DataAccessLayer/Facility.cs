using System;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a facility
    /// </summary>
    public struct Facility : IFacility, IDbItentity
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
