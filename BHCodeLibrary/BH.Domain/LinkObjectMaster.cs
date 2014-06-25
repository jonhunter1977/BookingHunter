using System;
using BH.Domain;
using System.Data.Linq.Mapping;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    [Table(Name = "LinkObjectMaster")]
    public class LinkObjectMaster : ILinkObjectMaster, IDbItentity
    {
        /// <summary>
        /// Link Id - identity column
        /// </summary>
        [Column
            (
                Name = "Id",
                IsPrimaryKey = true,
                IsDbGenerated = true
            )
        ]
        public int Id { get; set; }

        /// <summary>
        /// Link type id 1 link type
        /// </summary>
        [Column(Name = "MasterLinkTypeId")]
        public LinkType MasterLinkType { get; set; }

        /// <summary>
        /// Link type 1 record ID
        /// </summary>
        [Column(Name = "MasterLinkId")]
        public int? MasterLinkId { get; set; }

        /// <summary>
        /// Link type id 2 link type
        /// </summary>
        [Column(Name = "ChildLinkTypeId")]
        public LinkType ChildLinkType { get; set; }

        /// <summary>
        /// Link type 2 record ID
        /// </summary>
        [Column(Name = "ChildLinkId")]
        public int? ChildLinkId { get; set; }
    }
}
