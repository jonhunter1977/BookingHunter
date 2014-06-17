using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class MemberRepository : IMemberRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public MemberRepository(string memberConnectionString)
        {
            if (memberConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(memberConnectionString);

            if (_dataEngine == null) throw new Exception("Member Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Member Database query engine is not connected");
        }

        public IList<IMember> GetAll()
        {
            var memberList = new List<IMember>();

            _sqlToExecute = "SELECT * FROM [dbo].[Member]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Member - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                IMember member = CreateMemberFromData();
                memberList.Add(member);
            }

            return memberList;
        }

        public IMember GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[Member] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Member - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                IMember member = CreateMemberFromData();
                return member;
            }
            else
            {
                throw new Exception("Member Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(IMember saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@FirstName", saveThis.FirstName);
            _dataEngine.AddParameter("@LastName", saveThis.LastName);
            _dataEngine.AddParameter("@EmailAddress", saveThis.EmailAddress);
            _dataEngine.AddParameter("@MobileNumber", saveThis.MobileNumber);
            _dataEngine.AddParameter("@MembershipNumber", saveThis.MembershipNumber);

            _sqlToExecute = "INSERT INTO [dbo].[Member] ";
            _sqlToExecute += "([FirstName]";
            _sqlToExecute += ",[LastName]";
            _sqlToExecute += ",[EmailAddress]";
            _sqlToExecute += ",[MobileNumber]";
            _sqlToExecute += ",[MembershipNumber]) ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Member - Save failed");  
        }

        public void Delete(IMember deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[Member] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Member - Delete failed"); 
        }        

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private IMember CreateMemberFromData()
        {
            var member = new Member
            {
                FirstName = _dataEngine.Dr["FirstName"].ToString(),
                LastName = _dataEngine.Dr["LastName"].ToString(),
                EmailAddress = _dataEngine.Dr["EmailAddress"].ToString(),
                MobileNumber = _dataEngine.Dr["MobileNumber"].ToString(),
                MembershipNumber = _dataEngine.Dr["MembershipNumber"].ToString(),
                Id = int.Parse(_dataEngine.Dr["Id"].ToString())
            };

            return member;
        }
    }
}
