using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    internal class LocationLogic : ILocationLogic
    {
        private Lazy<DataAccess> _da = new Lazy<DataAccess>();

        public LocationLogic() { }

        public void CreateLocation(int customerId, ref Location location, ref Address address)
        {
            //Check a valid customer id has been passed
            try
            {
                var customer = _da.Value.Customer.GetById(customerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Create a new location record
            var insertedRowId = _da.Value.Location.Insert(location);

            if (insertedRowId == 0)
            {
                throw new Exception("Failed to create location record");
            }
            else
            {
                location.Id = insertedRowId;
            }

            //Link the customer and location
            var customerLocationLink = new LinkObjectMaster()
            {
                MasterLinkId = customerId,
                MasterLinkType = LinkType.Customer,
                ChildLinkId = location.Id,
                ChildLinkType = LinkType.Location
            };

            //Save the link record
            insertedRowId = _da.Value.Link.Insert(customerLocationLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the location record
                _da.Value.Location.Delete(location);

                throw new Exception("Failed to create customer location link record, transaction rolled back");
            }

            //Save the address record
            var addressCreated = false;
            if (address.Id == 0)
            {
                //Create a new address record
                insertedRowId = _da.Value.Address.Insert(address);

                if (insertedRowId == 0)
                {
                    throw new Exception("Failed to create address record");
                }
                else
                {
                    address.Id = insertedRowId;
                    addressCreated = true;
                }                
            }

            //Link the location and address records
            var locationAddressLink = new LinkObjectMaster()
            {
                MasterLinkId = location.Id,
                MasterLinkType = LinkType.Location,
                ChildLinkId = address.Id,
                ChildLinkType = LinkType.Address
            };

            //Save the link record
            insertedRowId = _da.Value.Link.Insert(locationAddressLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the location record
                _da.Value.Location.Delete(location);

                if (addressCreated)
                    _da.Value.Address.Delete(address);

                throw new Exception("Failed to create location address link record, transaction rolled back");
            }
        }

        public void UpdateLocation(ref Location location)
        {
            _da.Value.Location.Update(location);
        }

        public IEnumerable<Location> Search(Func<Location, bool> searchCriteria)
        {
            return _da.Value.Location.GetAll().Where(searchCriteria);
        }
    }
}
