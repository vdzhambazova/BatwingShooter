using System.Windows;
using System.Windows.Input;
using BatwingShooter.Data;

namespace BatwingShooter.Windows
{
    /// <summary>
    /// Interaction logic for GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        public GameOverWindow()
        {
            InitializeComponent();
        }

        public GameOverWindow(int highscore) : this()
        {
            this.Highscore = highscore;
            this.TextBlockHighScore.Text = string.Format("Your highscore is {0}", this.Highscore);
        }


        public void OnWindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public int Highscore { get; set; }

        private void OnSaveHighscoreButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.TextBoxNickname.Text))
            {
                MessageBox.Show("You should provide a Nickname");
                return;
            }
            XmlHighscoreStorage.Instance.Add(new PlayerHighscore(this.TextBoxNickname.Text, this.Highscore));
            new InitGameWindow().Show();
            this.Close();
        }

        public void OnDontSaveButtonClick(object sender, RoutedEventArgs e)
        {
            new InitGameWindow().Show();
            this.Close();
        }
    }
}