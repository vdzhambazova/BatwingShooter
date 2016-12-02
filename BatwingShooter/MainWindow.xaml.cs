using System.Windows;
using BatwingShooter.Engines;
using BatwingShooter.Renderer;

namespace BatwingShooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WpfRenderer renderer = new WpfRenderer(this.gameCanvas);
            this.Engine = new GameEngine(renderer);
            this.Engine.InitGame();
            this.Engine.StartGame();
        }

        public GameEngine Engine { get; set; }
    }
}
