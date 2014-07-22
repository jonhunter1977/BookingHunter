using System;
using BH.Domain;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Facility business logic
    /// </summary>
    public interface IFacilityLogic : IGenericLogic<Facility>
    {
        /// <summary>
        /// Logic to create a new facility
        /// </summary>
        void CreateFacility(int locationId, ref Facility facility);

        /// <summary>
        /// Link a facility schedule to a facility.  Only one facility schedule can be linked to a facility
        /// </summary>
        /// <param name="facilityId"> </param>
        /// <param name="facilityScheduleId"> </param>
        void LinkFacilityScheduleToFacility(int facilityId, int facilityScheduleId);

        /// <summary>
        /// Creates a court and links it to the facility
        /// </summary>
        /// <param name="facilityId">The ID of the facility to link the court to</param>
        /// <param name="court">The court to create</param>
        void CreateCourtAndLinkToFacility(int facilityId, ref Court court);
    }
}
