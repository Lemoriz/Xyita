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
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// Interaction logic for ForChangeRow.xaml
    /// </summary>
    public partial class ForChangeRow : Window
    {
        public ForChangeRow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string ForChangeNameItem
        {
            get { return forChangeNameItem.Text; }
        }

        public string ForChangePrise
        {
            get { return forChangePrise.Text; }
        }

    }
}
