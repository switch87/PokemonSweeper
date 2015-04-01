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

        public static void ShowScore(MainWindow sender, MineField Field)
        {
            Game.Messages.Score End = new Game.Messages.Score();
            foreach (MineSquare square in Field.Squares.Where(s => s.Pokemon != null))
                End.ListBoxPokemon.Items.Add(square.Pokemon);
            End.Owner = sender;
            End.ShowDialog();
            //MessageBox.Show("Gewonnen");
            sender.NewGame();
        }
    }
}
