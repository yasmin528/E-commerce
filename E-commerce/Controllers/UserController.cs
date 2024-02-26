using E_commerce.Entities;
using E_commerce.Models;
using E_commerce.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_commerce.Controllers
{

    public class UserController : Controller
    {
        EcommerceContext DBContext = new EcommerceContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModelView user)
        {
            if (ModelState.IsValid)
            {
                var userData = DBContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
                if (userData != null)
                {
                    string userNow = userData.Email;
                    HttpContext.Session.SetString("User", userNow);
                    HttpContext.Session.SetString("UserId", userData.Id.ToString());
                    HttpContext.Session.SetString("AorC", userData.AdminOrCustomer.ToString());
                    HttpContext.Session.SetString("Image", userData.ImagePath);
                    return HttpContext.Session.GetString("AorC")== "C" ? RedirectToAction("HomePage"): RedirectToAction("ViewProductsTable","Admin");
                }
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(user);
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            user.AdminOrCustomer = 'C';
            user.ImagePath = "avatar.png";
            if (ModelState.IsValid || ModelState["ImagePath"].ValidationState == ModelValidationState.Invalid && user.ImagePath != null && ModelState.ErrorCount == 1)
            {
                DBContext.Users.Add(user);
                DBContext.SaveChanges();
                Carts cart = new Carts();
                cart.UserId = user.Id;
                DBContext.Carts.Add(cart);
                DBContext.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
        public IActionResult LogOut()
        {
            // Clear session values
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("AorC");
            HttpContext.Session.Remove("Image");

            return View("Login");
        }
        public IActionResult HomePage()
        {
            var cat = DBContext.Products.ToList();
            return View(cat);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Search(string querySearch, string returnUrl)
        {
            List<Product> products = DBContext.Products.ToList();
            List<Product> matchingProducts = products
            .Where(product => product.Name.Contains(querySearch, StringComparison.OrdinalIgnoreCase))
            .ToList();
            return View("search",matchingProducts);
        }
    }
}
