﻿using ArcheryProject.Models;
using artaimusDBlib;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArcheryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //PlayerModel player = ApiHelper.GetUser(HttpContext.Session.GetString("UsernameOrEmail"));

            ApiHelper.SetUser("", null);

            return View();
        }

        public IActionResult Login()
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