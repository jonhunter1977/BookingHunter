using System;
using BH.DataAccessLayer;
using BH.Domain;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Customer business logic
    /// </summary>
    public interface ICustomerLogic
    {
        /// <summary>
        /// Logic to create a new customer
        /// </summary>
        void CreateCustomer(ICustomer customer, IAddress address);
    }
}
