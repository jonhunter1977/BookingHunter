using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting link objects from the link database
    /// </summary>
    internal class LinkRepositorySqlServer : ILinkRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public LinkRepositorySqlServer(string linkConnectionString)
        {
            if (linkConnectionString != null)
                _dataEngine = new DataQuerySqlServer(linkConnectionString);

            if (_dataEngine == null) throw new Exception("Link Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Link Database query engine is not connected");
        }

        public IList<LinkObjectMaster> GetAll()
        {
            var linkList = new List<LinkObjectMaster>();
            return linkList;
        }

        public LinkObjectMaster GetById(int id)
        {
            var linkObject = new LinkObjectMaster
            {
                Id = 0
            };

            return linkObject;
        }

        public void Save(LinkObjectMaster saveThis)
        {          
            _sqlToExecute = "INSERT INTO [dbo].[LinkObjectMaster] ";
            _sqlToExecute += "([MasterLinkTypeId]";
            _sqlToExecute += ",[MasterLinkId]";
            _sqlToExecute += ",[ChildLinkTypeId]";
            _sqlToExecute += ",[ChildLinkId])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(" + (int)saveThis.MasterLinkType;
            _sqlToExecute += "," + saveThis.MasterLinkId;
            _sqlToExecute += "," + (int)saveThis.ChildLinkType;
            _sqlToExecute += "," + saveThis.ChildLinkId + ")";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Link - Save failed");
        }

        public void Delete(LinkObjectMaster deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[LinkObjectMaster] WHERE Id = " + deleteThis.Id.ToString();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Link - Delete failed");
        }

        public List<LinkObjectMaster> GetChildLinkObjectId(LinkType masterLinkType, int masterLinkId, LinkType childLinkType)
        {
            var linkObjectList = new List<LinkObjectMaster>();

            _sqlToExecute = "SELECT * FROM [dbo].[LinkObjectMaster] ";
            _sqlToExecute += "WHERE MasterLinkTypeId = " + (int)masterLinkType + " ";
            _sqlToExecute += "AND MasterLinkId = " + masterLinkId + " ";
            _sqlToExecute += "AND ChildLinkTypeId = " + (int)childLinkType + " ";

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Link - GetLinkObject failed");

            while (_dataEngine.Dr.Read())
            {
                LinkObjectMaster linkObject = CreateLinkObjectFromData();
                linkObjectList.Add(linkObject);
            }

            return linkObjectList;
        }

        public List<LinkObjectMaster> GetMasterLinkObjectId(LinkType childLinkType, int childLinkId, LinkType masterLinkType)
        {
            var linkObjectList = new List<LinkObjectMaster>();

            _sqlToExecute = "SELECT * FROM [dbo].[LinkObjectMaster] ";
            _sqlToExecute += "WHERE ChildLinkTypeId = " + (int)childLinkType + " ";
            _sqlToExecute += "AND ChildLinkId = " + childLinkId + " ";
            _sqlToExecute += "AND MasterLinkTypeId = " + (int)masterLinkType + " ";

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Link - GetLinkObject failed");

            while (_dataEngine.Dr.Read())
            {
                LinkObjectMaster linkObject = CreateLinkObjectFromData();
                linkObjectList.Add(linkObject);
            }

            return linkObjectList;
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private LinkObjectMaster CreateLinkObjectFromData()
        {
            var linkObject = new LinkObjectMaster
            {
                Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                MasterLinkType = (LinkType)int.Parse(_dataEngine.Dr["MasterLinkTypeId"].ToString()),
                MasterLinkId = _dataEngine.Dr["MasterLinkTypeId"] == DBNull.Value ? (int?)null : int.Parse(_dataEngine.Dr["MasterLinkId"].ToString()),
                ChildLinkType = (LinkType)int.Parse(_dataEngine.Dr["ChildLinkTypeId"].ToString()),
                ChildLinkId = _dataEngine.Dr["ChildLinkId"] == DBNull.Value ? (int?)null : int.Parse(_dataEngine.Dr["ChildLinkId"].ToString())
            };

            return linkObject;
        }
    }
}
