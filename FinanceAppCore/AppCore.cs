using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceAppCore
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

        void AddDay(Day day);

        void AddPurchase(int id, Purchase pur);
        void AddPurchase(DateTime date, Purchase pur);

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

        public void AddDay(Day day)
        {
            if (day == null) return;

            int id = 0;
            if (Days.Count != 0) 
                id = Days.Max(w => w.Id) + 1;

            day.Id = id;
            Days.Add(day);
           
        }

        public void AddPurchase(int id, Purchase pur)
        {
            if (pur == null) return;

            var q = Days.FirstOrDefault(w => w.Id == id);
            if (q == null) return;


            pur.Id = 0;

            if(q.PurchaseList.Count!=0) pur.Id = q.PurchaseList.Count + 1;

            q.PurchaseList.Add(pur);
        }

        public void AddPurchase(DateTime date, Purchase pur)
        {
            if (pur == null) return;
            var q = Days.FirstOrDefault(w => w.Date == date);
            if (q == null) return;


            pur.Id = 0;

            if (q.PurchaseList.Count != 0) pur.Id = q.PurchaseList.Count + 1;

            q.PurchaseList.Add(pur);
        }

        public ICollection<Purchase> GetPurchaseList(DateTime date) => GetDay(date)?.PurchaseList;
    }


    public class JsonRepository : DayRepository
    {
        public override ICollection<Day> Days => _dayList;

        private IList<Day> _dayList;
        private string _fileName;
        private IJsonRepositoryConfig config;

        public JsonRepository(IJsonRepositoryConfig config)
        {

            this.config = config;
            _dayList = new List<Day>();
            _fileName = config.FileName;
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
            if (info == "")
            {
                SaveData();
                return;
            }

            var obj = JsonConvert.DeserializeObject<JArray>(info);
            _dayList = obj.ToObject<List<Day>>();

        }

        public override void SaveData()
        {
            File.WriteAllText(_fileName, JsonConvert.SerializeObject(_dayList, Formatting.Indented));
        }
    }

    public interface IJsonRepositoryConfig
    {
        string FileName { get; set; }
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
                },
                new Day
                {
                    Id = 0,
                    Date = DateTime.Today.AddDays(1),
                    PurchaseList = new List<Purchase> {
                        new Purchase { Id = 0, Item = "eqwd", Count = 41, Price = 100M },
                        new Purchase { Id = 1, Item = "iqvvvv", Count = 3, Price = 22M }}
                }
            };
        }
    }

    /// <summary>
    /// Логика работы приложения
    /// </summary>
    public class AppLogic
    {

        private readonly IDayRepository _repo;

        /// <summary>
        /// Репозиторий с данными
        /// </summary>
        public IDayRepository Repository => _repo;

        public AppLogic(IDayRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Просчитать остаток за день
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal CountForDay(DateTime date)
        {
            var day = _repo.GetDay(date);
            if (day == null) return 0;
            return day.PurchaseList.Sum(w => w.Price);
        }


        /// <summary>
        /// Просчитать остаток за диапазон дней
        /// </summary>
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
