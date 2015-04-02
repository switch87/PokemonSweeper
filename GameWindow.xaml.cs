using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokemonSweeper.Game.Field
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game.PokeSweepGame gameValue;

        public Game.PokeSweepGame Game
        {
            get { return gameValue; }
            set { gameValue = value; }
        }

        public GameWindow()
        {
            InitializeComponent();
            Game = new Game.PokeSweepGame();
        }

        private void Window_Loaded( object sender, RoutedEventArgs e )
        {
            Game.NewField( this );
        }



        public void MineSquare_MouseRightButtonDown( object sender, MouseButtonEventArgs e )
        {
            ( (MineSquare)sender ).RightButton( this );
        }

        public void MineSquare_Click( object sender, RoutedEventArgs e )
        {
            ( (MineSquare)sender ).LeftButton( this );

        }
    }
}
