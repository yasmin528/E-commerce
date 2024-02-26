using E_commerce.Entities;
using E_commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Attributes
{
    public class UniqueProductName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is Product product && product.Id == 0)
            {
                string name = value.ToString();
                EcommerceContext UniContext = new EcommerceContext();
                var res = UniContext.Products.Any(x => x.Name == name);
                if (res == false)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Name already exist");

            }
            else if (validationContext.ObjectInstance is Product product1)
            {
                string name = value.ToString();
                EcommerceContext UniContext = new EcommerceContext();
                var res = UniContext.Products.Any(x => x.Name == name && x.Id != product1.Id);
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
