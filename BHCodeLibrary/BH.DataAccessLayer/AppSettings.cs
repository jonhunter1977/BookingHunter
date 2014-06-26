using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;

namespace BH.DataAccessLayer
{
    internal class AppSettings
    {
        /// <summary>
        /// Connection string for the bookings data source
        /// </summary>
        private static string _bookingConnectionString;
        public static string BookingConnectionString
        {
            set
            {
                _bookingConnectionString = value;
            }
            get
            {
                if (_bookingConnectionString == null || _bookingConnectionString.Equals(string.Empty)) throw new Exception("Booking Connection string is empty");
                return _bookingConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the configuration data source
        /// </summary>
        private static string _cfgConnectionString;
        public static string CfgConnectionString
        {
            set
            {
                _cfgConnectionString = value;
            }
            get
            {
                if (_cfgConnectionString == null || _cfgConnectionString.Equals(string.Empty)) throw new Exception("Config Connection string is empty");
                return _cfgConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the contact data source
        /// </summary>
        private static string _contactConnectionString;
        public static string ContactConnectionString
        {
            set
            {
                _contactConnectionString = value;
            }
            get
            {
                if (_contactConnectionString == null || _contactConnectionString.Equals(string.Empty)) throw new Exception("Contact Connection string is empty");
                return _contactConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the links data source
        /// </summary>
        private static string _linksConnectionString;
        public static string LinksConnectionString
        {
            set
            {
                _linksConnectionString = value;
            }
            get
            {
                if (_linksConnectionString == null || _linksConnectionString.Equals(string.Empty)) throw new Exception("Links Connection string is empty");
                return _linksConnectionString;
            }
        }

        /// <summary>
        /// Connection string for the links data source
        /// </summary>
        private static string _memberConnectionString;
        public static string MemberConnectionString
        {
            set
            {
                _memberConnectionString = value;
            }
            get
            {
                if (_memberConnectionString == null || _memberConnectionString.Equals(string.Empty)) throw new Exception("Member Connection string is empty");
                return _memberConnectionString;
            }
        }

        /// <summary>
        /// Namespace to use for data access
        /// </summary>
        private static string _dataAccessNameSpace;
        public static string DataAccessNameSpace
        {
            set
            {
                _dataAccessNameSpace = value;
            }
            get
            {
                if (_dataAccessNameSpace == null || _dataAccessNameSpace.Equals(string.Empty)) throw new Exception("Data access namespace is not set");
                return _dataAccessNameSpace;
            }
        }

        /// <summary>
        /// Assembly to use for data access
        /// </summary>
        private static string _dataAccessAssembly;
        public static string DataAccessAssembly
        {
            set
            {
                _dataAccessAssembly = value;

                if (_dataAccessAssembly != null)
                    _assembly = Assembly.LoadFrom(value);
                else
                    _assembly = null;
            }
            get
            {
                if (_dataAccessAssembly == null || _dataAccessAssembly.Equals(string.Empty)) throw new Exception("Data access assembly is not set");
                return _dataAccessAssembly;
            }
        }

        /// <summary>
        /// The assembly to use for data access
        /// </summary>
        private static Assembly _assembly;
        public static Assembly Assembly
        {
            get
            {
                return _assembly;
            }
        }

        static AppSettings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(@"C:\BookingHunter\NUnitTestingLibrary\bin\Debug\App.config");

            //Connection strings
            _bookingConnectionString = GetAppSetting(config, "BookingDbConnectionString");;
            _cfgConnectionString = GetAppSetting(config, "ConfigurationDbConnectionString");
            _contactConnectionString = GetAppSetting(config, "ContactDbConnectionString");
            _linksConnectionString = GetAppSetting(config, "LinksDbConnectionString");
            _memberConnectionString = GetAppSetting(config, "MemberDbConnectionString");

            //App settings
            _dataAccessNameSpace = GetAppSetting(config, "DataAccessNamespace");
            _dataAccessAssembly = GetAppSetting(config, "DataAccessAssembly"); 
        }

        static string GetAppSetting(Configuration config, string key)
        {
            KeyValueConfigurationElement element = config.AppSettings.Settings[key];
            if (element != null)
            {
                string value = element.Value;
                if (!string.IsNullOrEmpty(value))
                    return value;
            }
            return string.Empty;
        }
    }
}
