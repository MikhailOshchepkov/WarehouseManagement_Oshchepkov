using System.Collections.Generic;
using System.Linq;
using WarehouseManagement_Oshchepkov.Data.Models;

namespace WarehouseManagement_Oshchepkov.Data.Services
{
    /// Сервис для работы с накладными (партионный учет)
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

        /// Создать новую накладную
        public Invoice CreateInvoice(InvoiceType type, long warehouseId)
        {
            var invoice = new Invoice(type, warehouseId);
            // Перезаписываем ID, чтобы не было конфликтов
            var property = typeof(Invoice).GetProperty("Id");
            if (property != null && property.CanWrite)
            {
                property.SetValue(invoice, _nextId++);
            }
            return invoice;
        }

        /// Добавить товар в накладную
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

        /// Удалить товар из накладной
        public void RemoveItemFromInvoice(Invoice invoice, string productArticle)
        {
            var item = invoice.Items.FirstOrDefault(i => i.ProductArticle == productArticle);
            if (item != null)
                invoice.Items.Remove(item);
        }

        /// Подтвердить накладную (изменить остатки на складе)
        public bool ConfirmInvoice(Invoice invoice)
        {
            if (invoice.IsConfirmed)
                return false;

            // Для расходной накладной проверяем, хватает ли товара
            if (invoice.Type == InvoiceType.Outgoing)
            {
                foreach (var item in invoice.Items)
                {
                    var product = _productService.GetByArticle(item.ProductArticle);
                    if (product == null || product.StockQuantity < item.Quantity)
                    {
                        return false; // Не хватает товара
                    }
                }
            }

            // Обновляем остатки
            foreach (var item in invoice.Items)
            {
                int change = invoice.Type == InvoiceType.Incoming ? item.Quantity : -item.Quantity;
                _productService.UpdateStock(item.ProductArticle, change);
            }

            invoice.IsConfirmed = true;
            _invoices.Add(invoice);
            return true;
        }

        /// Получить все накладные
        public List<Invoice> GetAllInvoices()
        {
            return _invoices.ToList();
        }

        /// Получить накладные по складу
        public List<Invoice> GetInvoicesByWarehouse(long warehouseId)
        {
            return _invoices.Where(i => i.WarehouseId == warehouseId).ToList();
        }
    }
}