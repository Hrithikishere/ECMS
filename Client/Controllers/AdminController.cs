﻿using Client.Models;
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
        public async Task<ActionResult> AddProduct()
        {
            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");

            if (ModelState.IsValid)
            {
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
            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");

            var product = await _apiService.GetAsync<Product>($"products/{id}");
            int categoryId = product.CategoryId;
            product.Category = await _apiService.GetAsync<Category>($"categories/{categoryId}");

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            ViewBag.Categories = await _apiService.GetAsync<List<Category>>("categories");

            if (ModelState.IsValid)
            {
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
            var data = await _apiService.GetAsync<List<User>>("users");
            var customerUsers = data.Where(user => user.Role == "Customer").ToList();
            return View(customerUsers);
        }
        
        public async Task<ActionResult> Customer(int id)
        {
            var data = await _apiService.GetAsync<User>($"users/{id}");
            return View(data);
        }


        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(User user)
        {
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


        [HttpGet]
        public async Task<ActionResult> UpdateCustomer(int id)
        {
            var user = await _apiService.GetAsync<User>($"users/{id}");

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCustomer(User user)
        {
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
                order.Product = await _apiService.GetAsync<Product>($"products/{order.ProductId}");
            }

            return View(data);
        }

        //Logout ------------------------------------------

        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Admin");
        }
    }
}