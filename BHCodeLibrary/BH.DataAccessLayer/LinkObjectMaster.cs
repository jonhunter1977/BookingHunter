using System;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public struct LinkObjectMaster : ILinkObjectMaster, IDbItentity
    {
        /// <summary>
        /// Link Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Link type id 1 link type
        /// </summary>
        public LinkType MasterLinkType { get; set; }

        /// <summary>
        /// Link type 1 record ID
        /// </summary>
        public int? MasterLinkId { get; set; }

        /// <summary>
        /// Link type id 2 link type
        /// </summary>
        public LinkType ChildLinkType { get; set; }

        /// <summary>
        /// Link type 2 record ID
        /// </summary>
        public int? ChildLinkId { get; set; }
    }
}
