using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Variabelen2kassa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VATNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (VATNumberTextBox.Text.Length <= 10)
            {
                double VATNumber;
                bool isValid = double.TryParse(VATNumberTextBox.Text, out VATNumber);
                if (isValid)
                { }
                else {
            }

        }
    }
}