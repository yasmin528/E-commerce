using System.ComponentModel.DataAnnotations;
using Task.Attributes;

namespace E_commerce.MetaData
{
    public class UserMetaData
    {
        [Required(ErrorMessage = "Username is required.")]
        [UniqueUsername]
        public string Username { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [UniqueEmail]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=_]).*$", ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid phone number. Please enter a 11-digit number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Range(18, 100, ErrorMessage = "Age should be not less than 18 and not more than 100")]
        public int Age { get; set; }
    }
}
