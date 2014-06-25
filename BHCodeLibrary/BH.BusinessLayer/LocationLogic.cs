using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    public class LocationLogic : ILocationLogic
    {
        private DataAccessType _accessType;
        private string _cfgConnectionString;
        private string _contactConnectionString;
        private string _linksConnectionString;
        private IDataAccess _da;

        /// <summary>
        /// Create access to location logic using required connection strings and access type
        /// </summary>
        /// <param name="accessType">The database access type</param>
        /// <param name="cfgConnectionString">Connection string to use to cfg data source</param>
        /// <param name="contactConnectionString">Connection string to use to contact data source</param>
        /// <param name="linksConnectionString">Connection string to use to links data source</param>
        public LocationLogic(
            DataAccessType accessType,
            string cfgConnectionString,
            string contactConnectionString,
            string linksConnectionString)
        {
            _accessType = accessType;
            _cfgConnectionString = cfgConnectionString;
            _contactConnectionString = contactConnectionString;
            _linksConnectionString = linksConnectionString;

            _da = new DataAccess
            {
                ContactConnectionString = _contactConnectionString,
                CfgConnectionString = _cfgConnectionString,
                LinksConnectionString = _linksConnectionString,
                AccessType = _accessType
            };
        }

        public IDataAccess da
        {
            get 
            {
                return _da;
            }
        }

        public void CreateLocation(int customerId, ref Location location, ref Address address)
        {
            //Check a valid customer id has been passed
            try
            {
                var customer = _da.Customer.GetById(customerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Create a new location record
            var insertedRowId = _da.Location.Insert(location);

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
            insertedRowId = _da.Link.Insert(customerLocationLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the location record
                _da.Location.Delete(location);

                throw new Exception("Failed to create customer location link record, transaction rolled back");
            }

            //Save the address record
            bool addressCreated = false;
            if (address.Id == 0)
            {
                //Create a new address record
                insertedRowId = _da.Address.Insert(address);

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
            insertedRowId = _da.Link.Insert(locationAddressLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the location record
                _da.Location.Delete(location);

                if (addressCreated)
                    _da.Address.Delete(address);

                throw new Exception("Failed to create location address link record, transaction rolled back");
            }
        }

        public void UpdateLocation(ref Location location)
        {
            _da.Location.Update(location);
        }

        public IEnumerable<Location> Search(Func<Location, bool> searchCriteria)
        {
            var objList = _da.Location.GetAll();
            var filteredObjList = objList.Where(searchCriteria);

            return filteredObjList;
        }
    }
}
