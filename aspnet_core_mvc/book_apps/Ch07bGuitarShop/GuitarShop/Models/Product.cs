using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GuitarShop.Models
{
    public class Product
    {
        // EF will instruct the database to automatically generate this value
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryID { get; set; }  // foreign key property

        [Required(ErrorMessage = "Please enter a product code.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a product name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a product price.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Slug => Name.Replace(' ', '-');

        [ValidateNever]
        public Category Category { get; set; } = null!;

    }
}