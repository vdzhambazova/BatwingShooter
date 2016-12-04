using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using BatwingShooter.Data;

namespace BatwingShooter.Windows
{
    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        public HighScoreWindow()
        {
            InitializeComponent();
            var scores = XmlHighscoreStorage.Instance.Highscores;
            foreach (var score in scores)
            {
                this.PanelScores.Children.Add(new UniformGrid
                {
                    Rows = 1,
                    Children = {
                        new TextBlock { Text = score.Nickname },
                        new TextBlock { Text = score.Score.ToString() }
                    }
                });
            }
        }

        public void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            new InitGameWindow().Show();
            this.Close();
        }

        public void OnWindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}