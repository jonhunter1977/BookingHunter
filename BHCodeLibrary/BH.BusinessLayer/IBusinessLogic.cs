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
        /// Access to the database tables
        /// </summary>
        IDataAccess da { get;}
    }
}
