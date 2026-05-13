using System.Windows;

namespace WarehouseManagement_Oshchepkov.WPF
{
    public partial class ConfirmDeleteDialog : Window
    {
        private string _orgName;

        public ConfirmDeleteDialog(string organizationName)
        {
            InitializeComponent();
            _orgName = organizationName;

            // ���������� �������� �����������, ������� �������
            TxtOrgName.Text = organizationName;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TxtConfirmName.Text == _orgName)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("��������� �������� �� ��������� � ��������� �����������",
                    "������", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtConfirmName.Text = "";
                TxtConfirmName.Focus();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}