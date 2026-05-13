using System.Collections.Generic;
using System.Linq;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    /// Сервис для работы со складами
    public class WarehouseService : IRepository<Warehouse>
    {
        private List<Warehouse> _warehouses;
        private OrganizationService _orgService;

        public WarehouseService(OrganizationService orgService)
        {
            _orgService = orgService;
            var organizations = _orgService.GetAll();
            _warehouses = DataGenerator.GenerateWarehouses(organizations);
        }

        public List<Warehouse> GetAll()
        {
            return _warehouses.ToList();
        }

        /// Получить склады по ID организации
        public List<Warehouse> GetByOrganizationId(long organizationId)
        {
            return _warehouses.Where(w => w.OrganizationId == organizationId).ToList();
        }

        public Warehouse? GetById(object id)
        {
            if (id is long longId)
                return _warehouses.FirstOrDefault(w => w.Id == longId);
            return null;
        }

        public void Add(Warehouse entity)
        {
            _warehouses.Add(entity);
        }

        public void Update(Warehouse entity)
        {
            var index = _warehouses.FindIndex(w => w.Id == entity.Id);
            if (index != -1)
                _warehouses[index] = entity;
        }

        public void Delete(object id)
        {
            var warehouse = GetById(id);
            if (warehouse != null)
                _warehouses.Remove(warehouse);
        }
    }
}