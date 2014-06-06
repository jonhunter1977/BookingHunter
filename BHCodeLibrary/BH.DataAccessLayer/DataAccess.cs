using System;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data from the data source
    /// </summary>
    public struct DataAccess : IDataAccess
    {
        /// <summary>
        /// Connection string for the configuration data source
        /// </summary>
        public string CfgConnectionString { get; set; }

        /// <summary>
        /// Connection string for the contact data source
        /// </summary>
        public string ContactConnectionString { get; set; }

        /// <summary>
        /// Connection string for the links data source
        /// </summary>
        public string LinksConnectionString { get; set; }

        /// <summary>
        /// Data access type being used
        /// </summary>
        public DataAccessType AccessType;

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
                switch (AccessType)
                {
                    case DataAccessType.SqlServer:
                        return new CourtRepositorySqlServer(CfgConnectionString);
                    default: throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Database access for customers
        /// </summary>
        public ICustomerRepository Customer
        {
            get
            {
                switch (AccessType)
                {
                    case DataAccessType.SqlServer:
                        return new CustomerRepositorySqlServer(CfgConnectionString);
                    default: throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Database access for facilities
        /// </summary>
        public IFacilityRepository Facility
        {
            get
            {
                switch (AccessType)
                {
                    case DataAccessType.SqlServer:
                        return new FacilityRepositorySqlServer(CfgConnectionString);
                    default: throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Database access for facility schedules
        /// </summary>
        public IFacilityScheduleRepository FacilitySchedule
        {
            get
            {
                switch (AccessType)
                {
                    case DataAccessType.SqlServer:
                        return new FacilityScheduleRepositorySqlServer(CfgConnectionString);
                    default: throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Database access for locations
        /// </summary>
        public ILocationRepository Location
        {
            get
            {
                switch (AccessType)
                {
                    case DataAccessType.SqlServer:
                        return new LocationRepositorySqlServer(CfgConnectionString);
                    default: throw new NotImplementedException();
                }
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
                switch (AccessType)
                {
                    case DataAccessType.SqlServer:
                        return new AddressRepositorySqlServer(ContactConnectionString);
                    default: throw new NotImplementedException();
                }
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
                switch (AccessType)
                {
                    case DataAccessType.SqlServer:
                        return new LinkRepositorySqlServer(LinksConnectionString);
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
