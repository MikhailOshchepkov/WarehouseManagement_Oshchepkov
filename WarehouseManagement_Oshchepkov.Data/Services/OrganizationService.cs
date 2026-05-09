using System.Collections.Generic;
using System.Linq;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    /// Сервис для работы с организациями
    public class OrganizationService : IRepository<Organization>
    {
        private List<Organization> _organizations;

        public OrganizationService()
        {
            _organizations = DataGenerator.GenerateOrganizations();
        }

        public List<Organization> GetAll()
        {
            return _organizations.ToList();
        }

        public Organization? GetById(object id)
        {
            if (id is long longId)
                return _organizations.FirstOrDefault(o => o.Id == longId);
            return null;
        }

        public void Add(Organization entity)
        {
            _organizations.Add(entity);
        }

        public void Update(Organization entity)
        {
            var index = _organizations.FindIndex(o => o.Id == entity.Id);
            if (index != -1)
                _organizations[index] = entity;
        }

        public void Delete(object id)
        {
            var org = GetById(id);
            if (org != null)
                _organizations.Remove(org);
        }
    }
}