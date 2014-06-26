using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    public class FacilityLogic : IFacilityLogic
    {
        public void CreateFacility(int locationId, ref Facility facility)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Facility> Search(Func<Facility, bool> searchCriteria)
        {
            throw new NotImplementedException();
        }
    }
}
