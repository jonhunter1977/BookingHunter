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
    }
}
