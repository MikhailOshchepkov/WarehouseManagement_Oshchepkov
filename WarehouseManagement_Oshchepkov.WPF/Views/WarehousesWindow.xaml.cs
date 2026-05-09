using System.Linq;
using System.Windows;
using WarehouseManagement_Oshchepkov.Data.Models;
using WarehouseManagement_Oshchepkov.Data.Services;

namespace WarehouseManagement_Oshchepkov.WPF
{
    public partial class WarehousesWindow : Window
    {
        private WarehouseService _warehouseService;
        private long _organizationId;
        private string _organizationName;
        private Warehouse? _selectedWarehouse;

        public WarehousesWindow(long organizationId, string organizationName)
        {
            InitializeComponent();
            _organizationId = organizationId;
            _organizationName = organizationName;

            // Создаем сервисы
            var orgService = new OrganizationService();
            _warehouseService = new WarehouseService(orgService);

            TxtOrganizationInfo.Text = $"Организация: {_organizationName} (ID: {_organizationId})";

            LoadWarehouses();
        }

        private void LoadWarehouses()
        {
            var warehouses = _warehouseService.GetByOrganizationId(_organizationId);
            LvWarehouses.ItemsSource = warehouses;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new WarehouseDialog();
            if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.WarehouseName))
            {
                var newWarehouse = new Warehouse(dialog.WarehouseName, dialog.WarehouseAddress, _organizationId);
                _warehouseService.Add(newWarehouse);
                LoadWarehouses();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWarehouse == null)
            {
                MessageBox.Show("Выберите склад", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new WarehouseDialog(_selectedWarehouse.Name, _selectedWarehouse.Address);
            if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.WarehouseName))
            {
                _selectedWarehouse.Name = dialog.WarehouseName;
                _selectedWarehouse.Address = dialog.WarehouseAddress;
                _warehouseService.Update(_selectedWarehouse);
                LoadWarehouses();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWarehouse == null)
            {
                MessageBox.Show("Выберите склад", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Удалить склад \"{_selectedWarehouse.Name}\"?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _warehouseService.Delete(_selectedWarehouse.Id);
                _selectedWarehouse = null;
                LoadWarehouses();
            }
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenProductsWindow();
        }

        private void LvWarehouses_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenProductsWindow();
        }

        private void OpenProductsWindow()
        {
            if (_selectedWarehouse == null)
            {
                MessageBox.Show("Выберите склад", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var productsWindow = new ProductsWindow(_organizationId, _organizationName, _selectedWarehouse);
            productsWindow.Show();
            this.Close();
        }

        private void LvWarehouses_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedWarehouse = LvWarehouses.SelectedItem as Warehouse;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var orgWindow = new OrganizationsWindow();
            orgWindow.Show();
            this.Close();
        }
    }
}