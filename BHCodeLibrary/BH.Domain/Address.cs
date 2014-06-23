using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for an address
    /// </summary>
    public class Address : IAddress, IDbItentity
    {
        /// <summary>
        /// Address Id - identity column
        /// </summary>
        public int Id { get; set; }    

        /// <summary>
        /// Address line 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Address line 3
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// Address other
        /// </summary>
        public string AddressOther { get; set; }

        /// <summary>
        /// Address country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Address county
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Address portcode
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Address town
        /// </summary>
        public string Town { get; set; }
    }
}
