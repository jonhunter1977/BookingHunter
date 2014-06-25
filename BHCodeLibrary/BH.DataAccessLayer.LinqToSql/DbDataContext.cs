using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using BH.Domain;

namespace BH.DataAccessLayer.LinqToSql
{
    internal class CfgDataContext : DataContext
    {
        public Table<Court> Courts;
        public Table<Customer> Customers;
        public Table<Facility> Facilities;
        public Table<FacilitySchedule> FacilitySchedules;
        public Table<Location> Locations;
        public CfgDataContext(string connection) : base(connection) { }
    }

    internal class ContactDataContext : DataContext
    {
        public Table<Address> Addresses;
        public ContactDataContext(string connection) : base(connection) { }
    }

    internal class BookingDataContext : DataContext
    {
        public Table<BookingRecord> Bookings;
        public Table<CourtBookingSheet> CourtBookingSheets;
        public BookingDataContext(string connection) : base(connection) { }
    }

    internal class LinksDataContext : DataContext
    {
        public Table<LinkObjectMaster> Links;
        public LinksDataContext(string connection) : base(connection) { }
    }

    internal class MemberDataContext : DataContext
    {
        public Table<Member> Members;
        public MemberDataContext(string connection) : base(connection) { }
    }
}
