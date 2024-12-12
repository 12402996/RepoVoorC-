using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Printing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace C_mastermindSprint1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<string> colors = new List<string> { "Red", "Yellow", "Orange", "White", "Green", "Blue" };
        List<Brush> ellipseColor = new List<Brush> { Brushes.Red, Brushes.Yellow, Brushes.DarkOrange, Brushes.White, Brushes.Green, Brushes.Blue };
        private List<string> secretCode = new List<string>();
        int guessAttempts = 0;
        DateTime startedGuessTime;
        int score = 100;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text))
            {
                MessageBox.Show("Je moet je naam ingeven", "Geef je naam in", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void HighScore()
        {
            StringBuilder opslaanHighScore = new StringBuilder();
            string[] highScores = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", };
            //foreach (string s in highScores)
            //{
            //    opslaanHighScore.Append(s);
            //}
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var randomCode = new Random();
            for (int i = 0; i < 4; i++)
            {
                secretCode.Add(colors[randomCode.Next(colors.Count)]);
            }

            generatedCodeTextBox.Text = $"{secretCode}".ToString();
            this.Title = $"Poging: {guessAttempts}";
            StartCountDown();
            generatedCodeTextBox.Visibility = Visibility.Collapsed;
            StartGame();
        }

        private void ellipseOne_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse ellipse)
            {
                if(ellipse.Fill == null)
                {
                    ellipse.Fill = ellipseColor[0];
                }
                else
                {
                    // Get the current brush of the ellipse
                    Brush currentBrush = ellipse.Fill;

                    // Find the current brush in the list
                    int currentIndex = ellipseColor.IndexOf(currentBrush);

                    // Calculate the index of the next brush
                    int nextIndex = (currentIndex + 1) % ellipseColor.Count;

                    // Get the next brush
                    Brush nextBrush = ellipseColor[nextIndex];

                    // Set the ellipse's fill to the next brush
                    ellipse.Fill = nextBrush;
                }
            }
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Deel 1. Bereken tijdsverschil
            TimeSpan timeUsedToGuess = DateTime.Now - startedGuessTime;
            // Deel 2. controle, if time > 10 seconds
            if (timeUsedToGuess.TotalSeconds > 10)
            {
                StopCountDown();
                startedGuessTime = DateTime.Now;
            }
            timerTextBox.Text = timeUsedToGuess.TotalSeconds.ToString("N0");
        }
        /// <summary>
        /// Deze methode zorgt eerst en vooral ervoor dat de timer stopt en vervolgens de poging verhoogt. 
        /// Nadien wordt er een messagebox getoond.
        /// de titel van het venster wordt aangepast en de timer wordt opnieuw gestart.
        /// </summary>
        private void StopCountDown()
        {
            timer.Stop();
            guessAttempts++;
            MessageBox.Show("Je beurt is over, probeer opnieuw", "Je tijd is verstreken, To Slow.", MessageBoxButton.OK, MessageBoxImage.Warning);
            this.Title = $"Poging: {guessAttempts}";
            StartCountDown();
        }
        /// <summary>
        /// Start de interval van de timer en zorgt ervoor dat de timer elke 100 milliseconden telt.
        /// </summary>
        private void StartCountDown()
        {
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            startedGuessTime = DateTime.Now;
            timer.Start();
        }
        private void AmountOfAttemptsToDecide()
        {
            //if (guessAttempts >= {inputUserAttempts})
            //{
            //    timer.Stop();
            //    MessageBoxResult result = MessageBox.Show($"Je hebt geen pogingen meer over. De correcte code was {string.Join(", ", secretCode)}."
            //        , "Game Over", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    ResetGame();
               
            //}

        }
        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            guessAttempts++;
            this.Title = $"Poging: " + guessAttempts;
            UpdateTitle();

            string checkColor1 = ((SolidColorBrush)ellipseOne.Fill).Color.ToString();
            string checkColor2 = ((SolidColorBrush)ellipseTwo.Fill).Color.ToString();
            string checkColor3 = ((SolidColorBrush)ellipseThree.Fill).Color.ToString();
            string checkColor4 = ((SolidColorBrush)ellipseFour.Fill).Color.ToString();

            List<string> inputColor = new List<string> { checkColor1, checkColor2, checkColor3, checkColor4 };


            StartCountDown();

            StackPanel colorPanel = new StackPanel { Orientation = Orientation.Horizontal};
            for (int i = 0; i < inputColor.Count; i++)
            {
                Ellipse rect = new Ellipse
                {
                    Width = 190,
                    Height = 40,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(inputColor[i]))
                };

                if (secretCode[i] == inputColor[i])
                {
                    rect.Stroke = Brushes.DarkRed;
                    rect.StrokeThickness = 5;
                }
                else if (secretCode.Contains(inputColor[i]))
                {
                    rect.Stroke = Brushes.Wheat;
                    rect.StrokeThickness = 5;
                }
                else
                {
                    rect.Stroke = Brushes.Transparent;
                    rect.StrokeThickness = 5;
                }

                colorPanel.Children.Add(rect);
            }
            colorHistoryListBox.Items.Add(new ListBoxItem { Content = colorPanel });

            int penaltyPoints = CalculatePenaltyPoints(inputColor); // Berekening van strafpunten
            score -= penaltyPoints; // Aftrekken van strafpunten van de score
            labelScore.Content = $"Score: {score}"; // Bijwerken van de score
            
            if (inputColor.SequenceEqual(secretCode))
            {
                timer.Stop();
                MessageBoxResult result = MessageBox.Show($"Proficiat, je hebt de code gekraakt in {guessAttempts} pogingen.",
                    "Gewonnen", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
            }
            
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
            }            
        }
        private void ResetGame()
        {
            secretCode.Clear();
            var randomCode = new Random();
            for (int i = 0; i < 4; i++)
            {
                secretCode.Add(colors[randomCode.Next(colors.Count)]);
            }

            guessAttempts = 0;
            score = 100;
            labelScore.Content = $"Score: {score}";
            colorHistoryListBox.Items.Clear();
            this.Title = $"Poging: {guessAttempts}";
            StartCountDown();
        }
        private int CalculatePenaltyPoints(List<string> userGuess)
        {
            int penaltyPoints = 0;

            for (int i = 0; i < userGuess.Count; i++)
            {
                if (userGuess[i] == secretCode[i])
                {
                    // Correct color and position
                    penaltyPoints += 0;
                }
                else if (secretCode.Contains(userGuess[i]))
                {
                    // Correct color but wrong position
                    penaltyPoints += 1;
                }
                else
                {
                    // Color not in the code
                    penaltyPoints += 2;
                }
            }

            return penaltyPoints;
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
        /// <summary>
        /// Hier kijkt de methode of de textbox zichtbaar is of niet.
        /// Vervolgens kan ik de textbox zichtbaar maken of verbergen met de toetsencombinatie CTRL + F12.
        /// </summary>
        private void ToggleDebug()
        {
            if (generatedCodeTextBox.Visibility == Visibility.Visible)
            {
                generatedCodeTextBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (secretCode is List<string> secretCodeList)
                {
                    generatedCodeTextBox.Text = string.Join(", ", secretCodeList);
                    generatedCodeTextBox.Visibility = Visibility.Visible;
                }
            }
        }

        private void Menu_Nieuw_Spel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_HighScores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Afsluiten_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Aantal_Pogingen_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}


     
    