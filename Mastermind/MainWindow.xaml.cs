using System.Printing;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.WebRequestMethods;

namespace C_mastermindSprint1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<string> colors = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        private List<string> secretCode = new List<string>();
        int guessAttempts = 0;
        DateTime startedGuessTime;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartTimer();
            var randomCode = new Random();

            for (int i = 0; i < 4; i++)
            {
                secretCode.Add(colors[randomCode.Next(colors.Count)]);
            }

            generatedCodeTextBox.Text = $"{secretCode}".ToString();
            this.Title = $"Poging: {guessAttempts}";
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Deel 1. Bereken tijdsverschil
            TimeSpan timeUsedToGuess = DateTime.Now - startedGuessTime;
            // Deel 2. controle, if time > 10 seconds
            if (timeUsedToGuess.TotalSeconds > 10)
            {
                StopCountDown();
                startedGuessTime = DateTime.Now.AddSeconds(-1);
            }
            timerTextBox.Text = timeUsedToGuess.TotalSeconds.ToString("N0");
        }

        private void StopCountDown()
        {
            timer.Stop();
            guessAttempts++;
            MessageBox.Show("Je beurt is over, probeer opnieuw", "Je tijd is verstreken, To Slow.", MessageBoxButton.OK, MessageBoxImage.Warning);
            this.Title = $"Poging: {guessAttempts}";
            StartTimer();
        }

        private void StartTimer()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            startedGuessTime = DateTime.Now;
            timer.Start();
        }


        private void ComboBoxColour_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            if (sender == comboBoxColour1 && comboBoxColour1.SelectedItem != null)
            {
                switch (comboBoxColour1.SelectedIndex)
                {
                    case 0:
                        labelColorOne.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1:
                        labelColorOne.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2:
                        labelColorOne.Background = new SolidColorBrush(Colors.Orange);
                        break;
                    case 3:
                        labelColorOne.Background = new SolidColorBrush(Colors.White);
                        break;
                    case 4:
                        labelColorOne.Background = new SolidColorBrush(Colors.Green);
                        break;
                    case 5:
                        labelColorOne.Background = new SolidColorBrush(Colors.Blue);
                        break;
                }
            }
            if (sender == comboBoxColour2 && comboBoxColour2.SelectedItem != null)
            {
                switch (comboBoxColour2.SelectedIndex)
                {
                    case 0:
                        labelColorTwo.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1:
                        labelColorTwo.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2:
                        labelColorTwo.Background = new SolidColorBrush(Colors.Orange);
                        break;
                    case 3:
                        labelColorTwo.Background = new SolidColorBrush(Colors.White);
                        break;
                    case 4:
                        labelColorTwo.Background = new SolidColorBrush(Colors.Green);
                        break;
                    case 5:
                        labelColorTwo.Background = new SolidColorBrush(Colors.Blue);
                        break;
                }
            }
            if (sender == comboBoxColour3 && comboBoxColour3.SelectedItem != null)
            {
                switch (comboBoxColour3.SelectedIndex)
                {
                    case 0:
                        labelColorThree.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1:
                        labelColorThree.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2:
                        labelColorThree.Background = new SolidColorBrush(Colors.Orange);
                        break;
                    case 3:
                        labelColorThree.Background = new SolidColorBrush(Colors.White);
                        break;
                    case 4:
                        labelColorThree.Background = new SolidColorBrush(Colors.Green);
                        break;
                    case 5:
                        labelColorThree.Background = new SolidColorBrush(Colors.Blue);
                        break;

                }
            }
            if (sender == comboBoxColour4 && comboBoxColour4.SelectedItem != null)
            {
                switch (comboBoxColour4.SelectedIndex)
                {
                    case 0:
                        labelColorFour.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case 1:
                        labelColorFour.Background = new SolidColorBrush(Colors.Yellow);
                        break;
                    case 2:
                        labelColorFour.Background = new SolidColorBrush(Colors.Orange);
                        break;
                    case 3:
                        labelColorFour.Background = new SolidColorBrush(Colors.White);
                        break;
                    case 4:
                        labelColorFour.Background = new SolidColorBrush(Colors.Green);
                        break;
                    case 5:
                        labelColorFour.Background = new SolidColorBrush(Colors.Blue);
                        break;
                }

            }
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            {
                guessAttempts++;
                this.Title = $"Poging: " + guessAttempts;
                UpdateTitle();
                StartTimer();
            }
            string checkColor1 = comboBoxColour1.Text;
            string checkColor2 = comboBoxColour2.Text;
            string checkColor3 = comboBoxColour3.Text;
            string checkColor4 = comboBoxColour4.Text;
            List<string> inputColor = new List<string> { checkColor1, checkColor2, checkColor3, checkColor4 };

            for (int i = 0; i < 4; i++)
            {
                Label checkColors = null;

                switch (i)
                {
                    case 0:
                        checkColors = labelColorOne;
                        break;
                    case 1:
                        checkColors = labelColorTwo;
                        break;
                    case 2:
                        checkColors = labelColorThree;
                        break;
                    case 3:
                        checkColors = labelColorFour;
                        break;
                }

                if (secretCode[i] == inputColor[i])
                {
                    checkColors.BorderBrush = Brushes.DarkRed;
                    checkColors.BorderThickness = new Thickness(7);
                }
                else if (secretCode.Contains(inputColor[i]))
                {
                    checkColors.BorderBrush = Brushes.Wheat;
                    checkColors.BorderThickness = new Thickness(7);
                }
                else
                {
                    checkColors.BorderBrush = Brushes.Transparent;
                    checkColors.BorderThickness = new Thickness(7);
                }
            }

        }
        private void UpdateTitle()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(guessAttempts);
            this.Title = sb.ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.F12)
            {
                ToggleDebug();
            }
        }
        private void ToggleDebug()
        {
            if (generatedCodeTextBox.Visibility == Visibility.Visible)
            {
                generatedCodeTextBox.Visibility = Visibility.Collapsed;
            }
            else if (secretCode is List<string> secretCodeList)
            {
                generatedCodeTextBox.Text = string.Join(", ", secretCodeList);
                generatedCodeTextBox.Visibility = Visibility.Visible;
            }
        }
    }
}


     
    