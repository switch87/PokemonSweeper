using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSweeper.Game.Field
{


    public class MineField
    {
        private Random Random = new Random();

        private int columnValue, rowValue;

        public int Columns
        {
            get
            {
                return columnValue;
            }
            set
            {
                columnValue = value;
            }
        }

        public int Rows
        {
            get
            {
                return rowValue;
            }
            set
            {
                rowValue = value;
            }
        }

        private List<MineSquare> squaresValue;

        public List<MineSquare> Squares { get { return squaresValue; } set { squaresValue = value; } }

        private void PopulateField( int nrOfPokemon )
        {
            List<int> pokemonPlacers = new List<int>();


            int pokemonLocation;
            for ( int i = 0; i < nrOfPokemon; i++ )
            {
                do
                {
                    pokemonLocation = Random.Next( Rows * Columns );
                } while ( pokemonPlacers.Contains( pokemonLocation ) );
                pokemonPlacers.Add( pokemonLocation );
            }
            Squares = new List<MineSquare>();
            for ( int row = 0; row < Rows; row++ )
            {
                for ( int column = 0; column < Columns; column++ )
                {
                    Squares.Add( new MineSquare( this, Rows, Columns, row, column ) );
                    if ( pokemonPlacers.Contains( Squares.Count - 1 ) )
                    {
                        Squares[Squares.Count - 1].Pokemon = new Pokemon.Pokemon { Type = (Pokemon.PokemonList)Random.Next( 1, 386 ) };

                    }
                }
            }
        }

        public int ClearedSquares
        {
            get { return Squares.Where( Square => Square.Status == MineSquare.SquareStatus.Cleared ).Count(); }
        }

        public System.Diagnostics.Stopwatch Timer;

        private int nrOfClicksValue;

        public int NrOfClicks
        {
            get { return nrOfClicksValue; }
            set { nrOfClicksValue = value; }
        }


        public MineField( int rows, int columns, int nrOfPokemon )
        {
            Rows = rows;
            Columns = columns;
            this.PopulateField( nrOfPokemon );
            Timer = System.Diagnostics.Stopwatch.StartNew();
            NrOfClicks = 0;
        }


    }
}
