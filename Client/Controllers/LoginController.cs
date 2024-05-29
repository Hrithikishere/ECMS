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
    public class LoginController : Controller
    {
        private readonly ApiService _apiService;
        public LoginController()
        {
            _apiService = new ApiService();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await _apiService.PostAsync<Token, LoginModel>("login", loginModel);
                    if (token != null)
                    {
                        Session["AuthToken"] = token.TKey;

                        if (token.UserRole == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Customer");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid username or password.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred during login. Please try again.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please correct the errors and try again.";
            }
            return View();
        }





        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
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
    }
}