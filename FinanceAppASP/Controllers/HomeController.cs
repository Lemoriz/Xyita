using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinanceAppCore;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;


namespace FinanceAppASP.Controllers
{

    public class FF
    {
        public int Id { get; set; }

       public string Name { get; set; }
    }

    [Route("api/[controller]")]
    public class RepoController : Controller
    {

        private IDayRepository repo;

        public RepoController(IDayRepository repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        public IEnumerable<Day> Get() => repo.Days;

        [HttpGet("{id}")]
        public Day Get(int id) => repo.Days.FirstOrDefault(w => w.Id == id);

        [HttpPost]

        public void Post(Day res)
        {
            var w =  HttpContext.Request.Body;
            repo.Days.Add(res);
          
    
        }


    }

    public class HomeController : Controller
    {
        IDayRepository repo;
        private AppLogic logic;
     
        public HomeController(IDayRepository repo)
        {

            this.repo = repo;
            logic = new AppLogic(repo);
            
        }

        public IActionResult Index()
        {

            return View(repo.Days);

        }

        public class DayWithDate
        {
            public int Id { get; set; }
            public string Date { get; set; }

        }

        public class PurchaseWithId
        {
            public int Id { get; set; }
            public int Count { get; set; }
            public decimal Price { get; set; }
            public string Item { get; set; }
        }


        public RedirectToActionResult AddPurchase(PurchaseWithId s)
        {
            repo.AddPurchase(s.Id,new Purchase
            {
                Item = s.Item,
                Count = s.Count,
                Price = s.Price
                    
            });
            return RedirectToAction("Index");
        }

        public RedirectToActionResult AddDay(DayWithDate day)
        {
            Day t;

            if (DateTime.TryParse(day.Date, out var date))
            {
                t = new Day
                {
                    Date = date,
                    Id = day.Id,
                    PurchaseList = new List<Purchase>()

                };
            
                repo.AddDay(t);
                repo.SaveData();
             }

        return RedirectToAction("Index");

        }
    }
}
