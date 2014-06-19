using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a court
    /// </summary>
    public struct Court : ICourt, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer name column
        /// </summary>
        public string CourtDescription { get; set; }
    }
}
