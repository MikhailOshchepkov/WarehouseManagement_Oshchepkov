namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public class InvoiceItem
    {
        public string ProductArticle { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Product? Product { get; set; }

        public InvoiceItem() { }

        public InvoiceItem(Product product, int quantity)
        {
            Product = product;
            ProductArticle = product.Article;
            ProductName = product.Name;
            Quantity = quantity;
            Price = product.Price;
        }
    }
}