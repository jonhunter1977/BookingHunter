using System;
using System.Data.SqlClient;
using System.Diagnostics;
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
        public void a_Get_Application_Settings_And_Check_Booking_Connection_String_Is_Correct()
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
            
            if (customerList == null)
                Assert.Fail("{0} customer was not found", "Neston Cricket Club");

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
            
            if (customerList == null)
                Assert.Fail("{0} customer was not found", "Neston Cricket Club");

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

            if (facilityList == null)
                Assert.Fail("{0} facility was not found", "Squash");

            var facility = facilityList.First(f => f.FacilityDescription == "Squash");

            if(facility.FacilityBookAheadDays == 14)
                Assert.Pass("Facility {0} has {1} book ahead days set", facility.FacilityDescription,facility.FacilityBookAheadDays);
            else
                Assert.Fail("Facility {0} has {1} book ahead days set", facility.FacilityDescription, facility.FacilityBookAheadDays);          
        }

        [Test]
        public void f_Create_Facility_Schedule_Master()
        {
            var facilitySchedule = new FacilitySchedule()
                {
                    FacilityScheduleDescription = "Squash 40 minute slots start at 9am finish at 10pm",
                    StartMinuteMonday = 540,
                    EndMinuteMonday = 1320,
                    MondayFacilityBookLength = 40,
                    StartMinuteTuesday = 540,
                    EndMinuteTuesday = 1320,
                    TuesdayFacilityBookLength = 40,
                    StartMinuteWednesday = 540,
                    EndMinuteWednesday = 1320,
                    WednesdayFacilityBookLength = 40,
                    StartMinuteThursday = 540,
                    EndMinuteThursday = 1320,
                    ThursdayFacilityBookLength = 40,
                    StartMinuteFriday = 540,
                    EndMinuteFriday = 1320,
                    FridayFacilityBookLength = 40,
                    StartMinuteSaturday = 540,
                    EndMinuteSaturday = 1320,
                    SaturdayFacilityBookLength = 40,
                    StartMinuteSunday = 540,
                    EndMinuteSunday = 1320,
                    SundayFacilityBookLength = 40
                };

            TestingSetupClass._logic.FacilityScheduleLogic.CreateFacilitySchedule(facilitySchedule);

            if(facilitySchedule.Id == 0)
                Assert.Fail("Facility schedule was not created, ID is 0");
            else
                Assert.Pass("Facility schdule created with ID of {0}", facilitySchedule.Id);
        }

        [Test]
        public void g_Link_Facility_Schedule_To_Facility()
        {
            var facilityScheduleList =
                TestingSetupClass._logic.FacilityScheduleLogic.Search(
                    schedule =>
                    schedule.FacilityScheduleDescription.Equals("Squash 40 minute slots start at 9am finish at 10pm"));

            if (facilityScheduleList == null)
                Assert.Fail("{0} facility schedule was not found", "Squash 40 minute slots start at 9am finish at 10pm");

            var facilitySchedule =
                facilityScheduleList.First(
                    schedule =>
                    schedule.FacilityScheduleDescription.Equals("Squash 40 minute slots start at 9am finish at 10pm"));

            var facilityList = TestingSetupClass._logic.FacilityLogic.Search(f => f.FacilityDescription == "Squash");

            if (facilityList == null)
                Assert.Fail("{0} facility was not found", "Squash");

            var facility = facilityList.First(f => f.FacilityDescription == "Squash");

            TestingSetupClass._logic.FacilityLogic.LinkFacilityScheduleToFacility(facility.Id, facilitySchedule.Id);
        }

        [Test]
        public void h_Create_Squash_Courts_And_Link_To_Facility()
        {
            var facilityList =
                TestingSetupClass._logic.FacilityLogic.Search(fac => fac.FacilityDescription.Equals("Squash"));

            if (facilityList == null)
                Assert.Fail("{0} facility was not found", "Squash");

            var facility = facilityList.First(f => f.FacilityDescription == "Squash");

            for (int courtNo = 1; courtNo < 4; courtNo++)
            {
                var court = new Court()
                {
                    CourtDescription = "Squash Court " + courtNo
                };

                try
                {
                    TestingSetupClass._logic.FacilityLogic.CreateCourtAndLinkToFacility(facility.Id, ref court);
                }
                catch (Exception ex)
                {
                    Assert.Fail("Create court threw an exception : {0}", ex.Message);
                }

                if (court.Id == 0)
                    Assert.Fail("Court Id did not get set, it is 0");              
            }

            Assert.Pass("All courts created successfully");
        }
    }
}
