using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinanceAppCore;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;


namespace FinanceAppASP.Controllers
{

    

    public class DayViewModel
    {
        public int Color { get; set; }
        public int Id { get; set; }
        public string Date { get; set; }
        public decimal DayTotal { get; set; }
        public ICollection<Purchase> PurchaseList { get; set; }

    }


    public class MainViewModel
    {
        public string[] colors = {"green", "yellow", "red" };
        private AppLogic logic;

        public MainViewModel(AppLogic logic)
        {
            this.logic = logic;
            DayList = new List<DayViewModel>();

            foreach (var variable in logic.Repository.Days)
            {
                int color = 0;
                var t = logic.CountForDay(variable.Date);

                if (t > 100)
                    color = 1;

                if (t > 1000)
                    color = 2;


                var q = new DayViewModel
                {
                    PurchaseList = variable.PurchaseList,
                    Date = variable.Date.ToString("D"),
                    Id = variable.Id = variable.Id,
                    DayTotal = logic.CountForDay(variable.Date),
                    Color = color
                };

                DayList.Add(q);
            }
        }

        public string Week =>
            logic.CountForRange(DateTime.Today, DateTime.Today.AddDays(-7)).ToString("c");


        public List<DayViewModel> DayList { get;}


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



    public class HomeController : Controller
    {

        private AppLogic logic;

        public HomeController(AppLogic logic)
        {
            this.logic = logic;
        }

        public IActionResult Index()
        {
            return View(new MainViewModel(logic));
        }


        public RedirectToActionResult AddPurchase(PurchaseWithId s)
        {
            logic.Repository.AddPurchase(s.Id, new Purchase
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

                logic.Repository.AddDay(t);
                logic.Repository.SaveData();
            }

            return RedirectToAction("Index");

        }
    }
}
