using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting customer data from the database
    /// </summary>
    internal class CustomerRepositorySqlServer : ICustomerRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public CustomerRepositorySqlServer(string cfgConnectionString)
        {
            if (cfgConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(cfgConnectionString);

            if (_dataEngine == null) throw new Exception("Cfg Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Cfg Database query engine is not connected");
        }

        public IList<Customer> GetAll()
        {           
            var customerList = new List<Customer>();

            _sqlToExecute = "SELECT * FROM [dbo].[Customer]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Customer - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                var customer = new Customer
                {
                    Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                    CustomerName = _dataEngine.Dr["CustomerName"].ToString()                        
                };

                customerList.Add(customer);
            }

            return customerList;
        }

        public Customer GetById(int id)
        {
            _sqlToExecute = "SELECT * FROM [dbo].[Customer] WHERE Id = " + id.ToString();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Customer - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                var customer = new Customer
                {
                    CustomerName = _dataEngine.Dr["CustomerName"].ToString(),
                    Id = int.Parse(_dataEngine.Dr["Id"].ToString())
                };

                return customer;
            }
            else
            {
                throw new Exception("Customer Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(Customer saveThis)
        {          
            _sqlToExecute = "INSERT INTO [dbo].[Customer] ";
            _sqlToExecute += "([CustomerName])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "('" + saveThis.CustomerName + "')";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Customer - Save failed");
        }

        public void Delete(Customer deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[Customer] WHERE Id = " + deleteThis.Id.ToString();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Customer - Delete failed");
        }
    }
}
