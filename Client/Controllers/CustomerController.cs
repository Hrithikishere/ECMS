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
    public class CustomerController : Controller
    {
        private readonly ApiService _apiService;
        public CustomerController()
        {
            _apiService = new ApiService();
        }

        public async Task<ActionResult> Index()
        {
            var categories = await _apiService.GetAsync<List<Category>>("categories");
            return View(categories);
        }


        //Categories   --------------------------

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
        public async Task<ActionResult> CategoryProducts(int id)
        {
            var data = await _apiService.GetAsync<Category>($"categories/products/{id}");
            return View(data);
        }

        //Products   --------------------------



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

        //Cart   --------------------------



        //Orders   --------------------------


        public async Task<ActionResult> Orders()
        {
            var data = await _apiService.GetAsync<User>("users/orderitems/3");
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



    }
}