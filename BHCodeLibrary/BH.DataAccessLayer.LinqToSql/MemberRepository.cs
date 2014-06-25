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
    internal class MemberRepository : IMemberRepository
    {
        private MemberDataContext _db;

        public MemberRepository(string cfgConnectionString)
        {
            if (cfgConnectionString.Equals(string.Empty))
                throw new Exception("Member Database query engine is not connected");
            else
                _db = new MemberDataContext(cfgConnectionString);
        }

        public IQueryable<Member> GetAll()
        {
            try
            {
                var a = from b in _db.Members
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Member GetById(int id)
        {
            try
            {
                var a = from b in _db.Members
                        where b.Id == id
                        select b;

                return a.Single();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Insert(Member saveThis)
        {            
           try
           {
               _db.Members.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(Member updateThis)
        {
            try
            {
                _db.Members.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Member deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Members.DeleteOnSubmit(deleteThis);
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
