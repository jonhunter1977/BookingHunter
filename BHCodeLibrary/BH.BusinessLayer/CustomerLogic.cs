using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    internal class CustomerLogic : ICustomerLogic
    {
        private Lazy<DataAccess> _da = new Lazy<DataAccess>();

        public CustomerLogic() { }

        public void CreateCustomer(ref Customer customer, ref Address address)
        {
            //Save the customer record
            var insertedRowId = _da.Value.Customer.Insert(customer);

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
                insertedRowId = _da.Value.Address.Insert(address);

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
            insertedRowId = _da.Value.Link.Insert(customerAddressLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the customer record
                _da.Value.Customer.Delete(customer);

                //Delete the address record
                _da.Value.Address.Delete(address);

                throw new Exception("Failed to create customer address link record, transaction rolled back");
            }
        }

        public void UpdateCustomer(ref Customer customer)
        {
            _da.Value.Customer.Update(customer);            
        }

        public Customer FindCustomerById(int id)
        {
            try
            {
                var customer = _da.Value.Customer.GetById(id);
                return customer;
            }
            catch (Exception)
            {
                throw new Exception("Customer id " + id.ToString() + " does not exist");
            }           
        }

        public IEnumerable<Customer> Search(Func<Customer, bool> searchCriteria)
        {
            var objList = _da.Value.Customer.GetAll();
            var filteredObjList = objList.Where(searchCriteria);

            return filteredObjList;
        }

        public Address GetCustomerAddress(Customer customer)
        {
            var customerAddressLink =
                _da.Value.Link.GetChildLinkObjectId
                (
                    LinkType.Customer, 
                    customer.Id, 
                    LinkType.Address
                );

            var linkRecord = customerAddressLink.Single();

            if (linkRecord.ChildLinkId == null)
                throw new Exception("No address is linked to customer ID : " + customer.Id);

            var address = _da.Value.Address.GetById(linkRecord.ChildLinkId.Value);

            return address;
        }

        public IEnumerable<Location> GetCustomerLocations(Customer customer)
        {
            var linkObjs =
                _da.Value.Link.GetChildLinkObjectId
                (
                    LinkType.Customer, 
                    customer.Id, 
                    LinkType.Location
                ).AsEnumerable();

            var locationObjs = _da.Value.Location.GetAll().AsEnumerable();

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
