namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public class Warehouse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long OrganizationId { get; set; }

        public Warehouse() { }

        public Warehouse(string name, string address, long organizationId)
        {
            Name = name;
            Address = address;
            OrganizationId = organizationId;
        }

        public override string ToString() => Name;
    }
}