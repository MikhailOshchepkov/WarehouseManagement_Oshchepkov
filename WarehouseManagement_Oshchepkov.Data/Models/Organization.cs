namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public class Organization
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Organization() { }

        public Organization(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}