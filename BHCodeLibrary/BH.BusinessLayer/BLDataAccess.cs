using System;
using BH.DataAccessLayer;
using BH.Domain;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Singleton class for data access
    /// </summary>
    internal sealed class BLDataAccess
    {
        private static DataAccess _instance = new DataAccess();
        private static Object _mutex = new Object();

        public static DataAccessType accessType;
        public static string cfgConnectionString;
        public static string contactConnectionString;
        public static string linksConnectionString;
        public static string bookingConnectionString;
        public static string memberConnectionString;

        static BLDataAccess()
        {
        }

        BLDataAccess()
        {
        }

        /// <summary>
        /// Return singleton instance of DataAccess class
        /// </summary>
        public static DataAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex) //for thread safety
                    {
                        if (_instance == null)
                        {
                            _instance = new DataAccess()
                            {
                                BookingConnectionString = bookingConnectionString,
                                ContactConnectionString = contactConnectionString,
                                CfgConnectionString = cfgConnectionString,
                                LinksConnectionString = linksConnectionString,
                                MemberConnectionString = memberConnectionString,
                                AccessType = accessType
                            };
                        }
                    }
                }

                return _instance;
            }        
        }
    }
}
