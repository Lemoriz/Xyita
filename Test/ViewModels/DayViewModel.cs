using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Test.Annotations;
using Backend;


namespace Test.ViewModels
{
    public class DayViewModel 
    {

        public DayViewModel()
        {
            date = DateTime.Now;
            Ebala = new List<string>{"Huita", "pizda", "anan sigin"};
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;

            }
        }


        public List<string> Ebala { get; set; }

    }
}
