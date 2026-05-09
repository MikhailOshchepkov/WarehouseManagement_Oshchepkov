using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using WarehouseManagement_Oshchepkov.Data.Models;
using WarehouseManagement_Oshchepkov.Data.Services;

namespace WarehouseManagement_Oshchepkov.WPF
{
    public partial class ProductsWindow : Window
    {
        private ProductService _productService;
        private long _organizationId;
        private string _organizationName;
        private Warehouse _warehouse;
        private InvoiceService _invoiceService;

        public ProductsWindow(long organizationId, string organizationName, Warehouse warehouse)
        {
            InitializeComponent();
            _organizationId = organizationId;
            _organizationName = organizationName;
            _warehouse = warehouse;

            var warehouseService = new WarehouseService(new OrganizationService());
            _productService = new ProductService(warehouseService);
            _invoiceService = new InvoiceService(_productService);

            TxtContextOrg.Text = $"Организация: {_organizationName}";
            TxtContextWarehouse.Text = $"Склад: {_warehouse.Name}";
            LoadProducts();
        }

        private void LoadProducts()
        {
            LvProducts.ItemsSource = _productService.GetProductsByWarehouse(_warehouse.Id);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialog(_productService.GetAllCategories(),
                                           _productService.GetAllManufacturers(),
                                           _productService.GetAllSuppliers());
            if (dialog.ShowDialog() == true && dialog.Product != null)
            {
                _productService.Add(dialog.Product);
                LoadProducts();
                MessageBox.Show($"Товар \"{dialog.Product.Name}\" добавлен", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = LvProducts.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new ProductDialog(_productService.GetAllCategories(),
                                           _productService.GetAllManufacturers(),
                                           _productService.GetAllSuppliers(),
                                           selectedProduct);
            if (dialog.ShowDialog() == true && dialog.Product != null)
            {
                _productService.Update(dialog.Product);
                LoadProducts();
                MessageBox.Show($"Товар \"{dialog.Product.Name}\" обновлен", "Успешно",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = LvProducts.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Удалить товар \"{selectedProduct.Name}\"?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _productService.Delete(selectedProduct.Article);
                LoadProducts();
            }
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Выберите CSV файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var lines = System.IO.File.ReadAllLines(openFileDialog.FileName);
                    int imported = 0;

                    for (int i = 1; i < lines.Length; i++)
                    {
                        var parts = lines[i].Split(',');
                        if (parts.Length >= 3)
                        {
                            var product = new Product
                            {
                                Name = parts[0].Trim(),
                                Price = decimal.TryParse(parts[1], out var price) ? price : 0,
                                StockQuantity = int.TryParse(parts[2], out var stock) ? stock : 0
                            };
                            _productService.Add(product);
                            imported++;
                        }
                    }

                    LoadProducts();
                    MessageBox.Show($"Импортировано товаров: {imported}", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnInvoiceIn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InvoiceDialog(_productService.GetAllProducts(), InvoiceType.Incoming);
            if (dialog.ShowDialog() == true && dialog.Invoice != null)
            {
                dialog.Invoice.WarehouseId = _warehouse.Id;
                if (_invoiceService.ConfirmInvoice(dialog.Invoice))
                {
                    LoadProducts();
                    MessageBox.Show("Приходная накладная проведена", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при проведении накладной", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnInvoiceOut_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InvoiceDialog(_productService.GetAllProducts(), InvoiceType.Outgoing);
            if (dialog.ShowDialog() == true && dialog.Invoice != null)
            {
                dialog.Invoice.WarehouseId = _warehouse.Id;
                if (_invoiceService.ConfirmInvoice(dialog.Invoice))
                {
                    LoadProducts();
                    MessageBox.Show("Расходная накладная проведена", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Недостаточно товара на складе", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var warehousesWindow = new WarehousesWindow(_organizationId, _organizationName);
            warehousesWindow.Show();
            this.Close();
        }
    }
}