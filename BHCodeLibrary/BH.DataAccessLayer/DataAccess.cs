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
        /// <summary>
        /// Connection string for the bookings data source
        /// </summary>
        private string _bookingConnectionString;
        public string BookingConnectionString
        {
            set
            {
                if (value == null || value.Equals(string.Empty)) throw new Exception("Booking Connection string is empty");
                _bookingConnectionString = value;
            }
            get
            {
                return _bookingConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the configuration data source
        /// </summary>
        private string _cfgConnectionString;
        public string CfgConnectionString
        {
            set
            {
                if (value == null || value.Equals(string.Empty)) throw new Exception("Configuration Connection string is empty");
                _cfgConnectionString = value;
            }
            get
            {
                return _cfgConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the contact data source
        /// </summary>
        private string _contactConnectionString;
        public string ContactConnectionString
        {
            set
            {
                if (value == null || value.Equals(string.Empty)) throw new Exception("Contact Connection string is empty");
                _contactConnectionString = value;
            }
            get
            {
                return _contactConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the links data source
        /// </summary>
        private string _linksConnectionString;
        public string LinksConnectionString
        {
            set
            {
                if (value == null || value.Equals(string.Empty)) throw new Exception("Links Connection string is empty");
                _linksConnectionString = value;
            }
            get
            {
                return _linksConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the links data source
        /// </summary>
        private string _memberConnectionString;
        public string MemberConnectionString
        {
            set
            {
                if (value == null || value.Equals(string.Empty)) throw new Exception("Member Connection string is empty");
                _memberConnectionString = value;
            }
            get
            {
                return _memberConnectionString;
            }
        }

        /// <summary>
        /// Data access type being used
        /// </summary>
        public DataAccessType AccessType;

        private string _dataAccessNameSpace
        {
            get
            {
                switch (AccessType)
                {
                    case DataAccessType.ADONet:
                        return "BH.DataAccessLayer.ADONet.";
                    case DataAccessType.LinqToSql:
                        return "BH.DataAccessLayer.LinqToSql.";
                    default:
                        return "BH.DataAccessLayer.LinqToSql.";
                }
            }
        }

        /// <summary>
        /// The assembly to reference for data access based on the data access type
        /// </summary>
        private Assembly _assembly
        {
            get
            {
                switch (AccessType)
                {
                    case DataAccessType.ADONet:
                        return Assembly.LoadFrom(_dataAccessNameSpace + "dll");
                    default :
                        return Assembly.LoadFrom(_dataAccessNameSpace + "dll");
                }
            }
        }

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
                _type = _assembly.GetType(_dataAccessNameSpace + "BookingRecordRepository");
                return (IBookingRecordRepository)Activator.CreateInstance(_type, BookingConnectionString);
            }
        }

        /// <summary>
        /// Database access for court booking sheets
        /// </summary>
        public ICourtBookingSheetRepository CourtBookingSheet
        {
            get
            {
                _type = _assembly.GetType(_dataAccessNameSpace + "CourtBookingSheetRepository");
                return (ICourtBookingSheetRepository)Activator.CreateInstance(_type, BookingConnectionString);
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
                _type = _assembly.GetType(_dataAccessNameSpace + "CourtRepository");
                return (ICourtRepository)Activator.CreateInstance(_type, CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for customers
        /// </summary>
        public ICustomerRepository Customer
        {
            get
            {
                _type = _assembly.GetType(_dataAccessNameSpace + "CustomerRepository");
                return (ICustomerRepository)Activator.CreateInstance(_type, CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for facilities
        /// </summary>
        public IFacilityRepository Facility
        {
            get
            {
                _type = _assembly.GetType(_dataAccessNameSpace + "FacilityRepository");
                return (IFacilityRepository)Activator.CreateInstance(_type, CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for facility schedules
        /// </summary>
        public IFacilityScheduleRepository FacilitySchedule
        {
            get
            {
                _type = _assembly.GetType(_dataAccessNameSpace + "FacilityScheduleRepository");
                return (IFacilityScheduleRepository)Activator.CreateInstance(_type, CfgConnectionString);
            }
        }

        /// <summary>
        /// Database access for locations
        /// </summary>
        public ILocationRepository Location
        {
            get
            {
                _type = _assembly.GetType(_dataAccessNameSpace + "LocationRepository");
                return (ILocationRepository)Activator.CreateInstance(_type, CfgConnectionString);
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
                _type = _assembly.GetType(_dataAccessNameSpace + "AddressRepository");
                return (IAddressRepository)Activator.CreateInstance(_type, ContactConnectionString);
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
                _type = _assembly.GetType(_dataAccessNameSpace + "LinkRepository");
                return (ILinkRepository)Activator.CreateInstance(_type, LinksConnectionString);
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
                _type = _assembly.GetType(_dataAccessNameSpace + "MemberRepository");
                return (IMemberRepository)Activator.CreateInstance(_type, MemberConnectionString);
            }
        }
    }
}
