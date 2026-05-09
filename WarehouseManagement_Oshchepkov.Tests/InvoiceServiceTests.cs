using NUnit.Framework;
using WarehouseManagement_Oshchepkov.Data.Models;
using WarehouseManagement_Oshchepkov.Data.Services;
using System.Linq;

namespace WarehouseManagement_Oshchepkov.Tests.ServicesTests
{
    [TestFixture]
    public class InvoiceServiceTests
    {
        private InvoiceService _invoiceService;
        private ProductService _productService;
        private WarehouseService _warehouseService;
        private OrganizationService _orgService;

        [SetUp]
        public void Setup()
        {
            _orgService = new OrganizationService();
            _warehouseService = new WarehouseService(_orgService);
            _productService = new ProductService(_warehouseService);
            _invoiceService = new InvoiceService(_productService);
        }

        [Test]
        public void CreateInvoice_ShouldReturnNewInvoice()
        {
            var invoice = _invoiceService.CreateInvoice(InvoiceType.Incoming, 1);
            Assert.That(invoice, Is.Not.Null);
            Assert.That(invoice.Type, Is.EqualTo(InvoiceType.Incoming));
            Assert.That(invoice.WarehouseId, Is.EqualTo(1));
            Assert.That(invoice.IsConfirmed, Is.False);
        }

        [Test]
        public void AddItemToInvoice_ShouldAddItem()
        {
            var invoice = _invoiceService.CreateInvoice(InvoiceType.Incoming, 1);
            var product = _productService.GetAllProducts().First();
            var quantity = 5;
            _invoiceService.AddItemToInvoice(invoice, product.Article, quantity);
            Assert.That(invoice.Items.Count, Is.EqualTo(1));
            Assert.That(invoice.Items[0].ProductArticle, Is.EqualTo(product.Article));
            Assert.That(invoice.Items[0].Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void RemoveItemFromInvoice_ShouldRemoveItem()
        {
            var invoice = _invoiceService.CreateInvoice(InvoiceType.Incoming, 1);
            var product = _productService.GetAllProducts().First();
            _invoiceService.AddItemToInvoice(invoice, product.Article, 5);
            _invoiceService.RemoveItemFromInvoice(invoice, product.Article);
            Assert.That(invoice.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void ConfirmIncomingInvoice_ShouldIncreaseStock()
        {
            var product = _productService.GetAllProducts().First();
            var initialStock = product.StockQuantity;
            var quantity = 10;
            var invoice = _invoiceService.CreateInvoice(InvoiceType.Incoming, 1);
            _invoiceService.AddItemToInvoice(invoice, product.Article, quantity);
            var result = _invoiceService.ConfirmInvoice(invoice);
            var updatedProduct = _productService.GetByArticle(product.Article);
            Assert.That(result, Is.True);
            Assert.That(invoice.IsConfirmed, Is.True);
            Assert.That(updatedProduct.StockQuantity, Is.EqualTo(initialStock + quantity));
        }

        [Test]
        public void ConfirmOutgoingInvoice_ShouldDecreaseStock()
        {
            var product = _productService.GetAllProducts().First();
            var initialStock = product.StockQuantity;
            var quantity = 1;
            var invoice = _invoiceService.CreateInvoice(InvoiceType.Outgoing, 1);
            _invoiceService.AddItemToInvoice(invoice, product.Article, quantity);
            var result = _invoiceService.ConfirmInvoice(invoice);
            var updatedProduct = _productService.GetByArticle(product.Article);
            Assert.That(result, Is.True);
            Assert.That(invoice.IsConfirmed, Is.True);
            Assert.That(updatedProduct.StockQuantity, Is.EqualTo(initialStock - quantity));
        }

        [Test]
        public void ConfirmOutgoingInvoice_WithInsufficientStock_ShouldFail()
        {
            var product = _productService.GetAllProducts().First();
            var initialStock = product.StockQuantity;
            var quantity = initialStock + 100;
            var invoice = _invoiceService.CreateInvoice(InvoiceType.Outgoing, 1);
            _invoiceService.AddItemToInvoice(invoice, product.Article, quantity);
            var result = _invoiceService.ConfirmInvoice(invoice);
            var updatedProduct = _productService.GetByArticle(product.Article);
            Assert.That(result, Is.False);
            Assert.That(invoice.IsConfirmed, Is.False);
            Assert.That(updatedProduct.StockQuantity, Is.EqualTo(initialStock));
        }

        [Test]
        public void GetAllInvoices_ShouldReturnInvoices()
        {
            var invoice1 = _invoiceService.CreateInvoice(InvoiceType.Incoming, 1);
            var invoice2 = _invoiceService.CreateInvoice(InvoiceType.Outgoing, 1);
            var product = _productService.GetAllProducts().First();
            _invoiceService.AddItemToInvoice(invoice1, product.Article, 5);
            _invoiceService.AddItemToInvoice(invoice2, product.Article, 2);
            _invoiceService.ConfirmInvoice(invoice1);
            _invoiceService.ConfirmInvoice(invoice2);
            var invoices = _invoiceService.GetAllInvoices();
            Assert.That(invoices, Is.Not.Null);
            Assert.That(invoices.Count, Is.GreaterThanOrEqualTo(2));
        }
    }
}