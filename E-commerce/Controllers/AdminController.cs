using E_commerce.Entities;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace E_commerce.Controllers
{
    public class AdminController : Controller
    {
        EcommerceContext DBContext = new EcommerceContext();
        public IActionResult ViewProductsTable()
        {
            List<Product> products = DBContext.Products.ToList();
            ViewBag.Categories = DBContext.Categories.ToList();
            Dictionary<int, string> categoryDictionary = DBContext.Categories.ToDictionary(c => c.Id, c => c.Name);
            ViewBag.CategoryDictionary = categoryDictionary;
            return View(products);
        }
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product product = DBContext.Products.Where(x=>x.Id == id).FirstOrDefault();
            ViewBag.categories = DBContext.Categories.ToList();
            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product product,string image)
        {
            if (product.ImagePath == null)
            {
                product.ImagePath = image;
            }

            if (ModelState.IsValid || ModelState["ImagePath"].ValidationState == ModelValidationState.Invalid&& product.ImagePath != null&& ModelState.ErrorCount==1)
            {
                DBContext.Update(product);
                DBContext.SaveChanges();
                return RedirectToAction("ViewProductsTable");
            }
            ViewBag.categories = DBContext.Categories.ToList();
            return View(product);
        }
        public IActionResult DeleteProduct(int id)
        {
            Product p = DBContext.Products.FirstOrDefault(x => x.Id == id);
            DBContext.Remove(p);
            DBContext.SaveChanges();
            ViewBag.categories = DBContext.Categories.ToList();
            return RedirectToAction("ViewProductsTable");
        }
        [HttpGet]
        public IActionResult NewProduct()
        {
            ViewBag.categories = DBContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult NewProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                DBContext.Add(product);
                DBContext.SaveChanges();
                return RedirectToAction("ViewProductsTable");
            }
            ViewBag.categories = DBContext.Categories.ToList();
            return View(product);
        }
        public IActionResult ViewUsersTable()
        {
            List<User> users = DBContext.Users.ToList();
            return View(users);
        }
        public IActionResult ViewCategoriesTable()
        {
            List<Category> categories = DBContext.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            Category category = DBContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category, string image)
        {
            if (category.ImagePath == null)
            {
                category.ImagePath = image;
            }

            if (ModelState.IsValid || ModelState["ImagePath"].ValidationState == ModelValidationState.Invalid && category.ImagePath != null && ModelState.ErrorCount == 1)
            {
                DBContext.Update(category);
                DBContext.SaveChanges();
                return RedirectToAction("ViewCategoriesTable");
            }
            ViewBag.categories = DBContext.Categories.ToList();
            return View(category);
        }
        [HttpGet]
        public IActionResult Newcategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Newcategory(Category category)
        {
            if (ModelState.IsValid)
            {
                DBContext.Add(category);
                DBContext.SaveChanges();
                return RedirectToAction("ViewCategoriesTable");
            }
            return View(category);
        }
        public IActionResult DeleteCategory(int id)
        {
            List<Product> products = DBContext.Products.Where(x => x.CategoryId == id).ToList();
            foreach (Product p in products)
            {
                DBContext.Remove(p);
            }
            Category category = DBContext.Categories.FirstOrDefault(x => x.Id == id);
            DBContext.Remove(category);
            DBContext.SaveChanges();
            return RedirectToAction("ViewCategoriesTable");
        }
        public IActionResult ViewOrderTable()
        {
            List<Order> orders = DBContext.Orders.ToList();
            ViewBag.users = DBContext.Users.ToList();
            Dictionary<int, string> userDictionary = DBContext.Users.ToDictionary(c => c.Id, c => c.FirstName + " " + c.LastName) ;
            ViewBag.userDictionary = userDictionary;
            ViewBag.orderitems = DBContext.OrderItems.ToList();
            return View(orders);
        }
    }
}
