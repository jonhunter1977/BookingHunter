using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public interface ICustomer : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Customer name column
        /// </summary>
        string CustomerName { get; set; }
    }
}
