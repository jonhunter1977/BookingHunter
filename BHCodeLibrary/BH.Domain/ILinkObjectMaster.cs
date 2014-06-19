using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public interface ILinkObjectMaster : IDbItentity
    {
        /// <summary>
        /// Link Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Link type id 1 link type
        /// </summary>
        LinkType MasterLinkType { get; set; }

        /// <summary>
        /// Link type 1 record ID
        /// </summary>
        int? MasterLinkId { get; set; }

        /// <summary>
        /// Link type id 2 link type
        /// </summary>
        LinkType ChildLinkType { get; set; }

        /// <summary>
        /// Link type 2 record ID
        /// </summary>
        int? ChildLinkId { get; set; }
    }
}
