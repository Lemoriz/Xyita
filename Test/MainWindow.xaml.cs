using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Test
{

    public struct Buy
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
    }


    public struct Day
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public List<Buy> Items { get; set; }
    }

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        List<SolidColorBrush> Colors = new List<SolidColorBrush> { Brushes.Linen, Brushes.LavenderBlush, Brushes.AliceBlue, Brushes.SeaShell, Brushes.PeachPuff };

        Random rand = new Random();
        int previousNumber = 0;


        void AddDay(Day day)
        {
            var w = new Grid();
            w.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            w.RowDefinitions.Add(new RowDefinition());
            w.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });

            var stack = new StackPanel();
            foreach(var element in day.Items)
            {
                var xyita = new Label { Content = $"{element.Id} {element.Item} {element.Price}" };
                
                xyita.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) => 
                {
                    ForChangeRow win2 = new ForChangeRow();
                    win2.forChangeNameItem.Text = element.Item;
                    win2.forChangePrise.Text = element.Price.ToString();
                    win2.ShowDialog();
                    xyita.Content = $"{element.Id} {win2.forChangeNameItem.Text} {win2.forChangePrise.Text}";
                };
                stack.Children.Add(xyita);
            }


            var label = new Label { Content = $" {day.date.DayOfWeek} {day.date.Day}" };

            var randomNumber = rand.Next(0, 3);

            while (randomNumber == previousNumber)
                randomNumber = rand.Next(0, 3);

            w.Background = Colors[randomNumber];
            label.Background = Colors[4];
            previousNumber = randomNumber;


            w.Children.Add(label);
            Grid.SetRow(label, 0);
            w.Children.Add(stack);
            Grid.SetRow(stack, 1);


            var button = new Button();
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.Width = 70;
            button.Height = 25;
            button.Content = "Add record";
            button.Click += (object sender, RoutedEventArgs e) =>
            {
                ForChangeRow win2 = new ForChangeRow();
                win2.ShowDialog();
                var w1 = new Grid();

                w1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                w1.RowDefinitions.Add(new RowDefinition());
                w1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                var stack1 = new StackPanel();

                Buy buy = new Buy();
                var xyita = new Label { Content = $"{buy.Id+1} {win2.forChangeNameItem.Text} {win2.forChangePrise.Text}" };
                stack.Children.Add(xyita);
            };


            w.Children.Add(button);
            Grid.SetRow(button, 2);


            mainStack.Children.Add(w);

            scroll.ScrollToEnd();

        }


        public MainWindow()
        {
            InitializeComponent();
            //var w = new Day
            //{
            //    Id = 0,
            //    date = DateTime.Today,
            //    Items = new List<Buy>
            //    {
            //        new Buy{ Id=0, Item="Говно", Price=123.33M},
            //        new Buy{ Id=1, Item="Моча", Price=55.99M},
            //        new Buy{ Id=2, Item="Хуита", Price=1.33M},
            //    }

            //};
            //var e = new Day
            //{
            //    Id = 0,
            //    date = DateTime.Today.AddDays(2),
            //    Items = new List<Buy>
            //    {
            //        new Buy{ Id=0, Item="Предмет 1", Price=123.33M},
            //        new Buy{ Id=1, Item="Предмет 2", Price=55.99M},
            //        new Buy{ Id=2, Item="Предмет 3", Price=1.33M},
            //    }

            //};

            //var r = new Day
            //{
            //    Id = 0,
            //    date = DateTime.Today.AddDays(2),
            //    Items = new List<Buy>
            //    {
            //        new Buy{ Id=0, Item="Хуита 1", Price=123.33M},
            //        new Buy{ Id=1, Item="Хуита 6", Price=55.99M},
            //        new Buy{ Id=2, Item="Хуита 13", Price=1.33M},
            //    }

            //};

            //var u = new Day
            //{
            //    Id = 0,
            //    date = DateTime.Today.AddDays(5),
            //    Items = new List<Buy>
            //    {
            //        new Buy{ Id=0, Item="Говно 1", Price=123.33M},
            //    }

            //};

            //AddDay(w);
            //AddDay(e);
            //AddDay(r);
            //AddDay(u);



            //List<Day> list = new List<Day>();
            //list.Add(w);
            //list.Add(e);
            //list.Add(r);
            //list.Add(u);


            var str = File.ReadAllText("huita.txt");
            var tt = JsonConvert.DeserializeObject<JArray>(str);
            List<Day> wer = tt.ToObject<List<Day>>();
        
             foreach(var ee in wer)
            {
                AddDay(ee);
            }


        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btn_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        //private void hui(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("hui");
        //    FFF.Content = "sad";
        //}
    }
}
