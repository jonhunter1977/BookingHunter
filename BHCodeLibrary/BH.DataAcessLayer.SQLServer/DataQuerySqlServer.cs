using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for querying an SQL database
    /// </summary>
    internal class DataQuerySqlServer
    {
        private readonly SqlConnection _conn;
        private SqlCommand _command;

        public Boolean DatabaseConnected;
        public SqlDataReader Dr;

        /// <summary>
        /// Opens a connection to a database using the supplied connection string
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <returns>Boolean - true if the connection was opened or is open</returns>
        public DataQuerySqlServer(String connectionString)
        {
            if (connectionString.Equals(String.Empty))
            {
                DatabaseConnected = false;
                return;
            }

            _conn = new SqlConnection(connectionString);

            string connectionStatus;
            DatabaseConnected = OpenConnection(_conn);
        }

        /// <summary>
        /// Opens the connection to a database and sets the connection object
        /// </summary>
        /// <param name="conn">The SqlConnection object to use</param>
        /// <returns>Boolean - true if the connection was opened or is open</returns>
        private Boolean OpenConnection(SqlConnection conn)
        {
            switch (_conn.State)
            {
                case ConnectionState.Closed:
                    try
                    {
                        _conn.Open();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        return false;
                    }
                case ConnectionState.Open:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Destructor method
        /// </summary>
        ~DataQuerySqlServer()
        {
            CloseConnection();
        }

        /// <summary>
        /// Closes the database connection
        /// </summary>
        /// <returns>void</returns>
        public void CloseConnection()
        {
            if (_conn == null) return;
            if (_conn.State != ConnectionState.Open) return;
            try
            {
                _conn.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Closes the data reader
        /// </summary>
        /// <returns>void</returns>
        public void CloseDataReader()
        {
            Dr.Close();
            Dr.Dispose();

            _command.Dispose();
        }

        /// <summary>
        /// Read data using a stored procedure.  Populates the internal data reader
        /// with data from the table
        /// </summary>
        /// <param name="sqlToExecute">The TSQL query to execute</param>
        /// <returns>True if the command executed OK, false if not</returns>
        public bool CreateReaderFromSql(string sqlToExecute)
        {
            try
            {
                _command = new SqlCommand
                {
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandText = sqlToExecute,
                    CommandTimeout = 150
                };

                Dr = _command.ExecuteReader();

                return true;
            }
            catch (SqlException sqlErr)
            {
                return false;
            }
        }

        /// <summary>
        /// Runs a TSQL query against the data source
        /// </summary>
        /// <param name="sqlToExecute">The TSQL query to execute</param>
        /// <returns>True if the TSQL ran OK, false if not</returns>
        public bool ExecuteSql(string sqlToExecute)
        {
            try
            {
                _command = new SqlCommand
                {
                    Connection = _conn,
                    CommandType = CommandType.Text,
                    CommandText = sqlToExecute,
                    CommandTimeout = 150
                };

                _command.ExecuteNonQuery();

                return true;
            }
            catch (SqlException sqlErr)
            {
                throw sqlErr;
            }
        }
    }
}
