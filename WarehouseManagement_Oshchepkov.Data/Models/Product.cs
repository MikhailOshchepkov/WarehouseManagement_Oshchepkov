using System;

namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public class Product
    {
        public string Article { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = "רע";
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public int StockQuantity { get; set; }
        public string? PhotoPath { get; set; }
        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public int SupplierId { get; set; }

        public Category? Category { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public Supplier? Supplier { get; set; }
        public string SupplierName { get; set; } = string.Empty;

        public Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString() => $"{Name} (ְנע. {Article})";
    }
}