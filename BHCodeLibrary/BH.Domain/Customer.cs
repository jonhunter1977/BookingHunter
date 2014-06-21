﻿using System;

namespace BH.Domain
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

        /// <summary>
        /// Active name column
        /// </summary>
        public bool Active { get; set; }
    }
}
