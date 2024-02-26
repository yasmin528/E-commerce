using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Carts
    {
        public int Id { get; set; }
      
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }
}
