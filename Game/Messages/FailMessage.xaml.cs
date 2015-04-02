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
    /// Interaction logic for FailMessage.xaml
    /// </summary>
    public partial class FailMessage : Window
    {
        public FailMessage()
        {
            InitializeComponent();
        }

        public static void ShowMessage(GameWindow window, Pokemon.Pokemon pokemon )
        {
            Game.Messages.FailMessage Fail = new Game.Messages.FailMessage();
            Fail.EscapedPokemon.Source = pokemon.Picture;
            Fail.Message.Text = pokemon.Number + " - " + pokemon.Name + " managed to escape!";
            Fail.Title = "Game over!";
            Fail.Owner = window;
            Fail.ShowDialog();
            window.Game.NewField( window );
        }

        private void retry_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

    }
}
