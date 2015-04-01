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

        public static void ShowScore(GameWindow sender, MineField Field)
        {
            Game.Messages.Score Winner = new Game.Messages.Score();
            foreach (MineSquare square in Field.Squares.Where(s => s.Pokemon != null))
                Winner.ListBoxPokemon.Items.Add(square.Pokemon);
            Winner.Owner = sender;
            Winner.ShowDialog();
            //sender.Close();
            //MessageBox.Show("Gewonnen");
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            GameWindow OwnerWindow = ((GameWindow)Owner);
            OwnerWindow.NewGame(256);
            this.Close();

        }
    }
}
