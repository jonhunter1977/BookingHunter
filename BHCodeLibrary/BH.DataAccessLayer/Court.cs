using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Data for a court
    /// </summary>
    public struct Court : IDbItentity
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
