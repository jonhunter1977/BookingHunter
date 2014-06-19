using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a facility
    /// </summary>
    public interface IFacility : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// FacilityBookAheadDays name column
        /// </summary>
        int FacilityBookAheadDays { get; set; }
    }
}
