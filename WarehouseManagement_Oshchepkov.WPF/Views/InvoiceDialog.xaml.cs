using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.WPF
{
    public partial class InvoiceDialog : Window
    {
        public Invoice? Invoice { get; private set; }
        private List<Product> _products;
        private InvoiceType _type;

        public InvoiceDialog(List<Product> products, InvoiceType type)
        {
            InitializeComponent();
            _products = products;
            _type = type;

            Invoice = new Invoice(type, 0);

            Title = type == InvoiceType.Incoming ? "Приходная накладная" : "Расходная накладная";

            CmbProducts.ItemsSource = products;
            if (products.Any())
                CmbProducts.SelectedIndex = 0;

            UpdateTotal();
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (CmbProducts.SelectedItem is not Product product)
            {
                MessageBox.Show("Выберите товар", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TxtQuantity.Text, out var quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existingItem = Invoice!.Items.FirstOrDefault(i => i.ProductArticle == product.Article);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Invoice.Items.Add(new InvoiceItem(product, quantity));
            }

            LvItems.ItemsSource = Invoice.Items.ToList();
            UpdateTotal();

            TxtQuantity.Text = "1";
            CmbProducts.Focus();
        }

        private void UpdateTotal()
        {
            var total = Invoice!.Items.Sum(i => i.Quantity * i.Price);
            TxtTotal.Text = $"{total:N0} руб.";
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Invoice!.Items.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар в накладную", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
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