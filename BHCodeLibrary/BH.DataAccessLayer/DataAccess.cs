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
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".BookingRecordRepository");
                return (IBookingRecordRepository)Activator.CreateInstance(_type, AppSettings.BookingConnectionString);
            }
        }

        /// <summary>
        /// Database access for court booking sheets
        /// </summary>
        public ICourtBookingSheetRepository CourtBookingSheet
        {
            get
            {
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".CourtBookingSheetRepository");
                return (ICourtBookingSheetRepository)Activator.CreateInstance(_type, AppSettings.BookingConnectionString);
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
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".CourtRepository");
                return (ICourtRepository)Activator.CreateInstance(_type, AppSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for customers
        /// </summary>
        public ICustomerRepository Customer
        {
            get
            {
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".CustomerRepository");
                return (ICustomerRepository)Activator.CreateInstance(_type, AppSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for facilities
        /// </summary>
        public IFacilityRepository Facility
        {
            get
            {
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".FacilityRepository");
                return (IFacilityRepository)Activator.CreateInstance(_type, AppSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for facility schedules
        /// </summary>
        public IFacilityScheduleRepository FacilitySchedule
        {
            get
            {
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".FacilityScheduleRepository");
                return (IFacilityScheduleRepository)Activator.CreateInstance(_type, AppSettings.CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for locations
        /// </summary>
        public ILocationRepository Location
        {
            get
            {
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".LocationRepository");
                return (ILocationRepository)Activator.CreateInstance(_type, AppSettings.CfgConnectionString);
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
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".AddressRepository");
                return (IAddressRepository)Activator.CreateInstance(_type, AppSettings.ContactConnectionString);
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
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".LinkRepository");
                return (ILinkRepository)Activator.CreateInstance(_type, AppSettings.LinksConnectionString);
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
                _type = AppSettings.Assembly.GetType(AppSettings.DataAccessNameSpace + ".MemberRepository");
                return (IMemberRepository)Activator.CreateInstance(_type, AppSettings.MemberConnectionString);
            }
        }
    }
}
