using System;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Interface for interacting with the facility schedules in the database
    /// </summary>
    public interface IFacilityScheduleRepository : IRepository<FacilitySchedule>
    {

    }
}
