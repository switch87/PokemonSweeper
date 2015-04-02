using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSweeper.Game.Field.Game.Field
{
    /* FieldLevel
     * defenition of the levels.
     */

    public class FieldLevel
    {
        public int Dimention                // Dimention = Rows * Columns
        {
            get { return Rows * Columns; }
        }

        private int rowsValue;

        public int Rows                     // Rows - nr of rows
        {
            get { return rowsValue; }
            set { rowsValue = value; }
        }

        private int columnsValue;

        public int Columns                  // Columns - nr of columns
        {
            get { return columnsValue; }
            set { columnsValue = value; }
        }

        private int pokemonValue;

        public int Pokemon                  // Pokemon - nr of pokemon in the field
        {
            get { return pokemonValue; }
            set { pokemonValue = value; }
        }

        private int? rockValue;

        public int? Rock                    // Rock - will be used in later levels to represent mountains
        {
            get { return rockValue; }
            set { rockValue = value; }
        }

        private int nextLevelValue;

        public int NextLevel                // NextLevel - Points needed to gain to the next level
        {
            get { return nextLevelValue; }
            set { nextLevelValue = value; }
        }




    }
}
