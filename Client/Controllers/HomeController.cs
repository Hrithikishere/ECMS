using BLL.DTOs;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;
        public HomeController() {

            _apiService = new ApiService();
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Categories()
        {
            var data = await _apiService.GetAsync<List<CategoryDTO>>("categories");
            return View(data);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}