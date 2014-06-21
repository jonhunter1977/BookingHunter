using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using System.Data.Common;
using BH.BusinessLayer;
using BH.Domain;

namespace NUnitTestingLibrary
{
    [TestFixture]
    public class CustomerBusinessLayerTests
    {
        [Test]
        public void CreateCustomer()
        {
            var customer = new Customer
            {
                CustomerName = "Neston Cricket Club"
            };

            var address = new Address
            {
                Address1 = "Station Road",
                Town = "Neston",
                County = "Cheshire",
                PostCode = "CH64 6QJ"
            };

            var cl = new CustomerLogic
            (
                DataAccessType.SqlServer,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            try
            {
                cl.CreateCustomer(ref customer, ref address);
            }
            catch(Exception ex)
            {
                Assert.Fail("Create customer threw an exception : " + ex.Message);
            }

            if (customer.Id == 0)
                Assert.Fail("Customer Id did not get set, it is 0");

            if(address.Id == 0)
                Assert.Fail("Address Id did not get set, it is 0");    
        }

        [Test]
        public void RetrieveCustomer()
        {
            var cl = new CustomerLogic
            (
                DataAccessType.SqlServer,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            var customerList = cl.Search(c => c.CustomerName == "Neston Cricket Club");
            var customerCount = customerList.Count(c => c.CustomerName == "Neston Cricket Club");

            if (customerCount > 0)
                Assert.Pass(customerCount.ToString() + " customers found");
            else
                Assert.Fail("No customers found");
        }

        [Test]
        public void UpdateCustomer()
        {
            var cl = new CustomerLogic
            (
                DataAccessType.SqlServer,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            var customerList = cl.Search(c => c.CustomerName == "Neston Cricket Club");
            var customer = customerList.First(c => c.CustomerName == "Neston Cricket Club");

            customer.CustomerName = "Neston Squash Club";
            cl.UpdateCustomer(ref customer);
            
        }
    }
}
