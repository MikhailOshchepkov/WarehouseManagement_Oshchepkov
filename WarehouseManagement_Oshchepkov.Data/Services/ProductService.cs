using System.Collections.Generic;
using System.Linq;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    /// ������ ��� ������ � ��������
    public class ProductService
    {
        private List<Product> _products;
        private List<Category> _categories;
        private List<Manufacturer> _manufacturers;
        private List<Supplier> _suppliers;
        private WarehouseService _warehouseService;

        public ProductService(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            _categories = DataGenerator.GenerateCategories();
            _manufacturers = DataGenerator.GenerateManufacturers();
            _suppliers = DataGenerator.GenerateSuppliers();
            _products = DataGenerator.GenerateProducts(_categories, _manufacturers, _suppliers);
        }

        /// �������� ��� ������
        public List<Product> GetAllProducts()
        {
            return _products.ToList();
        }

        /// �������� ������ �� ���������� ������
        public List<Product> GetProductsByWarehouse(long warehouseId)
        {
            return _products.Where(p => p.StockQuantity > 0).ToList();
        }

        /// �������� ����� �� ��������
        public Product? GetByArticle(string article)
        {
            return _products.FirstOrDefault(p => p.Article == article);
        }

        /// �������� ����� �����
        public void Add(Product product)
        {
            _products.Add(product);
        }

        /// �������� �����
        public void Update(Product product)
        {
            var index = _products.FindIndex(p => p.Article == product.Article);
            if (index != -1)
                _products[index] = product;
        }

        /// ������� �����
        public void Delete(string article)
        {
            var product = GetByArticle(article);
            if (product != null)
                _products.Remove(product);
        }

        /// �������� ��� ���������
        public List<Category> GetAllCategories()
        {
            return _categories.ToList();
        }

        /// �������� ���� ��������������
        public List<Manufacturer> GetAllManufacturers()
        {
            return _manufacturers.ToList();
        }

        /// �������� ���� �����������
        public List<Supplier> GetAllSuppliers()
        {
            return _suppliers.ToList();
        }

        /// �������� ������� ������ �� ������
        public void UpdateStock(string article, int quantityChange)
        {
            var product = GetByArticle(article);
            if (product != null)
            {
                product.StockQuantity += quantityChange;
            }
        }
    }
}