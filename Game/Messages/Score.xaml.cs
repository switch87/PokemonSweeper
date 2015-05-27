using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PokemonSweeper.Game.Messages
{
    /// <summary>
    ///     Interaction logic for Score.xaml
    /// </summary>
    public partial class Score : Window
    {
        public Score()
        {
            InitializeComponent();
        }

        public static void ShowScore(GameWindow sender, Field Field)
        {
            Field.Timer.Stop();
            var PokeList = new List<Pokemon.Pokemon>();
            var Winner = new Score();

            foreach (var square in Field.Squares.Where(s => s.Pokemon != null))
            {
                Winner.ListBoxPokemon.Items.Add(square.Pokemon);
                PokeList.Add(square.Pokemon);
            }
            var newScore = sender.Game.CalculateNewScore(Field.Timer, Field.NrOfClicks, PokeList);
            Winner.score.Text = "Goed zo! Je hebt alle Pokemon gevangen!! uw score is " + newScore;
            Winner.Owner = sender;
            Winner.ShowDialog();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            var OwnerWindow = ((GameWindow) Owner);
            OwnerWindow.Game.NewField(OwnerWindow);
            Close();
        }
    }
}