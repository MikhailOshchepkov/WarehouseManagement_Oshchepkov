namespace WarehouseManagement_Oshchepkov.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Category() { }

        public Category(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}