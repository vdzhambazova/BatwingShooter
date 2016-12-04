using System.Windows;
using System.Windows.Input;

namespace BatwingShooter.Windows
{
    /// <summary>
    /// Interaction logic for InitGameWindow.xaml
    /// </summary>
    public partial class InitGameWindow : Window
    {
        public InitGameWindow()
        {
            InitializeComponent();
        }

        public void OnNewGameButtonClick(object sender, RoutedEventArgs e)
        {
            new GameWindow().Show();
            Close();
        }

        public void OnShowHighScoresButtonClick(object sender, RoutedEventArgs e)
        {
            new HighScoreWindow().Show();
            Close();
        }

        public void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnWindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}