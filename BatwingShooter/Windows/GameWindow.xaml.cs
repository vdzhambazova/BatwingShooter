using System.Windows;
using BatwingShooter.Engines;
using BatwingShooter.Renderer;

namespace BatwingShooter.Windows
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            this.InitializeComponent();
            var wpfRenderer = new WpfRenderer(this.GameCanvas);
            this.Engine = new GameEngine(wpfRenderer);
            this.Engine.InitGame();
            this.Engine.StartGame();
        }

        public IGameEngine Engine { get; set; }
    }
}
