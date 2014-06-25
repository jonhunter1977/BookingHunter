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
    internal class FacilityRepository : IFacilityRepository
    {
        private CfgDataContext _db;

        public FacilityRepository(string cfgConnectionString)
        {
            if (cfgConnectionString.Equals(string.Empty))
                throw new Exception("Cfg Database query engine is not connected");
            else
                _db = new CfgDataContext(cfgConnectionString);
        }

        public IQueryable<Facility> GetAll()
        {
            try
            {
                var a = from b in _db.Facilities
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Facility GetById(int id)
        {
            try
            {
                var a = from b in _db.Facilities
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(Facility saveThis)
        {
           try
           {
               _db.Facilities.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(Facility updateThis)
        {
            try
            {
                _db.Facilities.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Facility deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Facilities.DeleteOnSubmit(deleteThis);
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
