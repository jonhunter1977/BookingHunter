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
        private readonly SqlConnectionStringBuilder _cfgConnection = 
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_cfg;User Id=sa;Password=info51987!;");

        private readonly SqlConnectionStringBuilder _contactConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_contact;User Id=sa;Password=info51987!;");

        private readonly SqlConnectionStringBuilder _linksConnection =
            new SqlConnectionStringBuilder("Data Source=127.0.0.1\\SQLEXPRESS2012;Initial Catalog=sys_links;User Id=sa;Password=info51987!;");

        private DataAccess _da;

        private Customer _customer;
        private int _currentCustomerId;

        private Address _address;
        private int _currentAddressId;

        private LinkObjectMaster _linkObject;
        private int _linkObjectId;
       
        [SetUp]
        public void SetUp()
        {
            _da = new DataAccess
            {
                CfgConnectionString = _cfgConnection.ConnectionString,
                ContactConnectionString = _contactConnection.ConnectionString,
                LinksConnectionString = _linksConnection.ConnectionString,
                AccessType = DataAccessType.SqlServer
            };
        }

        [Test]
        public void ConnectionExceptionIfCfgConnectionStringIsBlank()
        {
            var da = new DataAccess
            {
                CfgConnectionString = string.Empty,
                AccessType = DataAccessType.SqlServer
            };

            var ex = Assert.Throws<Exception>(() => da.Customer.GetAll());
            Assert.That(ex.Message, Is.EqualTo("Cfg Database query engine is not connected"));         
        }

        [Test]
        public void ConnectionExceptionIfContactConnectionStringIsBlank()
        {
            var da = new DataAccess
            {
                CfgConnectionString = _cfgConnection.ConnectionString,
                ContactConnectionString = string.Empty,
                AccessType = DataAccessType.SqlServer
            };

            var ex = Assert.Throws<Exception>(() => da.Address.GetAll());
            Assert.That(ex.Message, Is.EqualTo("Contact Database query engine is not connected"));
        }

        [Test]
        public void ConnectionExceptionIfLinkConnectionStringIsBlank()
        {
            var da = new DataAccess
            {
                LinksConnectionString = string.Empty,
                AccessType = DataAccessType.SqlServer
            };

            var ex = Assert.Throws<Exception>(() => da.Link.GetAll());
            Assert.That(ex.Message, Is.EqualTo("Link Database query engine is not connected"));
        }

        [Test]
        public void CreateAndRetrieveCustomer()
        {
            var customer = new Customer
            {
                CustomerName = "Neston Cricket Club"
            };

            _da.Customer.Save(customer);

            var customerList = _da.Customer.GetAll();

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

            _da.Address.Save(address);

            var addressList = _da.Address.GetAll();

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
            var customer = _da.Customer.GetById(_currentCustomerId);
            Assert.AreEqual(customer.CustomerName, "Neston Cricket Club");
        }

        [Test]
        public void GetCurrentAddressById()
        {
            var address = _da.Address.GetById(_currentAddressId);
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

            _da.Link.Save(linkObject);

            var linkObjectList = _da.Link.GetChildLinkObjectId(
                LinkType.Customer,
                _customer.Id,
                LinkType.Address);

            //Search for the object using LINQ
            _linkObject = linkObjectList.Find(x => x.MasterLinkId == _customer.Id);

            _linkObjectId = _linkObject.Id;

            Assert.AreEqual(_address.Id, _linkObject.ChildLinkId);
        }

        [Test]
        public void LoadCustomerAndAddressInOne()
        {
            _customer = _da.Customer.GetById(_currentCustomerId);

            var linkObjectList = _da.Link.GetChildLinkObjectId(
                LinkType.Customer,
                _customer.Id,
                LinkType.Address);

            //Search for the object using LINQ
            _linkObject = linkObjectList.Find(x => x.MasterLinkId == _customer.Id);

            _address = _da.Address.GetById(_linkObject.ChildLinkId);

            Assert.AreEqual(_address.Address1, "Station Road");
        }

        [Test]
        public void RemoveCustomerFromDatabase()
        {
            _da.Customer.Delete(_customer);
            var ex = Assert.Throws<Exception>(() => _da.Customer.GetById(_currentCustomerId));
            Assert.That(ex.Message, Is.EqualTo("Customer Id " + _currentCustomerId + " does not exist in database"));
        }

        [Test]
        public void RemoveAddressFromDatabase()
        {
            _da.Address.Delete(_address);
            var ex = Assert.Throws<Exception>(() => _da.Address.GetById(_currentAddressId));
            Assert.That(ex.Message, Is.EqualTo("Address Id " + _currentAddressId + " does not exist in database"));
        }

        [Test]
        public void RemoveCustomerAddressLinkObject()
        {
            _linkObject = new LinkObjectMaster();

            _da.Link.Delete(_linkObject);

            var linkObjectList = _da.Link.GetChildLinkObjectId(
                LinkType.Customer,
                _customer.Id,
                LinkType.Address);

            //Search for the object using LINQ
            _linkObject = linkObjectList.Find(x => x.MasterLinkId == _customer.Id);

            Assert.That(_linkObject.MasterLinkId, Is.EqualTo(null));
        }
    }
}
