using System;
using BH.DataAccessLayer;
using BH.Domain;
using System.Data.SqlClient;
using NUnit.Framework;

namespace NUnitTestingLibrary
{
    [SetUpFixture]
    internal class BHDataAccess
    {
        public static readonly SqlConnectionStringBuilder bookingConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_booking;User Id=sa;Password=info51987!;");

        public static readonly SqlConnectionStringBuilder cfgConnection =
           new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_cfg;User Id=sa;Password=info51987!;");

        public static readonly SqlConnectionStringBuilder contactConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_contact;User Id=sa;Password=info51987!;");

        public static readonly SqlConnectionStringBuilder linksConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_links;User Id=sa;Password=info51987!;");

        public static readonly SqlConnectionStringBuilder memberConnection =
           new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_member;User Id=sa;Password=info51987!;");

        internal static DataAccess _da;

        [SetUp]
        public void BuildDataAccess()
        {
            _da = new DataAccess
            {
                BookingConnectionString = bookingConnection.ConnectionString,
                ContactConnectionString = contactConnection.ConnectionString,
                CfgConnectionString = cfgConnection.ConnectionString,
                LinksConnectionString = linksConnection.ConnectionString,
                MemberConnectionString = memberConnection.ConnectionString,
                AccessType = DataAccessType.SqlServer
            };
        }

        [TearDown]
        public void ResetDatabaseTablesAndSeeds()
        {
            //Delete all data from the tables
            foreach (var deleteMe in _da.Address.GetAll())
            {
                _da.Address.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.BookingRecord.GetAll())
            {
                _da.BookingRecord.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.Court.GetAll())
            {
                _da.Court.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.CourtBookingSheet.GetAll())
            {
                _da.CourtBookingSheet.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.Customer.GetAll())
            {
                _da.Customer.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.Facility.GetAll())
            {
                _da.Facility.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.FacilitySchedule.GetAll())
            {
                _da.FacilitySchedule.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.Link.GetAll())
            {
                _da.Link.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.Location.GetAll())
            {
                _da.Location.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.Location.GetAll())
            {
                _da.Location.Delete(deleteMe);
            }

            foreach (var deleteMe in _da.Member.GetAll())
            {
                _da.Member.Delete(deleteMe);
            }
        }
    }
}
