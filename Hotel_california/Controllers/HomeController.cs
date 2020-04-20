using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hotel_california.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel_california.Controllers
{
    public class HomeController : Controller
    {
        public string returnUrl { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.HasClaim("Admin", "Yes"))
            {
                returnUrl = Url.Action("AdminPage", "Home");
                return LocalRedirect(returnUrl);
            }
            if (User.HasClaim("Waiter", "Yes"))
            {
                returnUrl = Url.Action("Waiter", "Waiter");
                return LocalRedirect(returnUrl);
            }

            if (User.HasClaim("Reception", "Yes"))
            {
                returnUrl = Url.Action("Index", "Reception");
                return LocalRedirect(returnUrl);
            }

            if (User.HasClaim("Kitchen", "Yes"))
            {
                returnUrl = Url.Action("Test", "Kitchen");
                return LocalRedirect(returnUrl);
            }

            return LocalRedirect("/Identity/Account/Login");
        }

        public IActionResult AdminPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
