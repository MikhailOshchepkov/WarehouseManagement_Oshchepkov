namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Manufacturer() { }

        public Manufacturer(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}