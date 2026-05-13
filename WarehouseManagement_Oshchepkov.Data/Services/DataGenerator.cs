using System.Collections.Generic;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    public static class DataGenerator
    {
        public static List<Organization> GenerateOrganizations()
        {
            var organizations = new List<Organization>();

            var org1 = new Organization("��� ���� � ������");
            org1.Id = 1;

            var org2 = new Organization("�� ���������");
            org2.Id = 2;

            var org3 = new Organization("�� ������ �.�.");
            org3.Id = 3;

            var org4 = new Organization("��� �����������");
            org4.Id = 4;

            var org5 = new Organization("��� ����������");
            org5.Id = 5;

            organizations.Add(org1);
            organizations.Add(org2);
            organizations.Add(org3);
            organizations.Add(org4);
            organizations.Add(org5);

            return organizations;
        }

        public static List<Category> GenerateCategories()
        {
            var categories = new List<Category>();

            var cat1 = new Category("�����������");
            cat1.Id = 1;

            var cat2 = new Category("���������");
            cat2.Id = 2;

            var cat3 = new Category("������� �������");
            cat3.Id = 3;

            var cat4 = new Category("�������������");
            cat4.Id = 4;

            var cat5 = new Category("������� ��������������");
            cat5.Id = 5;

            categories.Add(cat1);
            categories.Add(cat2);
            categories.Add(cat3);
            categories.Add(cat4);
            categories.Add(cat5);

            return categories;
        }

        public static List<Manufacturer> GenerateManufacturers()
        {
            var manufacturers = new List<Manufacturer>();

            var man1 = new Manufacturer("Lenovo");
            man1.Id = 1;

            var man2 = new Manufacturer("Logitech");
            man2.Id = 2;

            var man3 = new Manufacturer("Samsung");
            man3.Id = 3;

            var man4 = new Manufacturer("HP");
            man4.Id = 4;

            var man5 = new Manufacturer("Dell");
            man5.Id = 5;

            var man6 = new Manufacturer("Apple");
            man6.Id = 6;

            var man7 = new Manufacturer("Asus");
            man7.Id = 7;

            var man8 = new Manufacturer("Acer");
            man8.Id = 8;

            manufacturers.Add(man1);
            manufacturers.Add(man2);
            manufacturers.Add(man3);
            manufacturers.Add(man4);
            manufacturers.Add(man5);
            manufacturers.Add(man6);
            manufacturers.Add(man7);
            manufacturers.Add(man8);

            return manufacturers;
        }

        public static List<Supplier> GenerateSuppliers()
        {
            var suppliers = new List<Supplier>();

            var sup1 = new Supplier("��� ���������� � �������");
            sup1.Id = 1;

            var sup2 = new Supplier("��������");
            sup2.Id = 2;

            var sup3 = new Supplier("���������");
            sup3.Id = 3;

            var sup4 = new Supplier("��������");
            sup4.Id = 4;

            var sup5 = new Supplier("�.�����");
            sup5.Id = 5;

            var sup6 = new Supplier("DNS");
            sup6.Id = 6;

            suppliers.Add(sup1);
            suppliers.Add(sup2);
            suppliers.Add(sup3);
            suppliers.Add(sup4);
            suppliers.Add(sup5);
            suppliers.Add(sup6);

            return suppliers;
        }

        public static List<Warehouse> GenerateWarehouses(List<Organization> organizations)
        {
            var warehouses = new List<Warehouse>();
            int whId = 1;

            foreach (var org in organizations)
            {
                if (org.Name == "��� ���� � ������")
                {
                    var wh1 = new Warehouse("����� �1", "�. ������, ��. ������, 1", org.Id);
                    wh1.Id = whId++;
                    warehouses.Add(wh1);

                    var wh2 = new Warehouse("����� �2", "�. ������, ��. ���������, 15", org.Id);
                    wh2.Id = whId++;
                    warehouses.Add(wh2);
                }
                else if (org.Name == "�� ���������")
                {
                    var wh3 = new Warehouse("�������� �����", "�. �����-���������, ��. ����, 42", org.Id);
                    wh3.Id = whId++;
                    warehouses.Add(wh3);
                }
                else if (org.Name == "�� ������ �.�.")
                {
                    var wh4 = new Warehouse("��������� �����", "�. ������������, ��. ���������, 7", org.Id);
                    wh4.Id = whId++;
                    warehouses.Add(wh4);
                }
                else if (org.Name == "��� �����������")
                {
                    var wh5 = new Warehouse("����������� �����", "�. �����������, ��. ��������, 10", org.Id);
                    wh5.Id = whId++;
                    warehouses.Add(wh5);
                }
                else if (org.Name == "��� ����������")
                {
                    var wh6 = new Warehouse("����� ������� ���������", "�. ������, ��. ������������, 5", org.Id);
                    wh6.Id = whId++;
                    warehouses.Add(wh6);
                }
            }

            return warehouses;
        }

        public static List<Product> GenerateProducts(List<Category> categories,
                                                       List<Manufacturer> manufacturers,
                                                       List<Supplier> suppliers)
        {
            var products = new List<Product>();
            int article = 1;

            int GetCategoryId(string name) => categories.Find(c => c.Name == name)?.Id ?? 1;
            int GetManufacturerId(string name) => manufacturers.Find(m => m.Name == name)?.Id ?? 1;
            int GetSupplierId(string name) => suppliers.Find(s => s.Name == name)?.Id ?? 1;

            var product1 = new Product("������� Lenovo IdeaPad 3", 45990);
            product1.Article = (article++).ToString("D6");
            product1.Unit = "��";
            product1.DiscountPercent = 5;
            product1.StockQuantity = 25;
            product1.CategoryId = GetCategoryId("�����������");
            product1.ManufacturerId = GetManufacturerId("Lenovo");
            product1.SupplierId = GetSupplierId("��� ���������� � �������");
            product1.Category = categories.Find(c => c.Id == product1.CategoryId);
            product1.Manufacturer = manufacturers.Find(m => m.Id == product1.ManufacturerId);
            product1.Supplier = suppliers.Find(s => s.Id == product1.SupplierId);
            product1.SupplierName = product1.Supplier?.Name ?? "";
            products.Add(product1);

            var product2 = new Product("���� Logitech MX Master 3", 14990);
            product2.Article = (article++).ToString("D6");
            product2.Unit = "��";
            product2.DiscountPercent = 0;
            product2.StockQuantity = 50;
            product2.CategoryId = GetCategoryId("���������");
            product2.ManufacturerId = GetManufacturerId("Logitech");
            product2.SupplierId = GetSupplierId("��������");
            product2.Category = categories.Find(c => c.Id == product2.CategoryId);
            product2.Manufacturer = manufacturers.Find(m => m.Id == product2.ManufacturerId);
            product2.Supplier = suppliers.Find(s => s.Id == product2.SupplierId);
            product2.SupplierName = product2.Supplier?.Name ?? "";
            products.Add(product2);

            var product3 = new Product("���������� Logitech G915", 22990);
            product3.Article = (article++).ToString("D6");
            product3.Unit = "��";
            product3.DiscountPercent = 10;
            product3.StockQuantity = 30;
            product3.CategoryId = GetCategoryId("���������");
            product3.ManufacturerId = GetManufacturerId("Logitech");
            product3.SupplierId = GetSupplierId("��������");
            product3.Category = categories.Find(c => c.Id == product3.CategoryId);
            product3.Manufacturer = manufacturers.Find(m => m.Id == product3.ManufacturerId);
            product3.Supplier = suppliers.Find(s => s.Id == product3.SupplierId);
            product3.SupplierName = product3.Supplier?.Name ?? "";
            products.Add(product3);

            var product4 = new Product("������� Samsung Odyssey G7", 49990);
            product4.Article = (article++).ToString("D6");
            product4.Unit = "��";
            product4.DiscountPercent = 15;
            product4.StockQuantity = 15;
            product4.CategoryId = GetCategoryId("�����������");
            product4.ManufacturerId = GetManufacturerId("Samsung");
            product4.SupplierId = GetSupplierId("���������");
            product4.Category = categories.Find(c => c.Id == product4.CategoryId);
            product4.Manufacturer = manufacturers.Find(m => m.Id == product4.ManufacturerId);
            product4.Supplier = suppliers.Find(s => s.Id == product4.SupplierId);
            product4.SupplierName = product4.Supplier?.Name ?? "";
            products.Add(product4);

            return products;
        }
    }
}