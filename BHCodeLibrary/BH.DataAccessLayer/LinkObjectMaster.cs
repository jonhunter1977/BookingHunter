using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public struct LinkObjectMaster : IDbItentity
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
        public int MasterLinkId { get; set; }

        /// <summary>
        /// Link type id 2 link type
        /// </summary>
        public LinkType ChildLinkType { get; set; }

        /// <summary>
        /// Link type 2 record ID
        /// </summary>
        public int ChildLinkId { get; set; }
    }
}
