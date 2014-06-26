using System;
using System.Collections.Generic;
using BH.DataAccessLayer;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Interface for accessing all business logic
    /// </summary>
    public interface IGenericLogic<T>
    {
        /// <summary>
        /// Generic search function
        /// </summary>
        /// <param name="searchCriteria">The search method to execute</param>
        /// <returns>An enumerable list of type T</returns>
        IEnumerable<T> Search(Func<T, bool> searchCriteria);
    }
}
