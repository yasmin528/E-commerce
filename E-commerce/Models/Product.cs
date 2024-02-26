using E_commerce.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [UniqueProductName]
        [MinLength(3)]
        public string Name { get; set; }
        [MinLength(10)]
        public string Description { get; set; }
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }

    }
}
