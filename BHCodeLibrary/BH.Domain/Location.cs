using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    [Table(Name = "Location")]
    public class Location : ILocation, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
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
        /// Customer name column
        /// </summary>
        [Column(Name = "LocationDescription")]
        public string LocationDescription { get; set; }
    }
}
