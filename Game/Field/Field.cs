using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PokemonSweeper.Game.Pokemon;

namespace PokemonSweeper
{
    public class Field
    {
        private readonly Random Random = new Random();
        public Stopwatch Timer;

        public Field(int rows, int columns, int nrOfPokemon, int openSquares, GameWindow window)
        {
            Rows = rows;
            Columns = columns;
            PopulateField(nrOfPokemon, openSquares, window);
            Timer = Stopwatch.StartNew();
            NrOfClicks = 0;
        }

        public int Columns { get; set; }
        public int Rows { get; set; }
        public List<Square> Squares { get; set; }

        public int ClearedSquares
        {
            get { return Squares.Where(Square => Square.Status == Square.SquareStatus.Cleared).Count(); }
        }

        public int NrOfClicks { get; set; }

        private void PopulateField(int nrOfPokemon, int openSquares, GameWindow window)
        {
            var pokemonPlacers = new List<int>();


            int pokemonLocation;
            for (var i = 0; i < nrOfPokemon; i++)
            {
                do
                {
                    pokemonLocation = Random.Next(Rows*Columns);
                } while (pokemonPlacers.Contains(pokemonLocation));
                pokemonPlacers.Add(pokemonLocation);
            }
            Squares = new List<Square>();
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    Squares.Add(new Square(this, Rows, Columns, row, column));
                    if (pokemonPlacers.Contains(Squares.Count - 1))
                    {
                        Squares[Squares.Count - 1].Pokemon = new Pokemon
                        {
                            Type = (PokemonList) Random.Next(1, 386)
                        };
                    }
                }
            }
            for (var i = 0; i < openSquares; i++)
            {
                int openLocation;
                do
                {
                    openLocation = Random.Next(Rows*Columns);
                } while (pokemonPlacers.Contains(openLocation) ||
                         Squares[openLocation].Status == Square.SquareStatus.Cleared);
                Squares[openLocation].Status = Square.SquareStatus.Cleared;
                Squares[openLocation].SwipeSquare(window);
            }
        }
    }
}