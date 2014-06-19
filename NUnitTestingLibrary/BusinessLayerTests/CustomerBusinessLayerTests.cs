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
    }
}
