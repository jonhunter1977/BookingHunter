using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using BH.Domain;
using BH.DataAccessLayer;

namespace BH.DataAccessLayer.LinqToSql
{
    /// <summary>
    /// Class for getting address data from the database
    /// </summary>
    internal class CustomerRepository : ICustomerRepository
    {
        private CfgDataContext _db;

        public CustomerRepository(string cfgConnectionString)
        {
            if (cfgConnectionString.Equals(string.Empty))
                throw new Exception("Cfg Database query engine is not connected");
            else
                _db = new CfgDataContext(cfgConnectionString);
        }

        public IQueryable<Customer> GetAll()
        {
            try
            {
                var a = from b in _db.Customers
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Customer GetById(int id)
        {
            try
            {
                var a = from b in _db.Customers
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Insert(Customer saveThis)
        {           
           try
           {
               _db.Customers.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw e;
           }
        }

        public void Update(Customer updateThis)
        {
            try
            {
                _db.Customers.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(Customer deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Customers.DeleteOnSubmit(deleteThis);
                    _db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
