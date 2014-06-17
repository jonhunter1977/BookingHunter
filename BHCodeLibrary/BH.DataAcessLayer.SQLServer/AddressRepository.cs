using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting address data from the database
    /// </summary>
    internal class AddressRepository : IAddressRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public AddressRepository(string contactConnectionString)
        {
            if (contactConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(contactConnectionString);

            if (_dataEngine == null) throw new Exception("Contact Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Contact Database query engine is not connected");
        }

        public IList<IAddress> GetAll()
        {
            var addressList = new List<IAddress>();

            _sqlToExecute = "SELECT * FROM [dbo].[Address]";

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Address - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                IAddress address = CreateAddressFromData();
                addressList.Add(address);
            }

            return addressList;
        }

        public IAddress GetById(int id)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[Address] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("Address - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                IAddress address = CreateAddressFromData();
                return address;
            }
            else
            {
                throw new Exception("Address Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(IAddress saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Address1", saveThis.Address1);
            _dataEngine.AddParameter("@Address2", saveThis.Address2);
            _dataEngine.AddParameter("@Address3", saveThis.Address3);
            _dataEngine.AddParameter("@Town", saveThis.Town);
            _dataEngine.AddParameter("@County", saveThis.County);
            _dataEngine.AddParameter("@Country", saveThis.Country);
            _dataEngine.AddParameter("@AddressOther", saveThis.AddressOther);
            _dataEngine.AddParameter("@PostCode", saveThis.PostCode);

            _sqlToExecute = "INSERT INTO [dbo].[Address] ";
           _sqlToExecute += "([Address1]";
           _sqlToExecute += ",[Address2]";
           _sqlToExecute += ",[Address3]";
           _sqlToExecute += ",[Town]";
           _sqlToExecute += ",[County]";
           _sqlToExecute += ",[Country]";
           _sqlToExecute += ",[AddressOther]";
           _sqlToExecute += ",[PostCode]) ";
           _sqlToExecute += "VALUES ";
           _sqlToExecute += "(";
           _sqlToExecute += _dataEngine.GetParametersForQuery();
           _sqlToExecute += ")";
            
            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Address - Save failed"); 
        }

        public void Delete(IAddress deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[Address] WHERE Id = " + _dataEngine.GetParametersForQuery();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("Address - Delete failed"); 
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private IAddress CreateAddressFromData()
        {
            var address = new Address
            {
                Address1 = _dataEngine.Dr["Address1"].ToString(),
                Address2 = _dataEngine.Dr["Address2"].ToString(),
                Address3 = _dataEngine.Dr["Address3"].ToString(),
                Town = _dataEngine.Dr["Town"].ToString(),
                County = _dataEngine.Dr["County"].ToString(),
                Country = _dataEngine.Dr["Country"].ToString(),
                AddressOther = _dataEngine.Dr["AddressOther"].ToString(),
                PostCode = _dataEngine.Dr["PostCode"].ToString(),
                Id = int.Parse(_dataEngine.Dr["Id"].ToString())
            };

            return address;
        }
    }
}
