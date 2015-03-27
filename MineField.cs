using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper1
{

    public class MineField
    {
        private int[] ValidDimentions = {81 /*Beginner*/, 256 /*Intermediate*/, 480 /*Expert*/};

        private int dimentionValue;

        public int Dimention
        {
            get { return dimentionValue; }
            set 
            {
                if (ValidDimentions.Contains(value)) dimentionValue = value;
                else throw new IndexOutOfRangeException("Given demention is not possible.");
            }
        }

        public int Columns
        {
            get { return (int)Math.Sqrt(Dimention); }
        }

        public int Rows
        {
            get { return (int)Math.Sqrt(Dimention);  }
        }

        public int? Mines
        {
            get 
            {
                if (Dimention == ValidDimentions[0]) return 10;         // Beginner
                else if (Dimention == ValidDimentions[1]) return 40;    // Intermediate
                else if (Dimention == ValidDimentions[2]) return 99;    // Expert
                else return null; 
            }
        }
        

        private List<MineSquare> squaresValue;

        public List<MineSquare> Squares { get { return squaresValue; } set { squaresValue = value; } }

        private void PopulateField()
        {
            List<int> mines = new List<int>();

            Random MinePlacer = new Random();
            int Mine;
            for (int i = 0; i < Mines; i++)
            {
                do
                {
                    Mine = MinePlacer.Next(Dimention);
                } while (mines.Contains(Mine));
                mines.Add(Mine);
            }

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    Squares.Add(new MineSquare(this, Rows, Columns, row, column));
                    if (mines.Contains(Squares.Count - 1)) Squares[Squares.Count - 1].Mine = true;
                }
            }
        }

        public MineField(int dimention)
        {
            Dimention = dimention;
            this.PopulateField();
        }
    }
}
