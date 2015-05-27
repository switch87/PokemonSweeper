using System.Windows;
using System.Windows.Input;
using PokemonSweeper.Game;

namespace PokemonSweeper
{
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            Game = new PokeSweepGame();
        }

        public PokeSweepGame Game { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Game.NewField(this);
        }

        public void MineSquare_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Square) sender).RightButton(this);
        }

        public void MineSquare_Click(object sender, RoutedEventArgs e)
        {
            ((Square) sender).LeftButton(this);
        }
    }
}