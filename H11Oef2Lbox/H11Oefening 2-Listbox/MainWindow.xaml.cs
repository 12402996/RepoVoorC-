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

namespace H11Oefening_2_Listbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            string[] _variabeleNog = { }; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            simpleListBox.Items.Clear();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //ListBox listBoxItem = new ListBox();
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = txtAdd.Text;
            simpleListBox.Items.Add(listBoxItem);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            simpleListBox.Items.Remove(simpleListBox.SelectedItem); 
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            string[] inputVanGebruiker = new string[simpleListBox.Items.Count];

            for (int i = 0; i < simpleListBox.Items.Count; i++)
            {
                ListBoxItem listItem = (ListBoxItem)simpleListBox.Items[i];
                inputVanGebruiker[i] = listItem.Content.ToString();
            }

            Array.Sort(inputVanGebruiker);
            simpleListBox.Items.Clear();

            foreach(string input in inputVanGebruiker)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = input;
                simpleListBox.Items.Add(newItem);
            }
        }

    }
}