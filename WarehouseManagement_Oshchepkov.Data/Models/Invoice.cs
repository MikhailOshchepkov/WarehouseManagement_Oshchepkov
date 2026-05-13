using System;
using System.Collections.ObjectModel;

namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public enum InvoiceType
    {
        Incoming,
        Outgoing
    }

    public class Invoice
    {
        public int Id { get; set; }
        public long WarehouseId { get; set; }
        public DateTime Date { get; set; }
        public InvoiceType Type { get; set; }
        public bool IsConfirmed { get; set; }
        public ObservableCollection<InvoiceItem> Items { get; set; }

        public Invoice()
        {
            Date = DateTime.Now;
            IsConfirmed = false;
            Items = new ObservableCollection<InvoiceItem>();
        }

        public Invoice(InvoiceType type, long warehouseId) : this()
        {
            Type = type;
            WarehouseId = warehouseId;
        }

        public override string ToString()
        {
            string typeName = Type == InvoiceType.Incoming ? "������" : "������";
            return $"{typeName} ��������� �{Id} �� {Date:dd.MM.yyyy}";
        }
    }
}