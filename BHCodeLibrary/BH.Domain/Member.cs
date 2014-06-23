using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a booking record
    /// </summary>
    public class Member : IMember, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name column
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name column
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email address column
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Mobile number column
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Membership number column
        /// </summary>
        public string MembershipNumber { get; set; }
    }
}
