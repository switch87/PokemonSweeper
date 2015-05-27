using System;
using System.Windows.Media.Imaging;

namespace PokemonSweeper.Game.Pokemon
{
    public class Pokemon
    {
        public PokemonList Type { get; set; }

        public int Number
        {
            get { return (int) Type; }
        }

        public string Name
        {
            get { return Type.ToString(); }
        }

        public BitmapImage Picture
        {
            get
            {
                var number = Number + "";
                if (Number/100 < 1)
                {
                    if (Number/10 < 1)
                    {
                        number = "0" + number;
                    }
                    number = "0" + number;
                }
                return new BitmapImage(new Uri(@"/Game/images/pokemon/" + number + ".png", UriKind.Relative));
            }
        }
    }
}