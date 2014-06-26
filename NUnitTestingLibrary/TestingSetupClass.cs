using System;
using BH.DataAccessLayer;
using BH.BusinessLayer;
using BH.Domain;
using System.Data.SqlClient;
using NUnit.Framework;

namespace NUnitTestingLibrary
{
    [SetUpFixture]
    internal class TestingSetupClass
    {
        internal static DataAccess _da;
        internal static BusinessLogic _logic;

        [SetUp]
        public void Setup()
        {
            _da = new DataAccess();
            _logic = new BusinessLogic();
        }

        //[TearDown]
        //public void ResetDatabaseTablesAndSeeds()
        //{
        //    //Delete all data from the tables
        //    foreach (var deleteMe in _da.Address.GetAll())
        //    {
        //        _da.Address.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.BookingRecord.GetAll())
        //    {
        //        _da.BookingRecord.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.Court.GetAll())
        //    {
        //        _da.Court.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.CourtBookingSheet.GetAll())
        //    {
        //        _da.CourtBookingSheet.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.Customer.GetAll())
        //    {
        //        _da.Customer.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.Facility.GetAll())
        //    {
        //        _da.Facility.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.FacilitySchedule.GetAll())
        //    {
        //        _da.FacilitySchedule.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.Link.GetAll())
        //    {
        //        _da.Link.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.Location.GetAll())
        //    {
        //        _da.Location.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.Location.GetAll())
        //    {
        //        _da.Location.Delete(deleteMe);
        //    }

        //    foreach (var deleteMe in _da.Member.GetAll())
        //    {
        //        _da.Member.Delete(deleteMe);
        //    }
        //}
    }
}
