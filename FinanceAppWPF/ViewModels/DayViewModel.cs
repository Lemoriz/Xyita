using System;
using System.Collections.Generic;

namespace Test.ViewModels
{
    public class DayViewModel
    {
        public DayViewModel()
        {
            Date = DateTime.Now;
            Ebala = new List<string> {"Huita", "pizda", "anan sigin"};
        }

        public DateTime Date { get; set; }


        public List<string> Ebala { get; set; }
    }
}