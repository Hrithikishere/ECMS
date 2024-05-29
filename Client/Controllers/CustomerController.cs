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
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var categories = await _apiService.GetAsync<List<Category>>("categories");
            return View(categories);
        }


        //Categories   --------------------------

        public async Task<ActionResult> Categories()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var categories = await _apiService.GetAsync<List<Category>>("categories");
            return View(categories);
        }

        public async Task<ActionResult> Category(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

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

        //Cart   --------------------------

        public async Task<ActionResult> Cart()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var data = await _apiService.GetAsync<User>("users/cartitems/3");  
            var cart = await _apiService.GetAsync<Cart>("carts/cartitems/2");

            foreach(var cartItem in cart.CartItems)
            {
                cartItem.Product = await _apiService.GetAsync<Product>($"products/{cartItem.ProductId}");
            }
            return View(cart);
        }

        public async Task<ActionResult> IncreaseQuantity(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            //var product = await _apiService.GetAsync<Product>($"products/{productId}");
            var cart = await _apiService.GetAsync<Cart>("carts/cartitems/2");
            
            foreach (var cartItem in cart.CartItems)
            {
                if(cartItem.Id == id)
                {
                    cartItem.Quantity = cartItem.Quantity + 1;
                }
            }
            var success = await _apiService.PostAsync("carts/update", cart);
            return RedirectToAction("Cart", "Customer");

        }
        
        public async Task<ActionResult> DecreaseQuantity(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            //var product = await _apiService.GetAsync<Product>($"products/{productId}");
            var cart = await _apiService.GetAsync<Cart>("carts/cartitems/2");
            var removeItem = new CartItem();
            foreach (var cartItem in cart.CartItems)
            {
                if(cartItem.Id == id)
                {
                    if (cartItem.Quantity >= 2)
                    {
                        cartItem.Quantity = cartItem.Quantity - 1;
                    }
                    else
                    {
                        removeItem = cartItem;
                    }
                }
            }

            if(removeItem != null)
            {
                cart.CartItems.Remove(removeItem);
            }
            var success = await _apiService.PostAsync("carts/update", cart);
            return RedirectToAction("Cart", "Customer");

        }

        
        public async Task<ActionResult> AddToCart(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var product = await _apiService.GetAsync<Product>($"products/{id}");
            //var data = await _apiService.GetAsync<User>("users/cartitems/3");
            var cart = await _apiService.GetAsync<Cart>("carts/cartitems/2");

            CartItem existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == product.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
            }
            else
            {
                CartItem cartItem = new CartItem
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    CartId = cart.Id
                };

                cart.CartItems.Add(cartItem);
            }

            var success = await _apiService.PostAsync("carts/update", cart);
            if (success)
            {
                return RedirectToAction("Cart", "Customer");
            }

            return RedirectToAction("Cart", "Customer");
        }




        //Orders   --------------------------

        public async Task<ActionResult> Orders()
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            _apiService.SetAuthToken(token);

            var data = await _apiService.GetAsync<User>("users/orderitems/2");
            return View(data);
        }

        public async Task<ActionResult> OrderProducts(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            _apiService.SetAuthToken(token);

            var data = await _apiService.GetAsync<Order>($"orders/orderitems/{id}");
            foreach (var order in data.OrderItems)
            {

                order.Product = await _apiService.GetAsync<Product>($"products/{order.ProductId}");
            }
            return View(data);
        }

        public async Task<ActionResult> PlaceOrder(int id)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var cart = await _apiService.GetAsync<Cart>($"carts/cartitems/{id}");

            foreach (var cartItem in cart.CartItems)
            {
                cartItem.Product = await _apiService.GetAsync<Product>($"products/{cartItem.ProductId}");
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(FormCollection formCollection)
        {
            var token = Session["AuthToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Login");
            }

            var cart = await _apiService.GetAsync<Cart>($"carts/cartitems/{formCollection["CartId"]}");

            foreach (var cartItem in cart.CartItems)
            {
                cartItem.Product = await _apiService.GetAsync<Product>($"products/{cartItem.ProductId}");
            }

            Order order = new Order();
            order.CustomerId = 2;
            order.OrderDate = DateTime.Now;
            order.Status = "Placed";
            order.ShippingAddress = formCollection["ShippingAddress"];
            order.BillingAddress = formCollection["BillingAddress"];
            order.TotalAmount = 0;
            foreach (var cartItem in cart.CartItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ProductId = cartItem.ProductId;
                orderItem.Quantity = cartItem.Quantity;
                orderItem.UnitPrice = cartItem.Product.Price;
                orderItem.TotalPrice = cartItem.Product.Price * cartItem.Quantity;

                order.OrderItems.Add( orderItem );

                order.TotalAmount = order.TotalAmount + Convert.ToInt32(orderItem.TotalPrice);
            }

            var success = await _apiService.PostAsync("orders/create", order);
            if (success)
            {

                var cartInfo = await _apiService.GetAsync<Cart>("carts/cartitems/2");
                cartInfo.CartItems.Clear();

                var cleared = await _apiService.PostAsync("carts/update", cartInfo);
                
                return RedirectToAction("Orders", "Customer");

            }
            else
            {
                return View(cart);
            }
        }
        
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
                return RedirectToAction("Index", "Customer");
            }
        }
    }
}