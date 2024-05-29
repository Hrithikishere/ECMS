using Client.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiService _apiService;
        public AdminController()
        {
            _apiService = new ApiService();
        }
        public ActionResult Index()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        //Categories -------------------------------------
        public async Task<ActionResult> Categories()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            try
            {
                var categories = await _apiService.GetAsync<List<Category>>("categories");
                return View(categories);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching the categories. Please try again later.";
                return View(new List<Category>());
            }

        }

        public async Task<ActionResult> Category(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            try
            {
                var category = await _apiService.GetAsync<Category>($"categories/{id}");
                return View(category);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching the category. Please try again later.";
                return View(new Category());
            }
        }


        [HttpGet]
        public async Task<ActionResult> AddCategory()
        {
            try
            {
                var token = Session["AuthToken"]?.ToString();
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Login");
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the add category page.";
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
            try
            {
                var token = Session["AuthToken"]?.ToString();
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Login");
                }

                if (ModelState.IsValid)
                {
                    var success = await _apiService.PostAsync("categories/create", category);
                    if (success)
                    {
                        return RedirectToAction("Categories", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create category.");
                    }
                }

                return View(category);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while adding the category.";
                return View(category);
            }
        }



        [HttpGet]
        public async Task<ActionResult> UpdateCategory(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var category = await _apiService.GetAsync<Category>($"categories/{id}");
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid)
            {
                var success = await _apiService.PostAsync("categories/update", category);
                if (success)
                {
                    return RedirectToAction("Categories", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create category.");
                }
            }

            return View(category);
        }

        public async Task<ActionResult> DeleteCategory(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var success = await _apiService.DeleteAsync($"categories/delete/{id}");
            if (success)
            {
                return RedirectToAction("Categories", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete category.");
            }

            return RedirectToAction("Categories", "Admin");
        }

        public async Task<ActionResult> CategoryProducts(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<Category>($"categories/products/{id}");
            return View(data);
        }



        //Products -------------------------------------



        public async Task<ActionResult> Products()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<List<Product>>("products");
            foreach (var product in data)
            {
                int id = product.CategoryId;
                product.Category = await _apiService.GetAsync<Category>($"categories/{id}");
            }
            return View(data);
        }
        public async Task<ActionResult> Product(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<Product>($"products/{id}");
            int categoryId = data.CategoryId;
            data.Category = await _apiService.GetAsync<Category>($"categories/{categoryId}");

            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> AddProduct()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product, HttpPostedFileBase ImageFile)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string originalFilePath = ImageFile.FileName;

                    string directoryPath = Server.MapPath("~/Assets/Images/");

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(originalFilePath);

                    string filePath = Path.Combine(directoryPath, fileName);
                    ImageFile.SaveAs(filePath);

                    product.ImagePath = "Assets/Images/" + fileName;
                }

                var success = await _apiService.PostAsync("products/create", product);

                if (success)
                {
                    return RedirectToAction("Products", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create category.");
                }
            }

            return View(product);
        }


        [HttpGet]
        public async Task<ActionResult> UpdateProduct(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");

            var product = await _apiService.GetAsync<Product>($"products/{id}");
            int categoryId = product.CategoryId;
            product.Category = await _apiService.GetAsync<Category>($"categories/{categoryId}");

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProduct(Product product, HttpPostedFileBase ImageFile)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string originalFilePath = ImageFile.FileName;

                    string directoryPath = Server.MapPath("~/Assets/Images/");

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(originalFilePath);

                    string filePath = Path.Combine(directoryPath, fileName);
                    ImageFile.SaveAs(filePath);

                    product.ImagePath = "Assets/Images/" + fileName;
                }

                var success = await _apiService.PostAsync("products/update", product);

                if (success)
                {
                    return RedirectToAction("Products", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update product.");
                }
            }

            return View(product);
        }

        public async Task<ActionResult> DeleteProduct(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var success = await _apiService.DeleteAsync($"products/delete/{id}");
            if (success)
            {
                return RedirectToAction("Products", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete category.");
            }

            return RedirectToAction("Categories", "Admin");
        }


        //Users -------------------------------------

        public async Task<ActionResult> Customers()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<List<User>>("users");
            var customerUsers = data.Where(user => user.Role == "Customer").ToList();
            return View(customerUsers);
        }
        
        public async Task<ActionResult> Customer(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<User>($"users/{id}");
            return View(data);
        }

        /*
        [HttpGet]
        public ActionResult AddCustomer()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(User user)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid)
            {
                user.Role = "Customer";
                user.JoinDate = DateTime.Now;
                var success = await _apiService.PostAsync("users/create", user);
                if (success)
                {
                    return RedirectToAction("Customers", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create category.");
                }
            }

            return View(user);
        }
        */

        [HttpGet]
        public async Task<ActionResult> UpdateCustomer(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var user = await _apiService.GetAsync<User>($"users/{id}");

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCustomer(User user)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid)
            {
                var success = await _apiService.PostAsync("users/update", user);
                if (success)
                {
                    return RedirectToAction("Customers", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update product.");
                }
            }

            return View(user);
        }

        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var success = await _apiService.DeleteAsync($"users/delete/{id}");
            if (success)
            {
                return RedirectToAction("Customers", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete category.");
            }

            return RedirectToAction("Customers", "Admin");
        }



        //Orders -------------------------------------

        public async Task<ActionResult> Orders()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<List<Order>>("orders");
            foreach (var order in data)
            {
                int id = order.CustomerId;
                order.User = await _apiService.GetAsync<User>($"users/{id}");
            }
            return View(data);
        }
 
        public async Task<ActionResult> OrderProducts(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<Order>($"orders/orderitems/{id}");
            foreach (var order in data.OrderItems)
            {
                order.Product = await _apiService.GetAsync<Product>($"products/{order.ProductId}");
            }

            return View(data);
        }
        
        public async Task<ActionResult> EditOrder(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Status = new List<string> { "Placed", "Processing", "Delivered" };
            var data = await _apiService.GetAsync<Order>($"orders/orderitems/{id}");
            foreach (var order in data.OrderItems)
            {
                order.Product = await _apiService.GetAsync<Product>($"products/{order.ProductId}");
            }

            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> EditOrder(FormCollection formCollection)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Status = new List<string> { "Placed", "Processing", "Delivered" };
            var data = await _apiService.GetAsync<Order>($"orders/orderitems/{formCollection["OrderId"]}");
            data.Status = formCollection["Status"];

            var cleared = await _apiService.PostAsync("orders/update", data);
            if (cleared)
            {
                return RedirectToAction("Orders", "Admin");
            }
            else
            {
                return View(data);
            }
        }

        //Logout ------------------------------------------

        public async Task<ActionResult> Logout()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            LogoutModel logoutModel = new LogoutModel();
            logoutModel.Token = Session["AuthToken"].ToString();

            var success = await _apiService.PostAsync("logout", logoutModel);

            if (success)
            {
                Session["AuthToken"] = null;
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}