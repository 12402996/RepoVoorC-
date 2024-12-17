using System.Dynamic;
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

namespace VulkanenGids
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] kolommen = new string[6]
            {
                "Naam", "Land", "Lengtegraad", "Breedtegraad", "Hoogte", "Type"
            };
        private string[,] vulkanen = new string[8, 6]
            {
                { "Eyjafjallajokull",       "Iceland",          "63.633",       "-19.633",       "1651",     "Stratovolcano" },
                 { "Fujisan",               "Japan",            "35.361",       "138.728",       "3776",     "Stratovolcano" },
                { "Mauna Loa",              "United States",    "19.475",       "-155.608",      "4170",     "Shield volcano" },
                { "Etna",                   "Italy",            "37.748",       "14.999",        "3320",     "Stratovolcano" },
                { "Fogo",                   "Cape Verde",       "14.95",        "-24.35",        "2829",     "Stratovolcano" },
                { "Pacaya",                 "Guatemala",        "14.382",       "-90.601",       "2569",     "Complex volcano" },
                { "Vesuvius",               "Italy",            "40.821",       "14.426",        "1281",     "Complex volcano" },
                { "Villarrica",             "Chile",            "-39.42",       "-71.93",        "2847",      "Stratovolcano" }
            };
        StringBuilder sb = new StringBuilder();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddComboBoxItems();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < kolommen.Length; i++)
            {
                    sb.AppendLine($"{kolommen[i]}: {vulkanen[vulkanenComboBox.SelectedIndex, i]}");
                
            }
                resultTextBox.Text = sb.ToString();
        }

        private void AddComboBoxItems()
        {   
            for (int i = 0; i < vulkanen.GetLength(0); i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = vulkanen[i, 0];
                vulkanenComboBox.Items.Add(item);
            }
             
            
        }
    }
}