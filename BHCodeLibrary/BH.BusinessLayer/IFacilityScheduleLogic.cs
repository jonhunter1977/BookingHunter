using System;
using BH.Domain;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Facility schedule business logic
    /// </summary>
    public interface IFacilityScheduleLogic : IGenericLogic<FacilitySchedule>
    {
        /// <summary>
        /// Logic to create a new facility schedule
        /// </summary>
        void CreateFacilitySchedule(FacilitySchedule facilitySchedule);
    }
}
