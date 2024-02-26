using E_commerce.Models;

namespace E_commerce.ModelView
{
    public class CartModelView
    {
        public int UserId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
    }
}
