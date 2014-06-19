using System;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Interface for interacting with the customers in the database
    /// </summary>
    public interface ICustomerRepository : IRepository<ICustomer>
    {

    }
}
