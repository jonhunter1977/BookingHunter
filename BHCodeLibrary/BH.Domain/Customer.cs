using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public class Customer : ICustomer, IDbItentity
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
        [Column(Name = "CustomerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Active name column
        /// </summary>
        [Column(Name = "Active")]
        public bool Active { get; set; }
    }
}
