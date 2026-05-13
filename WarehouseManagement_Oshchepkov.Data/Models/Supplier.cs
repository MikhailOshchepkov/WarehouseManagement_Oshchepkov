namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Supplier() { }

        public Supplier(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}