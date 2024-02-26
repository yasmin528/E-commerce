using E_commerce.MetaData;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    [ModelMetadataType(typeof(UserMetaData))]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }
        public string ImagePath { get; set; }
        public char Gender { get; set; }
        public int Age { get; set; }
        public char AdminOrCustomer { get; set; }
        public virtual ICollection<Carts>? Cart { get; set;}
        public virtual ICollection<Order>? Order { get; set; }
    }
}
