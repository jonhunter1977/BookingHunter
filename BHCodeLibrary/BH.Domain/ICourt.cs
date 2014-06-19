using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a court
    /// </summary>
    public interface ICourt : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Customer name column
        /// </summary>
        string CourtDescription { get; set; }
    }
}
