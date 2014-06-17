using System;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public struct Customer : ICustomer, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer name column
        /// </summary>
        public string CustomerName { get; set; }
    }
}
