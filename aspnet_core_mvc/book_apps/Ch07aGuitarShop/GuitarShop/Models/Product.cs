namespace GuitarShop.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public Category Category { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Slug => Name.Replace(' ', '-');
    }
}