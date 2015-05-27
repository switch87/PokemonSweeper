namespace PokemonSweeper
{
    public class FieldLevel
    {
        public int Dimention // Dimention = Rows * Columns
        {
            get { return Rows*Columns; }
        }

        public int Rows // Rows - nr of rows
        { get; set; }

        public int Columns // Columns - nr of columns
        { get; set; }

        public int Pokemon // Pokemon - nr of pokemon in the field
        { get; set; }

        public int? Rock // Rock - will be used in later levels to represent mountains
        { get; set; }

        public int NextLevel // NextLevel - Points needed to gain to the next level
        { get; set; }

        public int Open { get; set; }
    }
}