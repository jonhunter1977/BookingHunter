using System;
using System.Reflection;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data from the data source
    /// </summary>
    public class DataAccess : IDataAccess
    {
        public DataAccess() { }

        /// <summary>
        /// The type to use for data access
        /// </summary>
        private Type _type;

        //---------------------------------------------------
        //SYS_BOOKING DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for booking records
        /// </summary>
        public IBookingRecordRepository BookingRecord
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".BookingRecordRepository");
                return (IBookingRecordRepository)Activator.CreateInstance(_type, DataAccessSettings.BookingConnectionString);
            }
        }

        /// <summary>
        /// Database access for court booking sheets
        /// </summary>
        public ICourtBookingSheetRepository CourtBookingSheet
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".CourtBookingSheetRepository");
                return (ICourtBookingSheetRepository)Activator.CreateInstance(_type, DataAccessSettings.BookingConnectionString);
            }
        }

        //---------------------------------------------------
        //SYS_CFG DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for courts
        /// </summary>
        public ICourtRepository Court
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".CourtRepository");
                return (ICourtRepository)Activator.CreateInstance(_type, DataAccessSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for customers
        /// </summary>
        public ICustomerRepository Customer
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".CustomerRepository");
                return (ICustomerRepository)Activator.CreateInstance(_type, DataAccessSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for facilities
        /// </summary>
        public IFacilityRepository Facility
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".FacilityRepository");
                return (IFacilityRepository)Activator.CreateInstance(_type, DataAccessSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for facility schedules
        /// </summary>
        public IFacilityScheduleRepository FacilitySchedule
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".FacilityScheduleRepository");
                return (IFacilityScheduleRepository)Activator.CreateInstance(_type, DataAccessSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for locations
        /// </summary>
        public ILocationRepository Location
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".LocationRepository");
                return (ILocationRepository)Activator.CreateInstance(_type, DataAccessSettings.CfgConnectionString);
            }
        }

        //---------------------------------------------------
        //SYS_CONTACT DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for addresses
        /// </summary>
        public IAddressRepository Address
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".AddressRepository");
                return (IAddressRepository)Activator.CreateInstance(_type, DataAccessSettings.ContactConnectionString);
            }
        }

        //---------------------------------------------------
        //SYS_LINKS DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for links
        /// </summary>
        public ILinkRepository Link
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".LinkRepository");
                return (ILinkRepository)Activator.CreateInstance(_type, DataAccessSettings.LinksConnectionString);
            }
        }

        //---------------------------------------------------
        //SYS_MEMBER DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for members
        /// </summary>
        public IMemberRepository Member
        {
            get
            {
                _type = DataAccessSettings.Assembly.GetType(DataAccessSettings.DataAccessNameSpace + ".MemberRepository");
                return (IMemberRepository)Activator.CreateInstance(_type, DataAccessSettings.MemberConnectionString);
            }
        }
    }
}
