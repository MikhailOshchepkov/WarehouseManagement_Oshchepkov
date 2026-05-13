using NUnit.Framework;
using WarehouseManagement_Oshchepkov.Data.Models;
using WarehouseManagement_Oshchepkov.Data.Services;
using System.Linq;

namespace WarehouseManagement_Oshchepkov.Tests.ServicesTests
{
    [TestFixture]
    public class OrganizationServiceTests
    {
        private OrganizationService _service;

        [SetUp]
        public void Setup()
        {
            _service = new OrganizationService();
        }

        [Test]
        public void GetAll_ShouldReturnOrganizations()
        {
            var result = _service.GetAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Add_ShouldIncreaseCount()
        {
            var initialCount = _service.GetAll().Count;
            var newOrg = new Organization("Тестовая организация");
            _service.Add(newOrg);
            var newCount = _service.GetAll().Count;
            Assert.That(newCount, Is.EqualTo(initialCount + 1));
        }

        [Test]
        public void GetById_ShouldReturnCorrectOrganization()
        {
            var firstOrg = _service.GetAll().First();
            var result = _service.GetById(firstOrg.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(firstOrg.Id));
            Assert.That(result.Name, Is.EqualTo(firstOrg.Name));
        }

        [Test]
        public void Update_ShouldChangeOrganizationName()
        {
            var org = _service.GetAll().First();
            var newName = "Обновленное название";
            org.Name = newName;
            _service.Update(org);
            var updatedOrg = _service.GetById(org.Id);
            Assert.That(updatedOrg.Name, Is.EqualTo(newName));
        }

        [Test]
        public void Delete_ShouldRemoveOrganization()
        {
            var org = _service.GetAll().First();
            var initialCount = _service.GetAll().Count;
            _service.Delete(org.Id);
            var newCount = _service.GetAll().Count;
            Assert.That(newCount, Is.EqualTo(initialCount - 1));
            Assert.That(_service.GetById(org.Id), Is.Null);
        }
    }
}