using System;
using BH.Domain;

namespace BH.BusinessLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        public BusinessLogic
        (
            DataAccessType accessType,
            string cfgConnectionString,
            string contactConnectionString,
            string linksConnectionString,
            string bookingConnectionString,
            string memberConnectionString
        )
        {
            BLDataAccess.accessType = accessType;
            BLDataAccess.cfgConnectionString = cfgConnectionString;
            BLDataAccess.contactConnectionString = contactConnectionString;
            BLDataAccess.linksConnectionString = linksConnectionString;
            BLDataAccess.bookingConnectionString = bookingConnectionString;
            BLDataAccess.memberConnectionString = memberConnectionString;           
        }

        public ICustomerLogic customerLogic
        {
            get { return CustomerLogic.Instance; }
        }

        public ILocationLogic locationLogic
        {
            get { return LocationLogic.Instance; }
        }
    }
}
