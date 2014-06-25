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
    internal class CourtRepository : ICourtRepository
    {
        private CfgDataContext _db;

        public CourtRepository(string cfgConnectionString)
        {
            if (cfgConnectionString.Equals(string.Empty))
                throw new Exception("Cfg Database query engine is not connected");
            else
                _db = new CfgDataContext(cfgConnectionString);
        }

        public IQueryable<Court> GetAll()
        {
            try
            {
                var a = from b in _db.Courts
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Court GetById(int id)
        {
            try
            {
                var a = from b in _db.Courts
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(Court saveThis)
        {
           try
           {
               _db.Courts.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(Court updateThis)
        {
            try
            {
                _db.Courts.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Court deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Courts.DeleteOnSubmit(deleteThis);
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
