using Microsoft.AspNetCore.Mvc;
using SEDC.Homework.Class02.ClientApp.DataModels;
using SEDC.Homework.Class02.ClientApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.Homework.Class02.ClientApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiService _apiService;

        public UserController (IApiService apiService )
        {
            _apiService = apiService;
        }
        [HttpGet]
        public IActionResult Users()
        {
            var response = _apiService.GetAll().Result;

            return View(response);
        }
        [HttpGet("create")]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult CreateUser(User user)
        {
            var response = _apiService.CreateUser(user);
            if (response.Value)
            {
                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Users", ViewBag["ErrorMessage"] = "Couldn't register user");
            }
        }
    }
}
