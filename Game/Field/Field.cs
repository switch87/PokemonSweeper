using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSweeper.Game.Field
{
    public class Field
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

        private List<Square> squaresValue;

        public List<Square> Squares { get { return squaresValue; } set { squaresValue = value; } }

        private void PopulateField( int nrOfPokemon, int openSquares, GameWindow window )
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
            Squares = new List<Square>();
            for ( int row = 0; row < Rows; row++ )
            {
                for ( int column = 0; column < Columns; column++ )
                {
                    Squares.Add( new Square( this, Rows, Columns, row, column ) );
                    if ( pokemonPlacers.Contains( Squares.Count - 1 ) )
                    {
                        Squares[Squares.Count - 1].Pokemon = new Pokemon.Pokemon { Type = (Pokemon.PokemonList)Random.Next( 1, 386 ) };

                    }
                }
            }
            for (int i = 0; i < openSquares; i++)
            {
                int openLocation;
                do
                {
                    openLocation = Random.Next( Rows * Columns );
                } while ( pokemonPlacers.Contains( openLocation ) || Squares[openLocation].Status == Square.SquareStatus.Cleared );
                Squares[openLocation].Status = Square.SquareStatus.Cleared;
                Squares[openLocation].SwipeSquare(window);
            }
        }

        public int ClearedSquares
        {
            get { return Squares.Where( Square => Square.Status == Square.SquareStatus.Cleared ).Count(); }
        }

        public System.Diagnostics.Stopwatch Timer;

        private int nrOfClicksValue;

        public int NrOfClicks
        {
            get { return nrOfClicksValue; }
            set { nrOfClicksValue = value; }
        }

        public Field( int rows, int columns, int nrOfPokemon, int openSquares, GameWindow window)
        {
            Rows = rows;
            Columns = columns;
            this.PopulateField( nrOfPokemon, openSquares, window );
            Timer = System.Diagnostics.Stopwatch.StartNew();
            NrOfClicks = 0;
        }

    }
}
