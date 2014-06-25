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
        public void a_CreateNestonCricketClubAsNewCustomer()
        {
            var customer = new Customer
            {
                CustomerName = "Neston Cricket Club",
                Active = true,
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
                DataAccessType.LinqToSql,
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
        }

        [Test]
        public void b_CreateNestonSquashAsLocationLinkedToNestonCricketClub()
        {
            var cl = new CustomerLogic
            (
                DataAccessType.LinqToSql,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            var customerList = cl.Search(c => c.CustomerName == "Neston Cricket Club");
            var customerCount = customerList.Count(c => c.CustomerName == "Neston Cricket Club");

            if (customerCount == 0)
                Assert.Fail("No customers found");

            var customer = customerList.First(c => c.CustomerName == "Neston Cricket Club");

            var address = cl.GetCustomerAddress(customer);

            if (!address.Address1.Equals("Station Road"))
                Assert.Fail("Incorrect address returned");

            var ll = new LocationLogic
            (
            DataAccessType.LinqToSql,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            var location = new Location()
            {
                LocationDescription = "Neston Squash Club"
            };

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












        [Ignore]
        public void b_RetrieveCustomer()
        {

        }

        [Ignore]
        public void c_UpdateCustomer()
        {
            var cl = new CustomerLogic
            (
                DataAccessType.LinqToSql,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            var customerList = cl.Search(c => c.CustomerName == "Neston Cricket Club");
            var customer = customerList.First(c => c.CustomerName == "Neston Cricket Club");

            customer.CustomerName = "Neston Squash Club";
            cl.UpdateCustomer(ref customer);
            
        }

        [Ignore]
        public void d_UpdateLocation()
        {
            var logic = new LocationLogic
            (
                DataAccessType.LinqToSql,
                BHDataAccess.cfgConnection.ConnectionString,
                BHDataAccess.contactConnection.ConnectionString,
                BHDataAccess.linksConnection.ConnectionString
            );

            var objList = logic.Search(a => a.LocationDescription  == "Neston Squash Club");
            var obj = objList.First(a => a.LocationDescription == "Neston Squash Club");

            obj.LocationDescription = "Neston Squash Courts";
            logic.UpdateLocation(ref obj);

        }
    }
}
