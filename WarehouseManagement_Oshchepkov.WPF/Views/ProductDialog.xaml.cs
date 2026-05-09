using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.WPF
{
    public partial class ProductDialog : Window
    {
        public Product? Product { get; private set; }

        public ProductDialog(List<Category> categories,
                              List<Manufacturer> manufacturers,
                              List<Supplier> suppliers,
                              Product? existingProduct = null)
        {
            InitializeComponent();

            CmbCategory.ItemsSource = categories;
            CmbManufacturer.ItemsSource = manufacturers;
            CmbSupplier.ItemsSource = suppliers;

            if (existingProduct != null)
            {
                Title = "Edit Product";
                Product = existingProduct;

                TxtName.Text = existingProduct.Name;
                TxtUnit.Text = existingProduct.Unit;
                TxtPrice.Text = existingProduct.Price.ToString();
                TxtDiscount.Text = existingProduct.DiscountPercent.ToString();
                TxtStockQuantity.Text = existingProduct.StockQuantity.ToString();
                TxtDescription.Text = existingProduct.Description;

                CmbCategory.SelectedValue = existingProduct.CategoryId;
                CmbManufacturer.SelectedValue = existingProduct.ManufacturerId;
                CmbSupplier.SelectedValue = existingProduct.SupplierId;
            }
            else
            {
                Title = "Add Product";
                Product = new Product();

                if (categories.Any()) CmbCategory.SelectedIndex = 0;
                if (manufacturers.Any()) CmbManufacturer.SelectedIndex = 0;
                if (suppliers.Any()) CmbSupplier.SelectedIndex = 0;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Enter product name", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(TxtPrice.Text, out var price))
            {
                MessageBox.Show("Enter valid price", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Price must be greater than 0", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Product!.Name = TxtName.Text;
            Product.Unit = TxtUnit.Text;
            Product.Price = price;
            Product.DiscountPercent = decimal.TryParse(TxtDiscount.Text, out var discount) ? discount : 0;
            Product.StockQuantity = int.TryParse(TxtStockQuantity.Text, out var stock) ? stock : 0;
            Product.Description = TxtDescription.Text;

            if (CmbCategory.SelectedItem is Category category)
            {
                Product.CategoryId = category.Id;
                Product.Category = category;
            }

            if (CmbManufacturer.SelectedItem is Manufacturer manufacturer)
            {
                Product.ManufacturerId = manufacturer.Id;
                Product.Manufacturer = manufacturer;
            }

            if (CmbSupplier.SelectedItem is Supplier supplier)
            {
                Product.SupplierId = supplier.Id;
                Product.Supplier = supplier;
                Product.SupplierName = supplier.Name;
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}