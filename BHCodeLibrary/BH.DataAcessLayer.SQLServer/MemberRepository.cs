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
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[Member] WHERE Id = " + _dataEngine.GetParametersForQuery();

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

        public int Insert(Member saveThis)
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
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("Member - Save failed");

            return insertedRowId; 
        }

        public void Update(Member saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@FirstName", saveThis.FirstName);
            _dataEngine.AddParameter("@LastName", saveThis.LastName);
            _dataEngine.AddParameter("@EmailAddress", saveThis.EmailAddress);
            _dataEngine.AddParameter("@MobileNumber", saveThis.MobileNumber);
            _dataEngine.AddParameter("@MembershipNumber", saveThis.MembershipNumber);

            _sqlToExecute = "UPDATE [dbo].[Member] SET ";
            _sqlToExecute += "([FirstName] = @FirstName";
            _sqlToExecute += ",[LastName] = @LastName";
            _sqlToExecute += ",[EmailAddress] = @EmailAddress";
            _sqlToExecute += ",[MobileNumber] = @MobileNumber";
            _sqlToExecute += ",[MembershipNumber] = @MembershipNumber) ";
            _sqlToExecute += "WHERE [Id] = " + saveThis.Id;

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Member - Update failed");
        }

        public void Delete(Member deleteThis)
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
