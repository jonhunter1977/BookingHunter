using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a booking record
    /// </summary>
    public interface IMember : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// First name column
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Last name column
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Email address column
        /// </summary>
        string EmailAddress { get; set; }

        /// <summary>
        /// Mobile number column
        /// </summary>
        string MobileNumber { get; set; }

        /// <summary>
        /// Membership number column
        /// </summary>
        string MembershipNumber { get; set; }
    }
}
