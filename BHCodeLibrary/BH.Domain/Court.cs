using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a court
    /// </summary>
    public class Court : ICourt, IDbItentity
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
        [Column(Name = "CourtDescription")]
        public string CourtDescription { get; set; }
    }
}
