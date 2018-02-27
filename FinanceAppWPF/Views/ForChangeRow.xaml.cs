using System.Windows;

namespace Test
{
    /// <summary>
    ///     Interaction logic for ForChangeRow.xaml
    /// </summary>
    public partial class ForChangeRow : Window
    {
        public ForChangeRow()
        {
            InitializeComponent();
        }

        public string ForChangeNameItem => forChangeNameItem.Text;

        public string ForChangePrise => forChangePrise.Text;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}