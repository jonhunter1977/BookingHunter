using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    internal class CustomerLogic : ICustomerLogic
    {
        private static CustomerLogic _instance = null;
        private static Object _mutex = new Object();

        static CustomerLogic() { }
        CustomerLogic() { }

        /// <summary>
        /// Return singleton instance of customer logic class
        /// </summary>
        public static CustomerLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex) //for thread safety
                    {
                        if (_instance == null)
                        {
                            _instance = new CustomerLogic();
                        }
                    }
                }

                return _instance;
            }
        }

        public void CreateCustomer(ref Customer customer, ref Address address)
        {
            //Save the customer record
            var insertedRowId = BLDataAccess.Instance.Customer.Insert(customer);

            if (insertedRowId == 0)
            {
                throw new Exception("Failed to create customer record");
            }
            else
            {
                customer.Id = insertedRowId;
            }

            //Save the address record
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
                }
            }

            //Link the customer and address records
            var customerAddressLink = new LinkObjectMaster()
            {
                MasterLinkId = customer.Id,
                MasterLinkType = LinkType.Customer,
                ChildLinkId = address.Id,
                ChildLinkType = LinkType.Address
            };

            //Save the link record
            insertedRowId = BLDataAccess.Instance.Link.Insert(customerAddressLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the customer record
                BLDataAccess.Instance.Customer.Delete(customer);

                //Delete the address record
                BLDataAccess.Instance.Address.Delete(address);

                throw new Exception("Failed to create customer address link record, transaction rolled back");
            }
        }

        public void UpdateCustomer(ref Customer customer)
        {
            BLDataAccess.Instance.Customer.Update(customer);            
        }

        public Customer FindCustomerById(int id)
        {
            try
            {
                var customer = BLDataAccess.Instance.Customer.GetById(id);
                return customer;
            }
            catch (Exception)
            {
                throw new Exception("Customer id " + id.ToString() + " does not exist");
            }           
        }

        public IEnumerable<Customer> Search(Func<Customer, bool> searchCriteria)
        {
            var objList = BLDataAccess.Instance.Customer.GetAll();
            var filteredObjList = objList.Where(searchCriteria);

            return filteredObjList;
        }

        public Address GetCustomerAddress(Customer customer)
        {
            var customerAddressLink =
                BLDataAccess.Instance.Link.GetChildLinkObjectId
                (
                    LinkType.Customer, 
                    customer.Id, 
                    LinkType.Address
                );

            var linkRecord = customerAddressLink.Single();

            if (linkRecord.ChildLinkId == null)
                throw new Exception("No address is linked to customer ID : " + customer.Id);

            var address = BLDataAccess.Instance.Address.GetById(linkRecord.ChildLinkId.Value);

            return address;
        }

        public IEnumerable<Location> GetCustomerLocations(Customer customer)
        {
            var linkObjs =
                BLDataAccess.Instance.Link.GetChildLinkObjectId
                (
                    LinkType.Customer, 
                    customer.Id, 
                    LinkType.Location
                ).AsEnumerable();

            var locationObjs = BLDataAccess.Instance.Location.GetAll().AsEnumerable();

            //var filteredLocations =
            //    from location in _da.Location.GetAll().AsEnumerable()
            //    join link in linkObjs
            //    on location.Id equals link.ChildLinkId
            //    select location;

            var filteredLocations =
                locationObjs.Where(l => linkObjs.Any(x => x.ChildLinkId == l.Id));                

            return filteredLocations.AsEnumerable();
        }
    }
}
