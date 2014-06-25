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
    internal class AddressRepository : IAddressRepository
    {
        private ContactDataContext _db;

        public AddressRepository(string contactConnectionString)
        {
            if (contactConnectionString.Equals(string.Empty))
                throw new Exception("Contact Database query engine is not connected");
            else
                _db = new ContactDataContext(contactConnectionString);
        }

        public IQueryable<Address> GetAll()
        {
            try
            {
                var a = from b in _db.Addresses
                        select b;

                return a;            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Address GetById(int id)
        {
            try
            {
                var a = from b in _db.Addresses
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(Address saveThis)
        {
           try
           {
               _db.Addresses.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(Address updateThis)
        {
            try
            {
                _db.Addresses.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Address deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Addresses.DeleteOnSubmit(deleteThis);
                    _db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
