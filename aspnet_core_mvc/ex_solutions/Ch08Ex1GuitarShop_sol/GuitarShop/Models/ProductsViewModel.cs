namespace GuitarShop.Models
{
    public class ProductsViewModel
    {
        public List<Category> Categories { get; set; } = null!;
        public List<Product> Products { get; set; } = null!;
        public string SelectedCategory { get; set; } = string.Empty;

        public string CheckActiveCategory(string category) =>
            category == SelectedCategory ? "active" : "";
    }

}
