using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    public class FacilityScheduleLogic : IFacilityScheduleLogic
    {
        private Lazy<DataAccess> _da = new Lazy<DataAccess>();

        public FacilityScheduleLogic() { }

        public void CreateFacilitySchedule(FacilitySchedule facilitySchedule)
        {
            //Save the facility schedule record
            var insertedRowId = _da.Value.FacilitySchedule.Insert(facilitySchedule);

            if (insertedRowId == 0)
            {
                throw new Exception("Failed to create facility schdule record");
            }
            else
            {
                facilitySchedule.Id = insertedRowId;
            }
        }

        public IEnumerable<FacilitySchedule> Search(Func<FacilitySchedule, bool> searchCriteria)
        {
            return _da.Value.FacilitySchedule.GetAll().Where(searchCriteria);
        }
    }
}
