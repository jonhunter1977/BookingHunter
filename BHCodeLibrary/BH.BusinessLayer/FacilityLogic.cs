using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    public class FacilityLogic : IFacilityLogic
    {
        private Lazy<DataAccess> _da = new Lazy<DataAccess>();

        public FacilityLogic() { }

        public void CreateFacility(int locationId, ref Facility facility)
        {
            //Save the facility record
            var insertedRowId = _da.Value.Facility.Insert(facility);

            if (insertedRowId == 0)
            {
                throw new Exception("Failed to create facility record");
            }
            else
            {
                facility.Id = insertedRowId;
            }

            //Link the facility and location records
            var locationFacilityLink = new LinkObjectMaster()
            {
                MasterLinkId = locationId,
                MasterLinkType = LinkType.Location,
                ChildLinkId = facility.Id,
                ChildLinkType = LinkType.Facility
            };

            //Save the link record
            insertedRowId = _da.Value.Link.Insert(locationFacilityLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the facility record
                _da.Value.Facility.Delete(facility);

                throw new Exception("Failed to create location facility link record, transaction rolled back");
            }
        }

        public IEnumerable<Facility> Search(Func<Facility, bool> searchCriteria)
        {
            return _da.Value.Facility.GetAll().Where(searchCriteria);
        }
    }
}
