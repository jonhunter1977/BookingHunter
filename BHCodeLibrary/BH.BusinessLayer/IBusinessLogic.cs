using System;
using BH.DataAccessLayer;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Interface for accessing all business logic
    /// </summary>
    public interface IBusinessLogic
    {
        /// <summary>
        /// Customer functions
        /// </summary>
        ICustomerLogic customer { get; }
    }
}
