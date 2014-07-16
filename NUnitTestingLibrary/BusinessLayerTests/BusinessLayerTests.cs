using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using System.Data.Common;
using BH.BusinessLayer;
using BH.Domain;
using BH.DataAccessLayer;

namespace NUnitTestingLibrary
{
    [TestFixture]
    public class BusinessLayerTests
    {
        [Test]
        public void a_GetApplicationSettings()
        {
            //var exeConfigPath = this.GetType().Assembly.Location;
            var bookingConnectionString = DataAccessSettings.BookingConnectionString;
            Assert.AreEqual(bookingConnectionString, @"Data Source=DLTGFST4Q1\SQLEXPRESS;Initial Catalog=sys_booking;User Id=sa;Password=info51987!;");
        }

        [Test]
        public void b_CreateNestonCricketClubAsNewCustomer()
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

            try
            {
                TestingSetupClass._logic.CustomerLogic.CreateCustomer(ref customer, ref address);

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
        public void c_CreateNestonSquashAsLocationLinkedToNestonCricketClub()
        {
            var customerList = TestingSetupClass._logic.CustomerLogic.Search(c => c.CustomerName == "Neston Cricket Club");
            var customerCount = customerList.Count(c => c.CustomerName == "Neston Cricket Club");

            if (customerCount == 0)
                Assert.Fail("No customers found");

            var customer = customerList.First(c => c.CustomerName == "Neston Cricket Club");

            var address = TestingSetupClass._logic.CustomerLogic.GetCustomerAddress(customer);

            if (!address.Address1.Equals("Station Road"))
                Assert.Fail("Incorrect address returned");

            var location = new Location()
            {
                LocationDescription = "Neston Squash Club"
            };

            try
            {
                TestingSetupClass._logic.LocationLogic.CreateLocation(customer.Id, ref location, ref address);

                if (location.Id == 0)
                    Assert.Fail("Location Id did not get set, it is 0");
            }
            catch (Exception ex)
            {
                Assert.Fail("CreateLocation threw an error : " + ex.Message);
            }
        }

        [Test]
        public void d_CreateSquashFacilityLinkedToNestonSquash()
        {
            var customerList = TestingSetupClass._logic.CustomerLogic.Search(c => c.CustomerName == "Neston Cricket Club");
            var customerCount = customerList.Count(c => c.CustomerName == "Neston Cricket Club");

            if (customerCount == 0)
                Assert.Fail("No customers found");

            var customer = customerList.First(c => c.CustomerName == "Neston Cricket Club");

            var locationList = TestingSetupClass._logic.CustomerLogic.GetCustomerLocations(customer);

            var location = locationList.FirstOrDefault(l => l.LocationDescription == "Neston Squash Club");

            if (location == null)
                Assert.Fail("Did not find location linked to customer");
            else
                Assert.Pass("Found " + location.LocationDescription);

            var facility = new Facility()
            {
                FacilityBookAheadDays = 14,
                FacilityDescription = "Squash"
            };
        }
    }
}
