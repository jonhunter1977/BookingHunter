using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a customer
    /// </summary>
    public class Location : ILocation, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer name column
        /// </summary>
        public string LocationDescription { get; set; }
    }
}
