using E_commerce.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [UniqueCategoryName]
        [MinLength(2)]
        public string Name { get; set; }
        [MinLength(10)]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
