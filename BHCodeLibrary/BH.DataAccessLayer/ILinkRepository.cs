using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace BH.DataAccessLayer
{
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

    /// <summary>
    /// Interface for interacting with link records in the database
    /// </summary>
    public interface ILinkRepository : IRepository<LinkObjectMaster>
    {
        /// <summary>
        /// Gets a list of child object ids of a specific link type that are linked to the master link type and id
        /// </summary>
        /// <param name="masterLinkType">The master link type</param>
        /// <param name="masterLinkId">The master link type id</param>
        /// <param name="childLinkType">The child link type</param>
        /// <returns>A list of matching LinkObjectMaster objects</returns>
        List<LinkObjectMaster> GetChildLinkObjectId(LinkType masterLinkType, int masterLinkId, LinkType childLinkType);

        /// <summary>
        /// Gets a list of master object ids of a specific link type that are linked to the child link type and id
        /// </summary>
        /// <param name="childLinkType">The child link type</param>
        /// <param name="childLinkId">The child link type id</param>
        /// <param name="masterLinkType">The master link type</param>
        /// <returns>A list of matching LinkObjectMaster objects</returns>
        List<LinkObjectMaster> GetMasterLinkObjectId(LinkType childLinkType, int childLinkId, LinkType masterLinkType);
    }
}
