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
        public void a_Get_Application_Settings()
        {
            //var exeConfigPath = this.GetType().Assembly.Location;
            var bookingConnectionString = DataAccessSettings.BookingConnectionString;
            Assert.AreEqual(bookingConnectionString, @"Data Source=DLTGFST4Q1\SQLEXPRESS;Initial Catalog=sys_booking;User Id=sa;Password=info51987!;");
        }

        [Test]
        public void b_Create_Neston_Cricket_Club_As_New_Customer()
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
        public void c_Create_Neston_Squash_As_Location_Linked_To_Neston_Cricket_Club()
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
        public void d_Create_Squash_Facility_Linked_To_Neston_Squash()
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

            var facility = new Facility()
            {
                FacilityBookAheadDays = 14,
                FacilityDescription = "Squash"
            };

            try
            {
                TestingSetupClass._logic.FacilityLogic.CreateFacility(location.Id, ref facility);
            }
            catch (Exception ex)
            {
                Assert.Fail("Create facility threw an exception : {0}", ex.Message);
            }
            
            if(facility.Id == 0)
                Assert.Fail("Facility Id did not get set, it is 0");
            else
            {
                Assert.Pass("Facility created with Id : {0}" , facility.Id);
            }
        }

        [Test]
        public void e_Search_For_Squash_Facility_And_Check_Book_Ahead_Days_Equals_14()
        {
            var facilityList = TestingSetupClass._logic.FacilityLogic.Search(f => f.FacilityDescription == "Squash");
            var facilityCount = facilityList.Count(f => f.FacilityDescription == "Squash");

            if (facilityCount == 0)
                Assert.Fail("Squash facility was not found");

            var facility = facilityList.First(f => f.FacilityDescription == "Squash");

            if(facility.FacilityBookAheadDays == 14)
                Assert.Pass("Facility {0} has {1} book ahead days set", facility.FacilityDescription,facility.FacilityBookAheadDays);
            else
                Assert.Fail("Facility {0} has {1} book ahead days set", facility.FacilityDescription, facility.FacilityBookAheadDays);          
        }
    }
}
