using Client.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        //Categories -------------------------------------
        public async Task<ActionResult> Categories()
        {
            var categories = await _apiService.GetAsync<List<Category>>("categories");
            return View(categories);
        }

        public async Task<ActionResult> Category(int id)
        {
            var category = await _apiService.GetAsync<Category>($"categories/{id}");
            return View(category);
        }

        
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
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


        [HttpGet]
        public async Task<ActionResult> UpdateCategory(int id)
        {
            var category = await _apiService.GetAsync<Category>($"categories/{id}");
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
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
            var data = await _apiService.GetAsync<Category>($"categories/products/{id}");
            return View(data);
        }



        //Products -------------------------------------



        public async Task<ActionResult> Products()
        {
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
            var data = await _apiService.GetAsync<Product>($"products/{id}");
            int categoryId = data.CategoryId;
            data.Category = await _apiService.GetAsync<Category>($"categories/{categoryId}");

            return View(data);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
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


        [HttpGet]
        public async Task<ActionResult> UpdateCategory(int id)
        {
            var category = await _apiService.GetAsync<Category>($"categories/{id}");
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
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


        //Users -------------------------------------

        public async Task<ActionResult> Customers()
        {
            var data = await _apiService.GetAsync<List<User>>("users");
            var customerUsers = data.Where(user => user.Role == "Customer").ToList();
            return View(customerUsers);
        }
        
        public async Task<ActionResult> Customer(int id)
        {
            var data = await _apiService.GetAsync<User>($"users/{id}");
            //data.Category = await _apiService.GetAsync<Category>($"categories/{categoryId}");

            return View(data);
        }
        

        //Orders -------------------------------------

        public async Task<ActionResult> Orders()
        {
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
            var data = await _apiService.GetAsync<Order>($"orders/orderitems/{id}");
            foreach (var order in data.OrderItems)
            {
                order.Product = await _apiService.GetAsync<Product>($"products/{id}");
            }

            return View(data);
        }
        


    }
}