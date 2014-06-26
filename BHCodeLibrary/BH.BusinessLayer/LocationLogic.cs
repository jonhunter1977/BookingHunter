using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    internal class LocationLogic : ILocationLogic
    {
        private static LocationLogic _instance = null;
        private static Object _mutex = new Object();

        static LocationLogic() { }
        LocationLogic() { }

        /// <summary>
        /// Return singleton instance of customer logic class
        /// </summary>
        public static LocationLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex) //for thread safety
                    {
                        if (_instance == null)
                        {
                            _instance = new LocationLogic();
                        }
                    }
                }

                return _instance;
            }
        }

        public void CreateLocation(int customerId, ref Location location, ref Address address)
        {
            //Check a valid customer id has been passed
            try
            {
                var customer = BLDataAccess.Instance.Customer.GetById(customerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Create a new location record
            var insertedRowId = BLDataAccess.Instance.Location.Insert(location);

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
            insertedRowId = BLDataAccess.Instance.Link.Insert(customerLocationLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the location record
                BLDataAccess.Instance.Location.Delete(location);

                throw new Exception("Failed to create customer location link record, transaction rolled back");
            }

            //Save the address record
            bool addressCreated = false;
            if (address.Id == 0)
            {
                //Create a new address record
                insertedRowId = BLDataAccess.Instance.Address.Insert(address);

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
            insertedRowId = BLDataAccess.Instance.Link.Insert(locationAddressLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the location record
                BLDataAccess.Instance.Location.Delete(location);

                if (addressCreated)
                    BLDataAccess.Instance.Address.Delete(address);

                throw new Exception("Failed to create location address link record, transaction rolled back");
            }
        }

        public void UpdateLocation(ref Location location)
        {
            BLDataAccess.Instance.Location.Update(location);
        }

        public IEnumerable<Location> Search(Func<Location, bool> searchCriteria)
        {
            var objList = BLDataAccess.Instance.Location.GetAll();
            var filteredObjList = objList.Where(searchCriteria);

            return filteredObjList;
        }
    }
}
