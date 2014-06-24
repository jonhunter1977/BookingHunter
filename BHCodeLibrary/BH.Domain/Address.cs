using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for an address
    /// </summary>
    [Table(Name = "Address")]
    public class Address : IAddress, IDbItentity
    {
        /// <summary>
        /// Address Id - identity column
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
        /// Address line 1
        /// </summary>
        [Column(Name = "Address1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        [Column(Name = "Address2")]
        public string Address2 { get; set; }

        /// <summary>
        /// Address line 3
        /// </summary>
        [Column(Name = "Address3")]
        public string Address3 { get; set; }

        /// <summary>
        /// Address other
        /// </summary>
        [Column(Name = "AddressOther")]
        public string AddressOther { get; set; }

        /// <summary>
        /// Address country
        /// </summary>
        [Column(Name = "Country")]
        public string Country { get; set; }

        /// <summary>
        /// Address county
        /// </summary>
        [Column(Name = "County")]
        public string County { get; set; }

        /// <summary>
        /// Address portcode
        /// </summary>
        [Column(Name = "PostCode")]
        public string PostCode { get; set; }

        /// <summary>
        /// Address town
        /// </summary>
        [Column(Name = "Town")]
        public string Town { get; set; }
    }
}
