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
using System.Windows.Shapes;

namespace PokemonSweeper.Game.Field.Game.Messages
{
    /// <summary>
    /// Interaction logic for Score.xaml
    /// </summary>
    public partial class Score : Window
    {

        public Score()
        {
            InitializeComponent();
        }

        public static void ShowScore( GameWindow sender, Field Field )
        {
            Field.Timer.Stop();
            List<Pokemon.Pokemon> PokeList = new List<Pokemon.Pokemon>();
            Game.Messages.Score Winner = new Game.Messages.Score();

            foreach ( Square square in Field.Squares.Where( s => s.Pokemon != null ) )
            {
                Winner.ListBoxPokemon.Items.Add( square.Pokemon );
                PokeList.Add( square.Pokemon );
            }
            int newScore = sender.Game.CalculateNewScore( Field.Timer, Field.NrOfClicks, PokeList );
            Winner.score.Text = "Goed zo! Je hebt alle Pokemon gevangen!! uw score is " + newScore;
            Winner.Owner = sender;
            Winner.ShowDialog();
        }

        private void Next_Click( object sender, RoutedEventArgs e )
        {
            GameWindow OwnerWindow = ( (GameWindow)Owner );
            OwnerWindow.Game.NewField( OwnerWindow );
            this.Close();

        }
    }
}
