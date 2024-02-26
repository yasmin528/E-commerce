using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace E_commerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        //[Column(TypeName = "nvarchar(max)")]
        //public string ListOfItems { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        //[NotMapped]
        //public List<string> Items
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(ListOfItems))
        //        {
        //            return new List<string>();
        //        }
        //        return JsonConvert.DeserializeObject<List<string>>(ListOfItems);
        //    }
        //    set
        //    {
        //        ListOfItems = JsonConvert.SerializeObject(value);
        //    }
        //}
    }
}
