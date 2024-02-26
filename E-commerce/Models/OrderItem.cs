namespace E_commerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }

    }
}
