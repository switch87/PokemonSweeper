using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PokemonSweeper.Game.Field.Pokemon
{
    public class Pokemon
    {
        private PokemonList typeValue;

        public PokemonList Type
        {
            get { return typeValue; }
            set { typeValue = value; }
        }

        public int Number
        {
            get { return (int)typeValue; }
        }

        public string Name
        {
            get { return typeValue.ToString(); }
        }



        public Image Picture
        {
            get {
                string number = Number+"";
                if (Number / 100 < 1)
                {
                    if (Number / 10 < 1)
                    {
                        number = "0" + number;
                    }
                    number = "0" + number;
                }
                //return "Game/images/pokemon/" + number + ".png";
                return new Image { Source = new BitmapImage(new Uri(@"Game/images/pokemon/" + number + ".png", UriKind.Relative)) };
            }
        }
        
        
    }
}
