using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSweeper.Game.Field.Game
{
    public class PokeSweepGame
    {
        private List<Field.FieldLevel> fieldLevelsValue;

        public List<Field.FieldLevel> FieldLevels
        {
            get { return fieldLevelsValue; }
            set { fieldLevelsValue = value; }
        }

        private int scoreValue;

        public int Score
        {
            get { return scoreValue; }
            set { scoreValue = value; }
        }

        private List<Pokemon.Pokemon> pokemonValue;

        public List<Pokemon.Pokemon> Pokemon
        {
            get { return pokemonValue; }
            set { pokemonValue = value; }
        }

        private int levelValue;

        public int Level
        {
            get { return levelValue; }
            set { levelValue = value; }
        }

        private Field fieldValue;

        public Field Field
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }



        // Calculate the score gained after finishing a field.
        public int CalculateNewScore( System.Diagnostics.Stopwatch timer, int clicks, List<Pokemon.Pokemon> pokemon )
        {
            // count the number of new (never before) catched pokemon.
            int newPokemon = 0;
            foreach ( Pokemon.Pokemon monster in pokemon.Where( m => !Pokemon.Contains( m ) ) ) newPokemon++;

            // Calculate the score and add it to the old score
            int newScore = (int)( ( newPokemon * 100 + ( 100 - clicks ) / ( timer.Elapsed.TotalSeconds / 2 ) ) );
            this.Score += newScore;
            // Return the field-score
            return newScore;
        }

        public PokeSweepGame()
        {
            Level = 0;
            Score = 0;
            Pokemon = new List<Pokemon.Pokemon>();      // make empty list of Pokemon captured

            FieldLevels = new List<Field.FieldLevel>();                                                                     // Make list of Game Levels
                                                                                                                            // ------------------------
            FieldLevels.Add( new Field.FieldLevel { Rows = 9, Columns = 9, Pokemon = 10, NextLevel = 1000 } );              // Beginner standard minesweeper
            FieldLevels.Add( new Field.FieldLevel { Rows = 16, Columns = 16, Pokemon = 40, NextLevel = 10000 } );           // Intermediate standard minesweeper
            FieldLevels.Add( new Field.FieldLevel { Rows = 16, Columns = 32, Pokemon = 99, NextLevel = 20000 } );           // Expert standard minesweeper
            FieldLevels.Add( new Field.FieldLevel { Rows = 16, Columns = 16, Pokemon = 70, NextLevel = 99999999, Open=50} ); 
        }

        public void NewField( GameWindow window )
        {
            window.MineFieldGrid.Children.Clear();

            for ( int i = Level; Score >= FieldLevels[i].NextLevel && i <= FieldLevels.Count(); i++ ) Level++;
            window.MineFieldGrid.Rows = FieldLevels[Level].Rows;
            window.MineFieldGrid.Columns = FieldLevels[Level].Columns;
            window.Width = 600 * FieldLevels[Level].Columns / FieldLevels[Level].Rows;
            window.MineFieldGrid.Width = 500 * FieldLevels[Level].Columns / FieldLevels[Level].Rows;
            Field = new Field( FieldLevels[Level].Rows, FieldLevels[Level].Columns, FieldLevels[Level].Pokemon, FieldLevels[Level].Open, window);

            foreach ( Square square in Field.Squares )
            {
                square.Click += window.MineSquare_Click;
                square.MouseRightButtonDown += window.MineSquare_MouseRightButtonDown;
                window.MineFieldGrid.Children.Add( square );
            }
        }

    }
}
