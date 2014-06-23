using System;
using BH.Domain;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Customer business logic
    /// </summary>
    public interface ILocationLogic : IBusinessLogic<Location>
    {
        /// <summary>
        /// Logic to create a new customer
        /// </summary>
        void CreateLocation(int customerId, ref Location location, ref Address address);

        /// <summary>
        /// Logic to update a customer
        /// </summary>
        /// <param name="location">The location object to update</param>
        void UpdateLocation(ref Location location);

        /// <summary>
        /// Search for a customer by ID
        /// </summary>
        /// <param name="id">The ID to search for</param>
        /// <returns>A customer object</returns>
        Customer FindCustomerById(int id);
    }
}
