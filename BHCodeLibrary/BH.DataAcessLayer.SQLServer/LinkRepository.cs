using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting link objects from the link database
    /// </summary>
    internal class LinkRepository : ILinkRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public LinkRepository(string linkConnectionString)
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

        public int Insert(LinkObjectMaster saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@MasterLinkTypeId", ((int)saveThis.MasterLinkType).ToString());
            _dataEngine.AddParameter("@MasterLinkId", saveThis.MasterLinkId.ToString());
            _dataEngine.AddParameter("@ChildLinkTypeId", ((int)saveThis.ChildLinkType).ToString());
            _dataEngine.AddParameter("@ChildLinkId", saveThis.ChildLinkId.ToString());

            _sqlToExecute = "INSERT INTO [dbo].[LinkObjectMaster] ";
            _sqlToExecute += "([MasterLinkTypeId]";
            _sqlToExecute += ",[MasterLinkId]";
            _sqlToExecute += ",[ChildLinkTypeId]";
            _sqlToExecute += ",[ChildLinkId]) ";
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("Link - Save failed");

            return insertedRowId;
        }

        public void Delete(LinkObjectMaster deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[LinkObjectMaster] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Link - Delete failed");
        }

        public List<LinkObjectMaster> GetChildLinkObjectId(LinkType masterLinkType, int masterLinkId, LinkType childLinkType)
        {
            var linkObjectList = new List<LinkObjectMaster>();

            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@MasterLinkTypeId", ((int)masterLinkType).ToString());
            _dataEngine.AddParameter("@MasterLinkId", masterLinkId.ToString());
            _dataEngine.AddParameter("@ChildLinkTypeId", ((int)childLinkType).ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[LinkObjectMaster] ";
            _sqlToExecute += "WHERE MasterLinkTypeId = @MasterLinkTypeId ";
            _sqlToExecute += "AND MasterLinkId = @MasterLinkId ";
            _sqlToExecute += "AND ChildLinkTypeId = @ChildLinkTypeId ";

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

            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@ChildLinkTypeId", ((int)childLinkType).ToString());
            _dataEngine.AddParameter("@ChildLinkId", childLinkId.ToString());
            _dataEngine.AddParameter("@MasterLinkTypeId", ((int)masterLinkType).ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[LinkObjectMaster] ";
            _sqlToExecute += "WHERE ChildLinkTypeId = @ChildLinkTypeId ";
            _sqlToExecute += "AND ChildLinkId = @ChildLinkId ";
            _sqlToExecute += "AND MasterLinkTypeId = @MasterLinkTypeId ";

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
