using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MineSweeper1
{
    public class MineSquare :  Button
    {
        public enum SquareStatus
        {
            Cleared,
            Mine,
            Open,
            Flagged,
            Question,
        }

        private SquareStatus statusValue;

        public SquareStatus Status
        {
            get { return statusValue; }
            set { statusValue = value; }
        }
        

        private int rowValue;

        public int Row
        {
            get { return rowValue; }
            set { rowValue = value; }
        }

        private int columnValue;

        public int Column
        {
            get { return columnValue; }
            set { columnValue = value; }
        }

        private int nrOfRowsValue;

	    public int NrOfRows	
        {
		get { return nrOfRowsValue;}
		set { nrOfRowsValue = value;}
	    }

        private int nrOfColumnsValue;

        public int NrOfColumns
        {
            get { return nrOfRowsValue; }
            set { nrOfRowsValue = value; }
        }

        private MineField fieldValue;

        public MineField Field
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        private bool mineValue;

        public bool Mine
        {
            get { return mineValue; }
            set { mineValue = value; }
        }
        

        public int Mines
        {
            get
            {
                int mines = 0;
                foreach (MineSquare Square in (Field.Squares.Where
                    (s => (s.Row >= this.Row-1) && (s.Row <= this.Row+1) && 
                    (s.Column >= this.Column-1) && (s.Column <= this.Column+1))
                    .ToList()))
                {
                    if (Square.Mine == true) mines++;
                }
                return mines;
            }
        }
        
        public MineSquare(MineField field, int rows, int columns, int row, int column)
        {
            Field = field;
            NrOfRows = rows;
            NrOfColumns = columns;
            Row = row;
            Column = column;
            Mine = false;
            Status = SquareStatus.Open;
        }
	
    }
}
