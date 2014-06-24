using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BH.Domain
{
    /// <summary>
    /// Supported data connection types
    /// </summary>
    public enum DataAccessType
    {
        ADONet,
        LinqToSql
    }

    /// <summary>
    /// The different ways a customer can update a booking to say they have arrived
    /// </summary>
    public enum ArrivalRegistrationMethod
    {
        NotArrived = 0,
        BookingReference = 1,
        BookingPIN = 2,
        QRCode = 3
    }

    /// <summary>
    /// The different statuses of a booking
    /// </summary>
    public enum BookingStatus
    {
        Draft = 1,
        Live = 2,
        Cancelled = 3
    }

    /// <summary>
    /// Decribes the link type objects to their Id in the link table
    /// </summary>
    public enum LinkType
    {
        Customer = 1,
        Location = 2,
        Address = 3,
        Facility = 4,
        FacilitySchedule = 5,
        Court = 6,
        CourtBookingSheet = 7,
        BookingRecord = 8,
        Member = 9
    }
}
