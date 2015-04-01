﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MineSweeper1.Pokemon
{
    class Pokemon
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
                string number = Number.ToString();
                if (Number / 100 < 1)
                {
                    if (Number / 10 < 1)
                    {
                        number = "0" + number;
                    }
                    number = "0" + number;
                }
                number = @"images/pokemon/" + number + ".png";
                return new Image { Source = new BitmapImage(new Uri(@number, UriKind.Relative)) };
            }
        }
        
        
    }
}