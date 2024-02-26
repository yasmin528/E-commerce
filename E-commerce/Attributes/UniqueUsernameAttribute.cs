using E_commerce.Entities;
using E_commerce.Models;
using System.ComponentModel.DataAnnotations;


namespace Task.Attributes
{
    public class UniqueUsernameAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(validationContext.ObjectInstance is User user && user.Id == 0)
            {
                string username = value.ToString();
                EcommerceContext UniContext = new EcommerceContext();
                var res = UniContext.Users.Any(x => x.Username == username);
                if (res == false)
                {
                    return ValidationResult.Success;
                }  
                return new ValidationResult("Username already exist");

            }
            else if (validationContext.ObjectInstance is User user1)
            {
                string username = value.ToString();
                EcommerceContext UniContext = new EcommerceContext();
                var res = UniContext.Users.Any(x => x.Username == username && x.Id != user1.Id);
                if (res == false)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Name already exist");
            }
            return ValidationResult.Success;
        }
    }
}
