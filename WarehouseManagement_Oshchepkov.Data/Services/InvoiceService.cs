using System.Collections.Generic;
using System.Linq;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    /// ������ ��� ������ � ���������� (���������� ����)
    public class InvoiceService
    {
        private List<Invoice> _invoices;
        private ProductService _productService;
        private int _nextId;

        public InvoiceService(ProductService productService)
        {
            _productService = productService;
            _invoices = new List<Invoice>();
            _nextId = 1;
        }

        /// ������� ����� ���������
        public Invoice CreateInvoice(InvoiceType type, long warehouseId)
        {
            var invoice = new Invoice(type, warehouseId);
            // �������������� ID, ����� �� ���� ����������
            var property = typeof(Invoice).GetProperty("Id");
            if (property != null && property.CanWrite)
            {
                property.SetValue(invoice, _nextId++);
            }
            return invoice;
        }

        /// �������� ����� � ���������
        public void AddItemToInvoice(Invoice invoice, string productArticle, int quantity)
        {
            var product = _productService.GetByArticle(productArticle);
            if (product == null) return;

            var existingItem = invoice.Items.FirstOrDefault(i => i.ProductArticle == productArticle);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                invoice.Items.Add(new InvoiceItem(product, quantity));
            }
        }

        /// ������� ����� �� ���������
        public void RemoveItemFromInvoice(Invoice invoice, string productArticle)
        {
            var item = invoice.Items.FirstOrDefault(i => i.ProductArticle == productArticle);
            if (item != null)
                invoice.Items.Remove(item);
        }

        /// ����������� ��������� (�������� ������� �� ������)
        public bool ConfirmInvoice(Invoice invoice)
        {
            if (invoice.IsConfirmed)
                return false;

            // ��� ��������� ��������� ���������, ������� �� ������
            if (invoice.Type == InvoiceType.Outgoing)
            {
                foreach (var item in invoice.Items)
                {
                    var product = _productService.GetByArticle(item.ProductArticle);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        return false; // �� ������� ������
                    }
                }
            }

            // ��������� �������
            foreach (var item in invoice.Items)
            {
                int change = invoice.Type == InvoiceType.Incoming ? item.Quantity : -item.Quantity;
                _productService.UpdateStock(item.ProductArticle, change);
            }

            invoice.IsConfirmed = true;
            _invoices.Add(invoice);
            return true;
        }

        /// �������� ��� ���������
        public List<Invoice> GetAllInvoices()
        {
            return _invoices.ToList();
        }

        /// �������� ��������� �� ������
        public List<Invoice> GetInvoicesByWarehouse(long warehouseId)
        {
            return _invoices.Where(i => i.WarehouseId == warehouseId).ToList();
        }
    }
}