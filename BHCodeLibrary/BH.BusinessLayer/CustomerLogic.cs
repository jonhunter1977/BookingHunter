using System;
using System.Linq;
using BH.Domain;
using BH.DataAccessLayer;
using System.Collections.Generic;

namespace BH.BusinessLayer
{
    public class CustomerLogic : ICustomerLogic
    {
        private DataAccessType _accessType;
        private string _cfgConnectionString;
        private string _contactConnectionString;
        private string _linksConnectionString;
        private IDataAccess _da;

        /// <summary>
        /// Create access to customer logic using required connection strings and access type
        /// </summary>
        /// <param name="accessType">The database access type</param>
        /// <param name="cfgConnectionString">Connection string to use to cfg data source</param>
        /// <param name="contactConnectionString">Connection string to use to contact data source</param>
        /// <param name="linksConnectionString">Connection string to use to links data source</param>
        public CustomerLogic(
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

        public void CreateCustomer(ref Customer customer, ref Address address)
        {
            //Validate the data passed
            if (customer.CustomerName.Equals(string.Empty))
                throw new Exception("Customer name cannot be blank");

            if (address.Address1.Equals(string.Empty))
                throw new Exception("Address 1 field cannot be blank");

            if (address.Town.Equals(string.Empty))
                throw new Exception("Town field cannot be blank");

            if (address.County.Equals(string.Empty))
                throw new Exception("County field cannot be blank");

            if (address.PostCode.Equals(string.Empty))
                throw new Exception("PostCode field cannot be blank");

            //Save the customer record
            var insertedRowId = _da.Customer.Insert(customer);
            customer.Id = insertedRowId;

            //Save the address record
            insertedRowId = _da.Address.Insert(address);
            address.Id = insertedRowId;

            //Link the customer and address records
            var customerAddressLink = new LinkObjectMaster()
            {
                MasterLinkId = customer.Id,
                MasterLinkType = LinkType.Customer,
                ChildLinkId = address.Id,
                ChildLinkType = LinkType.Address
            };

            //Save the link record
            insertedRowId = _da.Link.Insert(customerAddressLink);

            if (insertedRowId == 0)
            {
                //Roll back the inserts as it's failed
                //Delete the customer record
                _da.Customer.Delete(customer);

                //Delete the address record
                _da.Address.Delete(address);

                throw new Exception("Failed to create link record, transaction rolled back");
            }
        }

        public void UpdateCustomer(ref Customer customer)
        {
            _da.Customer.Update(customer);            
        }

        public Customer FindCustomerById(int id)
        {
            try
            {
                var customer = _da.Customer.GetById(id);
                return customer;
            }
            catch (Exception)
            {
                throw new Exception("Customer id " + id.ToString() + " does not exist");
            }           
        }

        public IEnumerable<Customer> Search(Func<Customer, bool> searchCriteria)
        {
            var customerList = _da.Customer.GetAll();
            var filteredCustomerList = customerList.Where(searchCriteria);

            return filteredCustomerList;
        }
    }
}
