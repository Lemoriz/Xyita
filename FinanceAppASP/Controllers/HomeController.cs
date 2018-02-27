using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinanceAppCore;


namespace FinanceAppASP.Controllers
{
    public class HomeController : Controller
    {
        private IDayRepository repo;
        private AppLogic logic;



    public HomeController()
        {
            repo = new JsonRepository("data.txt");
            logic = new AppLogic(repo);
            
        }

        public IActionResult Index()
        {

            return View(repo.Days);

        }

    }
}
