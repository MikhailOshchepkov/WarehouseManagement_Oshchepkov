using System.Windows;

namespace WarehouseManagement_Oshchepkov.WPF
{
    public partial class WarehouseDialog : Window
    {
        public string WarehouseName => TxtWarehouseName.Text;
        public string WarehouseAddress => TxtWarehouseAddress.Text;

        public WarehouseDialog(string initialName = "", string initialAddress = "")
        {
            InitializeComponent();
            TxtWarehouseName.Text = initialName;
            TxtWarehouseAddress.Text = initialAddress;

            if (!string.IsNullOrEmpty(initialName))
            {
                Title = "Редактирование склада";
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtWarehouseName.Text))
            {
                MessageBox.Show("Введите название склада", "Ошибка",
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