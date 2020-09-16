using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller 
    {
        public ViewResult Index() 
        {
            ViewData["Title"] = "Home";
            return View();
        }
        public ViewResult About()
        {
            ViewData["Title"] = "About";
            return View();
        }
        public ViewResult Contact()
        {
            ViewData["Title"] = "Contact";
            return View();
        }
    }
}
