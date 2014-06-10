namespace BH.DataAccessLayer
{
    public enum DataAccessType
    {
        SqlServer
    }
    /// <summary>
    /// Interface for accessing data
    /// </summary>
    public interface IDataAccess
    {
        //---------------------------------------------------
        //SYS_CFG DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for bookings
        /// </summary>
        IBookingRecordRepository BookingRecord { get; }

        /// <summary>
        /// Database access for court booking sheets
        /// </summary>
        ICourtBookingSheetRepository CourtBookingSheet { get; }

        //---------------------------------------------------
        //SYS_CFG DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for courts
        /// </summary>
        ICourtRepository Court { get; }

        /// <summary>
        /// Database access for customers
        /// </summary>
        ICustomerRepository Customer { get; }

        /// <summary>
        /// Database access for facilities
        /// </summary>
        IFacilityRepository Facility { get; }

        /// <summary>
        /// Database access for facility schedules
        /// </summary>
        IFacilityScheduleRepository FacilitySchedule { get; }

        /// <summary>
        /// Database access for locations
        /// </summary>
        ILocationRepository Location { get; }

        //---------------------------------------------------
        //SYS_CONTACT DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for addresses
        /// </summary>
        IAddressRepository Address { get; }

        //---------------------------------------------------
        //SYS_LINKS DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for links
        /// </summary>
        ILinkRepository Link { get; }

        //---------------------------------------------------
        //SYS_MEMBER DATABASE TABLES
        //---------------------------------------------------

        /// <summary>
        /// Database access for members
        /// </summary>
        IMemberRepository Member { get; }
    }
}
