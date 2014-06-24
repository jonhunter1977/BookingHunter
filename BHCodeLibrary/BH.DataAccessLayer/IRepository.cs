using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BH.Domain;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Interface declaring standard read/write database operations for database objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
        where T : IDbItentity
    {
        /// <summary>
        /// Run a select all on the table
        /// </summary>
        /// <returns>All records in the table</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Run a select by id on the table
        /// </summary>
        /// <param name="id">The id to search on</param>
        /// <returns>A single record</returns>
        T GetById(int id);

        /// <summary>
        /// Save a record to the table
        /// </summary>
        /// <param name="saveThis">The object to save</param>
        /// <returns>The identity ID of the inserted row</returns>
        int Insert(T saveThis);

        /// <summary>
        /// Update a record to the table
        /// </summary>
        /// <param name="saveThis">The object to update</param>
        void Update(T updateThis);

        /// <summary>
        /// Delete a record from the table
        /// </summary>
        /// <param name="deleteThis">The object to delete</param>
        void Delete(T deleteThis);
    }
}
