using System.Windows;

namespace WarehouseManagement_Oshchepkov.WPF
{
    public partial class OrganizationDialog : Window
    {
        public string OrganizationName => TxtOrganizationName.Text;

        public OrganizationDialog(string initialName = "")
        {
            InitializeComponent();
            TxtOrganizationName.Text = initialName;

            if (!string.IsNullOrEmpty(initialName))
            {
                Title = "�������������� �����������";
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtOrganizationName.Text))
            {
                MessageBox.Show("������� ������������ �����������", "������",
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