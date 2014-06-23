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
    public class BusinessLayerTests
    {
        [Test]
        public void CreateCustomerAndLocation()
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

                if (customer.Id == 0)
                    Assert.Fail("Customer Id did not get set, it is 0");

                if (address.Id == 0)
                    Assert.Fail("Address Id did not get set, it is 0");
            }
            catch(Exception ex)
            {
                Assert.Fail("Create customer threw an exception : " + ex.Message);
            }

            var location = new Location()
            {
                LocationDescription = "Neston Squash Club"
            };

            var ll = new LocationLogic
            (
                DataAccessType.SqlServer,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            try
            {
                ll.CreateLocation(customer.Id, ref location, ref address);

                if (location.Id == 0)
                    Assert.Fail("Location Id did not get set, it is 0");
            }
            catch (Exception ex)
            {
                Assert.Fail("CreateLocation threw an error : " + ex.Message);
            }
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
