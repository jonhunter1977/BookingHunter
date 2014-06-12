using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using BH.DataAccessLayer;
using System.Data.Common;

namespace NUnitTestingLibrary
{
    [TestFixture]
    public class CustomerDataAccessTests
    {
        private Customer _customer;
        private int _currentCustomerId;

        private Address _address;
        private int _currentAddressId;

        private LinkObjectMaster _linkObject;
        private int _linkObjectId;
       
        [SetUp]
        public void SetUp()
        {
            BHDataAccess.InitialiseDataAccess();
        }

        [Test]
        public void ConnectionExceptionIfCfgConnectionStringIsNull()
        {
            var ex = Assert.Throws<Exception>(() => 
                new DataAccess
                {
                    CfgConnectionString = null,
                    AccessType = DataAccessType.SqlServer
                }
            );
            Assert.That(ex.Message, Is.EqualTo("Configuration Connection string is empty"));         
        }

        [Test]
        public void ConnectionExceptionIfContactConnectionStringIsEmpty()
        {
            var ex = Assert.Throws<Exception>(() =>
                new DataAccess
                {
                    ContactConnectionString = string.Empty,
                    AccessType = DataAccessType.SqlServer
                }
            );
            Assert.That(ex.Message, Is.EqualTo("Contact Connection string is empty"));               
        }

        [Test]
        public void CreateAndRetrieveCustomer()
        {
            var customer = new Customer
            {
                CustomerName = "Neston Cricket Club"
            };

            BHDataAccess._da.Customer.Save(customer);

            var customerList = BHDataAccess._da.Customer.GetAll();

            if (customerList.Count == 0)
            {
                Assert.Fail("No customer records retrieved from database");
            }
            else
            {
                customer = customerList[0];
            }

            _customer = customer;
            _currentCustomerId = _customer.Id;

            Assert.AreEqual(_customer.CustomerName,"Neston Cricket Club");
        }

        [Test]
        public void CreateAndRetrieveCustomerAddress()
        {
            var address = new Address
            {
                Address1 = "Station Road",
                Town = "Neston",
                County = "Cheshire",
                PostCode = "CH64 6QJ"
            };

            BHDataAccess._da.Address.Save(address);

            var addressList = BHDataAccess._da.Address.GetAll();

            if (addressList.Count == 0)
            {
                Assert.Fail("No address records retrieved from database");
            }
            else
            {
                address = addressList[0];
            }

            _address = address;
            _currentAddressId = _address.Id;

            Assert.AreEqual(_address.Address1, "Station Road");
        }

        [Test]
        public void GetCurrentCustomerById()
        {
            var customer = BHDataAccess._da.Customer.GetById(_currentCustomerId);
            Assert.AreEqual(customer.CustomerName, "Neston Cricket Club");
        }

        [Test]
        public void GetCurrentAddressById()
        {
            var address = BHDataAccess._da.Address.GetById(_currentAddressId);
            Assert.AreEqual(address.Address1, "Station Road");
        }

        [Test]
        public void LinkCustomerAndAddress()
        {
            var linkObject = new LinkObjectMaster
            {
                MasterLinkType = LinkType.Customer,
                MasterLinkId = _customer.Id,
                ChildLinkType = LinkType.Address,
                ChildLinkId = _address.Id
            };

            BHDataAccess._da.Link.Save(linkObject);

            var linkObjectList = BHDataAccess._da.Link.GetChildLinkObjectId(
                LinkType.Customer,
                _customer.Id,
                LinkType.Address);

            _linkObject = linkObjectList.Find(x => x.MasterLinkId == _customer.Id);

            _linkObjectId = _linkObject.Id;

            Assert.AreEqual(_address.Id, _linkObject.ChildLinkId);
        }

        [Test]
        public void LoadCustomerAndAddressInOne()
        {
            _customer = BHDataAccess._da.Customer.GetById(_currentCustomerId);

            var linkObjectList = BHDataAccess._da.Link.GetChildLinkObjectId(
                LinkType.Customer,
                _customer.Id,
                LinkType.Address);

            _linkObject = linkObjectList.Find(x => x.MasterLinkId == _customer.Id);

            _address = BHDataAccess._da.Address.GetById(_linkObject.ChildLinkId.Value);

            Assert.AreEqual(_address.Address1, "Station Road");
        }

        [Test]
        public void RemoveCustomerFromDatabase()
        {
            BHDataAccess._da.Customer.Delete(_customer);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.Customer.GetById(_currentCustomerId));
            Assert.That(ex.Message, Is.EqualTo("Customer Id " + _currentCustomerId + " does not exist in database"));
        }

        [Test]
        public void RemoveAddressFromDatabase()
        {
            BHDataAccess._da.Address.Delete(_address);
            var ex = Assert.Throws<Exception>(() => BHDataAccess._da.Address.GetById(_currentAddressId));
            Assert.That(ex.Message, Is.EqualTo("Address Id " + _currentAddressId + " does not exist in database"));
        }

        [Test]
        public void RemoveCustomerAddressLinkObject()
        {
            BHDataAccess._da.Link.Delete(_linkObject);

            _linkObject = new LinkObjectMaster();

            var linkObjectList = BHDataAccess._da.Link.GetChildLinkObjectId(
                LinkType.Customer,
                _customer.Id,
                LinkType.Address);

            _linkObject = linkObjectList.Find(x => x.MasterLinkId == _customer.Id);

            Assert.That(_linkObject.MasterLinkId, Is.EqualTo(null));
        }
    }
}
