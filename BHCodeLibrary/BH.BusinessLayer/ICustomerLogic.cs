﻿using System;
using BH.Domain;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Customer business logic
    /// </summary>
    public interface ICustomerLogic : IBusinessLogic<Customer>
    {
        /// <summary>
        /// Logic to create a new customer
        /// </summary>
        void CreateCustomer(ref Customer customer, ref Address address);

        /// <summary>
        /// Logic to update a customer
        /// </summary>
        /// <param name="customer">The customer object to update</param>
        void UpdateCustomer(ref Customer customer);

        /// <summary>
        /// Search for a customer by ID
        /// </summary>
        /// <param name="id">The ID to search for</param>
        /// <returns>A customer object</returns>
        Customer FindCustomerById(int id);

        /// <summary>
        /// Find the address linked to a customer
        /// </summary>
        /// <param name="customer">The customer whose address you want to find</param>
        /// <returns>An address object</returns>
        Address GetCustomerAddress(Customer customer);

    }
}
