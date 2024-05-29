using Client.Models;
using Client.Services;
using ECMS.Models;
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
                var token = await _apiService.PostAsync<Token, LoginModel>("login", loginModel);
                if (token!=null)
                {
                    if(token.UserRole=="Admin")
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
                    ModelState.AddModelError("", "Error.");
                }
            }
            return View();
        }
    }
}