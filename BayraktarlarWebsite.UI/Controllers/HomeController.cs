﻿using Microsoft.AspNetCore.Mvc;

namespace BayraktarlarWebsite.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
