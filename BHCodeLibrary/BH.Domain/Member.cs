using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a booking record
    /// </summary>
    [Table(Name = "Member")]
    public class Member : IMember, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        [Column
            (
                Name = "Id",
                IsPrimaryKey = true,
                IsDbGenerated = true
            )
        ]
        public int Id { get; set; }

        /// <summary>
        /// First name column
        /// </summary>
        [Column(Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name column
        /// </summary>
        [Column(Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Email address column
        /// </summary>
        [Column(Name = "EmailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Mobile number column
        /// </summary>
        [Column(Name = "MobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Membership number column
        /// </summary>
        [Column(Name = "MembershipNumber")]
        public string MembershipNumber { get; set; }
    }
}
