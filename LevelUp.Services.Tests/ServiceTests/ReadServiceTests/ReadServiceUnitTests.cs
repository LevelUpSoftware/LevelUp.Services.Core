using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LevelUp.Services.Core.CrudRepositoryInterfaces;
using LevelUp.Services.Core.Mappers;
using LevelUp.Services.Tests.ServiceTests.ReadServiceTests.Mappers;
using LevelUp.Services.Tests.ServiceTests.ReadServiceTests.Models;
using LevelUp.Services.Tests.TestAssets;
using LevelUp.Services.Tests.TestAssets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LevelUp.Services.Tests.ServiceTests.ReadServiceTests
{
    [TestClass]
    public class ReadServiceUnitTests
    {
        private Mock<IReadRepository<long, DB_Customer>> _customerRepository;
        private Mock<IReadRepository<long, DB_Employer>> _employerRepository;
        private Mock<IReadRepository<long, DB_Address>> _addressRepository;
        private List<DB_Customer> _customerDataStore;
        private List<DB_Address> _addressDataStore;
        private List<DB_Employer> _employerDataStore;
        private IMapper _mapper;

        #region Get Async Method Tests

        [TestMethod]
        public async Task GetByIdAsync_WithEntityBase_Success()
        {
            var readService = new ReadService<long, DB_Customer, DisplayCustomer>(_customerRepository.Object, _mapper);

            var newCustomer = new DB_Customer
            {
                FirstName = "John",
                LastName = "Smith",
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Deleted = false,
                Id = 1
            };
            _customerDataStore.Add(newCustomer);

            var serviceResults = await readService.GetByIdAsync(1);

            Assert.IsTrue(serviceResults != null);
            Assert.IsTrue(serviceResults?.FirstName == "John");
            Assert.IsTrue(serviceResults?.LastName == "Smith");

            _customerDataStore.Remove(newCustomer);
        }

        [TestMethod]
        public async Task GetByIdAsync_WithoutEntityBase_Success()
        {
            var readService = new ReadService<long, DB_Employer, DisplayEmployer>(_employerRepository.Object, _mapper);

            var newEmployer = new DB_Employer
            {
                Id = 10,
                Name = "Jellystone Park",
                Created = DateTimeOffset.UtcNow
            };

            _employerDataStore.Add(newEmployer);
          
            var serviceResults = await readService.GetByIdAsync(10);

            Assert.IsTrue(serviceResults != null);
            Assert.IsTrue(serviceResults?.Name == "Jellystone Park");

            _employerDataStore.Remove(newEmployer);
        }

        [TestMethod]
        public async Task GetAllAsync_Success()
        {
            var readService = new ReadService<long, DB_Customer, DisplayCustomer>(_customerRepository.Object, _mapper);

            var newCustomer = new DB_Customer
            {
                FirstName = "John",
                LastName = "Smith",
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Deleted = false,
                Id = 13
            };

            var newCustomer2 = new DB_Customer
            {
                FirstName = "Yogi",
                LastName = "Bear",
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Deleted = false,
                Id = 14
            };

            var newCustomer3 = new DB_Customer
            {
                FirstName = "Fred",
                LastName = "Flinstone",
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Deleted = false,
                Id = 15
            };

            _customerDataStore.Add(newCustomer);
            _customerDataStore.Add(newCustomer2);
            _customerDataStore.Add(newCustomer3);

            var createResults = await readService.GetAllAsync();
            Assert.IsTrue(createResults.Count() == 3);

            Assert.IsTrue(createResults.First(x => x.Id == 14).FirstName == "Yogi");
            Assert.IsTrue(createResults.First(x => x.Id == 15).LastName == "Flinstone");

            _customerDataStore.Clear();
        }

        [TestMethod]
        public async Task GetByIdAsync_WithEntityBase_Failed_NoSuchId()
        {
            var readService = new ReadService<long, DB_Customer, DisplayCustomer>(_customerRepository.Object, _mapper);

            var newCustomer = new DB_Customer
            {
                FirstName = "John",
                LastName = "Smith",
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Deleted = false,
                Id = 2
            };
            _customerDataStore.Add(newCustomer);

            var createResults = await readService.GetByIdAsync(100);

            Assert.IsTrue(createResults == null);

            _customerDataStore.Clear();
        }

        [TestMethod]
        public async Task GetByIdAsync_WithoutEntityBase_Failed_NoSuchId()
        {
            var readService = new ReadService<long, DB_Employer, DisplayEmployer>(_employerRepository.Object, _mapper);

            var newEmployer = new DB_Employer
            {
                Id = 10,
                Name = "Jellystone Park",
                Created = DateTimeOffset.UtcNow
            };

            _employerDataStore.Add(newEmployer);
            
            var serviceResults = await readService.GetByIdAsync(12);

            Assert.IsNull(serviceResults);

            _employerDataStore.Clear();
        }

        #endregion


        #region  Test Setup

        [TestInitialize]
        public void Initialize()
        {
            _customerDataStore = new List<DB_Customer>();
            _addressDataStore = new List<DB_Address>();
            _employerDataStore = new List<DB_Employer>();

            _customerRepository = new Mock<IReadRepository<long, DB_Customer>>();
            _customerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(() => _customerDataStore.AsEnumerable());
            _customerRepository.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long id) =>
            {
                return _customerDataStore.FirstOrDefault(x => x.Id == id);
            });

            _employerRepository = new Mock<IReadRepository<long, DB_Employer>>();
            _employerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(() => _employerDataStore.AsEnumerable());
            _employerRepository.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long id) =>
            {
                return _employerDataStore.FirstOrDefault(x => x.Id == id);
            });
            
            _addressRepository = new Mock<IReadRepository<long, DB_Address>>();
            _addressRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(() => _addressDataStore.AsEnumerable());
            _addressRepository.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long id) =>
            {
                return _addressDataStore.FirstOrDefault(x => x.Id == id);
            });

            MapperConfigurationGenerator.AddProfile(new CustomerMapper());
            MapperConfigurationGenerator.AddProfile(new EmployerMapper());
            _mapper = new Mapper(MapperConfigurationGenerator.Create()); 
        }

        [TestCleanup]
        public void Cleanup()
        {
           _customerDataStore.Clear();
           _addressDataStore.Clear();
           _employerDataStore.Clear();
        }


        #endregion

    }
}