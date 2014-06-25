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
    internal class LocationRepository : ILocationRepository
    {
        private CfgDataContext _db;

        public LocationRepository(string cfgConnectionString)
        {
            if (cfgConnectionString.Equals(string.Empty))
                throw new Exception("Cfg Database query engine is not connected");
            else
                _db = new CfgDataContext(cfgConnectionString);
        }

        public IQueryable<Location> GetAll()
        {
            try
            {
                var a = from b in _db.Locations
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Location GetById(int id)
        {
            try
            {
                var a = from b in _db.Locations
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(Location saveThis)
        {           
           try
           {
               _db.Locations.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(Location updateThis)
        {
            try
            {
                _db.Locations.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Location deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Locations.DeleteOnSubmit(deleteThis);
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
