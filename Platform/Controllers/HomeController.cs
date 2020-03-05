using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Rokono_Control.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
