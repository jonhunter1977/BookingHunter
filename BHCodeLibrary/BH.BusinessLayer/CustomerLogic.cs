using System;
using BH.DataAccessLayer;
using BH.Domain;

namespace BH.BusinessLayer
{
    public class Customer : ICustomer, ICustomerLogic
    {
        public void CreateCustomer(ICustomer customer, IAddress address)
        {
            //Save the customer and address records to the database and then
            //create a link record in the database.
        }

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string CustomerName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
