using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a facility
    /// </summary>
    [Table(Name = "Facility")]
    public class Facility : IFacility, IDbItentity
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
        /// FacilityBookAheadDays name column
        /// </summary>
        [Column(Name = "FacilityBookAheadDays")]
        public int FacilityBookAheadDays { get; set; }

        /// <summary>
        /// FacilityBookAheadDays name column
        /// </summary>
        [Column(Name = "FacilityDescription")]
        public string FacilityDescription { get; set; }
    }
}
