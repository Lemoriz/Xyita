﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Backend
{
    public class Purchase
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

    }

    public class Day
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<Purchase> PurchaseList { get; set; }

    }

    public interface IDayRepository
    {

        ICollection<Day> Days { get; }
        ICollection<Purchase> GetPurchaseList(DateTime date);
        Day GetDay(DateTime date);

        void DeleteDay(DateTime date);
        void DeletePurchase(DateTime date, Purchase purchase);

        void GetData();
        void SaveData();

    }

    public abstract class DayRepository : IDayRepository
    {
        public abstract ICollection<Day> Days { get; }
        public abstract void GetData();
        public abstract void SaveData();

        public void DeleteDay(DateTime date)
        {
            var day = GetDay(date);
            if (day == null) return;

            Days.Remove(day);

        }

        public void DeletePurchase(DateTime date, Purchase purchase)
        {
            GetDay(date)?.PurchaseList.Remove(purchase);
        }



        public Day GetDay(DateTime date)
        {
            try
            {
                var day = Days.First(w => w.Date == date);
                return day;
            }

            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public ICollection<Purchase> GetPurchaseList(DateTime date) => GetDay(date)?.PurchaseList;
    }


    public class JsonRepository : DayRepository
    {
        public override ICollection<Day> Days => _dayList;

        private IList<Day> _dayList;
        private string _fileName;

        public JsonRepository(string filename)
        {

            _dayList = new List<Day>();
            _fileName = filename;
            GetData();
        }

        public override void GetData()
        {
            if (!File.Exists(_fileName))
            {
                SaveData();
                return;
            }

            string info = File.ReadAllText(_fileName);
            var obj = JsonConvert.DeserializeObject<JArray>(info);
            _dayList = obj.ToObject<List<Day>>();

        }

        public override void SaveData()
        {
            File.WriteAllText(_fileName,JsonConvert.SerializeObject(_dayList,Formatting.Indented));
        }
    }


    public class DayRepositoryMock : DayRepository
    {
        private List<Day> _dayList;

        public override ICollection<Day> Days => _dayList;

        public override void GetData()
        {
            throw new NotImplementedException();
        }

        public override void SaveData()
        {
            throw new NotImplementedException();
        }

        public DayRepositoryMock()
        {
            _dayList = new List<Day>
            {
                new Day
                {
                    Id = 0,
                    Date = DateTime.Today,
                    PurchaseList = new List<Purchase> {
            new Purchase { Id = 0, Item = "item1", Count = 2, Price = 100M },
             new Purchase { Id = 1, Item = "item2", Count = 3, Price = 22M }}
                }
            };
        }
    }

    class AppLogic
    {

        private readonly IDayRepository _repo;

        public IDayRepository Repository => _repo;

        public AppLogic(IDayRepository repo)
        {
            _repo = repo;
        }

        public decimal CountForDay(DateTime date)
        {
            var day = _repo.GetDay(date);
            if (day == null) return 0;
            return day.PurchaseList.Sum(w => w.Price);
        }

        public decimal CountForRange(DateTime date1, DateTime date2)
        {
            if (date1 < date2) return 0;

            decimal sum = 0;

            for (DateTime f = date1; f <= date2; f = f.AddDays(1))
            {
                var count = CountForDay(f);
                sum += count;
            }
            return sum;

        }

    }


}