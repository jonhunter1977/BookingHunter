using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    [Table(Name = "Customer")]
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
        private int _active;
        public bool Active 
        { 
            get { return _active == 1 ? true : false; }
            set { _active = value == true ? 1 : 0; }
        }
    }
}
