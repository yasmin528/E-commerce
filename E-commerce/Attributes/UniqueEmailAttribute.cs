using E_commerce.Entities;
using E_commerce.Models;
using System.ComponentModel.DataAnnotations;


namespace Task.Attributes
{
    public class UniqueEmailAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(validationContext.ObjectInstance is User user && user.Id == 0)
            {
                string email = value.ToString();
                EcommerceContext UniContext = new EcommerceContext();
                var res = UniContext.Users.Any(x => x.Email == email);
                if (res == false)
                {
                    return ValidationResult.Success;
                }  
                return new ValidationResult("Email already exist");

            }
            else if (validationContext.ObjectInstance is User user1)
            {
                string email = value.ToString();
                EcommerceContext UniContext = new EcommerceContext();
                var res = UniContext.Users.Any(x => x.Email == email && x.Id != user1.Id);
                if (res == false)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Email already exist");
            }
            return ValidationResult.Success;
            return ValidationResult.Success;
        }
    }
}
