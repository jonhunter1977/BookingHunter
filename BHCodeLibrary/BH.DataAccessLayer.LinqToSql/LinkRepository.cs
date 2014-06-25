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
    internal class LinkRepository : ILinkRepository
    {
        private LinksDataContext _db;

        public LinkRepository(string cfgConnectionString)
        {
            if (cfgConnectionString.Equals(string.Empty))
                throw new Exception("Link Database query engine is not connected");
            else
                _db = new LinksDataContext(cfgConnectionString);
        }

        public IQueryable<LinkObjectMaster> GetAll()
        {
            var a = from b in _db.Links
                    select b;

            return a;
        }

        public LinkObjectMaster GetById(int id)
        {
            var a = from b in _db.Links
                    where b.Id == id
                    select b;

            return a.Single();           
        }

        public int Insert(LinkObjectMaster saveThis)
        {
           try
           {
               _db.Links.InsertOnSubmit(saveThis);
               _db.SubmitChanges();
               return saveThis.Id;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
        }

        public void Update(LinkObjectMaster updateThis)
        {
            try
            {
                _db.Links.Attach(updateThis);
                _db.Refresh(RefreshMode.KeepCurrentValues, updateThis);
                _db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(LinkObjectMaster deleteThis)
        {
            try
            {
                if (deleteThis.Id > 0)
                {
                    _db.Links.DeleteOnSubmit(deleteThis);
                    _db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<LinkObjectMaster> GetChildLinkObjectId(LinkType masterLinkType, int masterLinkId, LinkType childLinkType)
        {
            try
            {
                var a = from b in _db.Links
                        where b.MasterLinkType == masterLinkType
                        && b.MasterLinkId == masterLinkId
                        && b.ChildLinkType == childLinkType
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<LinkObjectMaster> GetMasterLinkObjectId(LinkType childLinkType, int childLinkId, LinkType masterLinkType)
        {
            try
            {
                var a = from b in _db.Links
                        where b.ChildLinkType == childLinkType
                        && b.ChildLinkId == childLinkId
                        && b.MasterLinkType == masterLinkType
                        select b;

                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
