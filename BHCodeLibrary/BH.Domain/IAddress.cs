using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for an address
    /// </summary>
    public interface IAddress : IDbItentity
    {
        /// <summary>
        /// Address Id - identity column
        /// </summary>
        int Id { get; set; }    

        /// <summary>
        /// Address line 1
        /// </summary>
        string Address1 { get; set; }

        /// <summary>
        /// Address line 2
        /// </summary>
        string Address2 { get; set; }

        /// <summary>
        /// Address line 3
        /// </summary>
        string Address3 { get; set; }

        /// <summary>
        /// Address other
        /// </summary>
        string AddressOther { get; set; }

        /// <summary>
        /// Address country
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// Address county
        /// </summary>
        string County { get; set; }

        /// <summary>
        /// Address portcode
        /// </summary>
        string PostCode { get; set; }

        /// <summary>
        /// Address town
        /// </summary>
        string Town { get; set; }
    }
}
