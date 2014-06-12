using System;
using BH.DataAccessLayer;
using System.Data.SqlClient;

namespace NUnitTestingLibrary
{
    internal class BHDataAccess
    {
        private static readonly SqlConnectionStringBuilder _bookingConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_booking;User Id=sa;Password=info51987!;");

        private static readonly SqlConnectionStringBuilder _cfgConnection =
           new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_cfg;User Id=sa;Password=info51987!;");

        private static readonly SqlConnectionStringBuilder _contactConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_contact;User Id=sa;Password=info51987!;");

        private static readonly SqlConnectionStringBuilder _linksConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_links;User Id=sa;Password=info51987!;");

        private static readonly SqlConnectionStringBuilder _memberConnection =
           new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_member;User Id=sa;Password=info51987!;");

        internal static DataAccess _da;

        internal static void InitialiseDataAccess()
        {
            _da = new DataAccess
            {
                BookingConnectionString = _bookingConnection.ConnectionString,
                ContactConnectionString = _contactConnection.ConnectionString,
                CfgConnectionString = _cfgConnection.ConnectionString,
                LinksConnectionString = _linksConnection.ConnectionString,
                MemberConnectionString = _memberConnection.ConnectionString,
                AccessType = DataAccessType.SqlServer
            };
        }
    }
}
