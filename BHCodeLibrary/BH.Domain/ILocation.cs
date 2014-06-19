using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public interface ILocation : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Customer name column
        /// </summary>
        string LocationDescription { get; set; }
    }
}
