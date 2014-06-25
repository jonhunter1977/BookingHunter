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
    internal class FacilityScheduleRepository : IFacilityScheduleRepository
    {
        private CfgDataContext _db;

        public FacilityScheduleRepository(string cfgConnectionString)
        {
            if (cfgConnectionString.Equals(string.Empty))
                throw new Exception("Cfg Database query engine is not connected");
            else
                _db = new CfgDataContext(cfgConnectionString);
        }

        public IQueryable<FacilitySchedule> GetAll()
        {
            try
            {
                var a = from b in _db.FacilitySchedules
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public FacilitySchedule GetById(int id)
        {
            try
            {
                var a = from b in _db.FacilitySchedules
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(FacilitySchedule saveThis)
        {
           try
           {
               _db.FacilitySchedules.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(FacilitySchedule updateThis)
        {
            try
            {
                _db.FacilitySchedules.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(FacilitySchedule deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.FacilitySchedules.DeleteOnSubmit(deleteThis);
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
