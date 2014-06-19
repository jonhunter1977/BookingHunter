using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public CustomerRepository(string cfgConnectionString)
        {
            if (cfgConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(cfgConnectionString);

            if (_dataEngine == null) throw new Exception("Cfg Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Cfg Database query engine is not connected");
        }

        public IList<ICustomer> GetAll()
        {
            var customerList = new List<ICustomer>();

            _sqlToExecute = "SELECT * FROM [dbo].[Customer]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Customer - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                ICustomer customer = CreateCustomerFromData();
                customerList.Add(customer);
            }

            return customerList;
        }

        public ICustomer GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[Customer] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Customer - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                ICustomer customer = CreateCustomerFromData();
                return customer;
            }
            else
            {
                throw new Exception("Customer Id " + id.ToString() + " does not exist in database");
            }            
        }

        public int Insert(ICustomer saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@CustomerName", saveThis.CustomerName);

            _sqlToExecute = "INSERT INTO [dbo].[Customer] ";
            _sqlToExecute += "([CustomerName]) ";
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("Customer - Save failed");

            return insertedRowId;
        }

        public void Delete(ICustomer deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[Customer] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Customer - Delete failed");
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private ICustomer CreateCustomerFromData()
        {
            var customer = new Customer
            {
                Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                CustomerName = _dataEngine.Dr["CustomerName"].ToString()
            };

            return customer;
        }
    }
}
