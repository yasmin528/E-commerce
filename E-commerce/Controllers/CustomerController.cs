using E_commerce.Entities;
using E_commerce.Models;
using E_commerce.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace E_commerce.Controllers
{
    public class CustomerController : Controller
    {
        EcommerceContext DBContext = new EcommerceContext();
        [HttpGet]
        public IActionResult ViewProduct()
        {
            var Products = DBContext.Products.ToList();
            ViewBag.categories = DBContext.Categories.ToList();
            ViewBag.selectedCategory = -1;
            return View(Products);
        }
        [HttpGet]
        public IActionResult filter(int Id)
        {
            var Products = DBContext.Products.Where(x => x.CategoryId == Id).ToList();
            ViewBag.categories = DBContext.Categories.ToList();
            ViewBag.selectedCategory = Id;
            return View("ViewProduct", Products);
        }
        [HttpGet]
        public IActionResult ShowDetails(int Id)
        {
            var Product = DBContext.Products.Where(x => x.Id == Id).FirstOrDefault();
            return View(Product);
        }
        [HttpGet]
        public IActionResult showCart()
        {
            int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var Cart1 = DBContext.Carts.Where(x => x.UserId == UserId).FirstOrDefault();
            var cartItems = DBContext.CartItems.Where(x => x.CartID == Cart1.Id).ToList();
            List<CartModelView> items = new List<CartModelView>();
            if (cartItems != null)
            {
                foreach (var cart in cartItems)
                {
                    var product = DBContext.Products.Where(x => x.Id == cart.ProductId).FirstOrDefault();
                    items.Add(new CartModelView
                    {
                        UserId = UserId,
                        CartId = Cart1.Id,
                        quantity = cart.Quantity,
                        Product = product,
                        totalPrice = cart.Quantity * product.Price
                    });
                }
            }
            ViewBag.count = items.Count;
            return View(items);
        }
        [HttpGet]
        public IActionResult AddToCart(int PId, int quantity)
        {
            var product = DBContext.Products.Where(x => x.Id == PId).FirstOrDefault();
            if (product.Quantity >= quantity)
            {
                int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                var cartID = DBContext.Carts.Where(x => x.UserId == UserId).FirstOrDefault().Id;
                //add cart item
                var samePandU = DBContext.CartItems.Where(x => x.ProductId == PId && x.CartID == cartID).FirstOrDefault();
                if (samePandU == null)
                {
                    CartItem cartItems = new CartItem();
                    cartItems.Quantity = quantity;
                    cartItems.CartID = cartID;
                    cartItems.ProductId = PId;
                    DBContext.Update(cartItems);
                }
                else
                {
                    int cartAndDetails = samePandU.Quantity + quantity;
                    if(cartAndDetails <= product.Quantity)
                    {
                        samePandU.Quantity += quantity;
                    }
                    else
                    {
                        samePandU.Quantity = quantity;
                    }
                    DBContext.Update(samePandU);
                }
                DBContext.SaveChanges();
            }
            var Product = DBContext.Products.Where(x => x.Id == PId).FirstOrDefault();
            return View("ShowDetails", Product);
        }
        [HttpGet]
        public IActionResult EditProfile()
        {
            int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            User user = DBContext.Users.Where(x => x.Id == UserId).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public IActionResult EditProfile(User user, string image)
        {
            user.AdminOrCustomer = 'C';
            if (user.ImagePath == null)
            {
                user.ImagePath = image;
            }
            else
            {
                HttpContext.Session.Remove("Image");
                HttpContext.Session.SetString("Image", user.ImagePath);
            }
            if (ModelState.IsValid || ModelState["ImagePath"].ValidationState == ModelValidationState.Invalid && user.ImagePath != null && ModelState.ErrorCount == 1)
            {
                DBContext.Update(user);
                DBContext.SaveChanges();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            if (order.TotalAmount == 0) { return RedirectToAction("showCart"); }
            if (order != null)
            {
                order.OrderDate = DateTime.Now;
                var cartID = DBContext.Carts.Where(x => x.UserId == order.UserId).FirstOrDefault().Id;
                var cartItems = DBContext.CartItems.Where(x=>x.CartID==cartID).ToList();
                DBContext.Update(order);
                DBContext.SaveChanges();
                foreach (var item in cartItems)
                {
                    var product = DBContext.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();

                    var orderItem = new OrderItem();
                    orderItem.OrderId = order.Id;
                    orderItem.ProductName = product.Name;
                    orderItem.ProductQuantity = item.Quantity;
                    orderItem.ProductPrice = item.Quantity * product.Price;
                    DBContext.Add(orderItem); 
                    product.Quantity -= item.Quantity;
                    DBContext.Remove(item);
                    if(product.Quantity == 0)
                        DBContext.Remove(product);
                    else
                        DBContext.Update(product);
                    DBContext.SaveChanges();
                }
            }
            return RedirectToAction("showCart");
        }
        [HttpPost]
        public ActionResult UpdateProductQuantity(int productId, int newQuantity)
        {
            // Retrieve the product from the database based on the productId
            var product = DBContext.Products.FirstOrDefault(p => p.Id == productId);
            
            if (product != null)
            {
                int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                Carts cart = DBContext.Carts.Where(x => x.UserId == UserId).FirstOrDefault();
                var cartItem = DBContext.CartItems.FirstOrDefault(x => x.CartID == cart.Id && x.ProductId == productId);
                cartItem.Quantity = newQuantity;
                // Save changes to the database
                DBContext.SaveChanges();

                // Return a success message or JSON object
                return Json(new { success = true, message = "Product quantity updated successfully" });
            }

            // Return an error message or JSON object if the product is not found
            return Json(new { success = false, message = "Product not found" });
        }
        [HttpPost]
        public ActionResult RemoveProductQuantity(int productId)
        {
            // Retrieve the product from the database based on the productId
            var product = DBContext.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                Carts cart = DBContext.Carts.Where(x => x.UserId == UserId).FirstOrDefault();
                var cartItem = DBContext.CartItems.FirstOrDefault(x => x.CartID == cart.Id && x.ProductId == productId);
                DBContext.Remove(cartItem);
                // Save changes to the database
                DBContext.SaveChanges();

                // Return a success message or JSON object
                return Json(new { success = true, message = "Product quantity removed successfully" });
            }

            // Return an error message or JSON object if the product is not found
            return Json(new { success = false, message = "Product not found" });
        }
    }
}

