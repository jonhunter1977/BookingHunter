using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class MemberRepositorySqlServer : IMemberRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public MemberRepositorySqlServer(string memberConnectionString)
        {
            if (memberConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(memberConnectionString);

            if (_dataEngine == null) throw new Exception("Member Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Member Database query engine is not connected");
        }

        public IList<Member> GetAll()
        {
            var memberList = new List<Member>();

            _sqlToExecute = "SELECT * FROM [dbo].[Member]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Member - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                Member member = CreateMemberFromData();
                memberList.Add(member);
            }

            return memberList;
        }

        public Member GetById(int id)
        {
            _sqlToExecute = "SELECT * FROM [dbo].[Member] WHERE Id = " + id.ToString();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Member - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                Member member = CreateMemberFromData();
                return member;
            }
            else
            {
                throw new Exception("Member Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(Member saveThis)
        {
            _sqlToExecute = "INSERT INTO [dbo].[Member] ";
            _sqlToExecute += "([FirstName]";
            _sqlToExecute += ",[LastName]";
            _sqlToExecute += ",[EmailAddress]";
            _sqlToExecute += ",[MobileNumber]";
            _sqlToExecute += ",[MembershipNumber])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "('" + saveThis.FirstName + "'";
            _sqlToExecute += ",'" + saveThis.LastName + "'";
            _sqlToExecute += ",'" + saveThis.EmailAddress + "'";
            _sqlToExecute += ",'" + saveThis.MobileNumber + "'";
            _sqlToExecute += ",'" + saveThis.MembershipNumber + "')";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Member - Save failed");
        }

        public void Delete(Member deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[Member] WHERE Id = " + deleteThis.Id.ToString();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Member - Delete failed");
        }        

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private Member CreateMemberFromData()
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
