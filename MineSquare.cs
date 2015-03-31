using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public void RightButton(MainWindow sender)
        {
            
            if (Status == SquareStatus.Open)
            {
                Status = SquareStatus.Flagged;
                Content = new Image { Source = new BitmapImage(new Uri(@"images/pokeball.png", UriKind.Relative)) };
                //Content = "!";
                //Foreground = Brushes.Red;
                //FontWeight = FontWeights.Bold;
                List<MineSquare> FlaggedSqaures = Field.Squares.Where(square => square.Status == MineSquare.SquareStatus.Flagged).ToList();
                if (FlaggedSqaures.Count() == Field.Mines)
                {
                    bool win = true;
                    foreach (MineSquare flaggedSquare in FlaggedSqaures)
                    {
                        if (flaggedSquare.Mine == false)
                        {
                            win = false;
                        }
                    }
                    if (win)
                    {
                        MessageBox.Show("Gewonnen");
                        sender.NewGame();
                    }

                }
            }
            else if (Status == SquareStatus.Flagged)
            {
                Status = SquareStatus.Question;
                //knopje.Content = new Image { Source = new BitmapImage(new Uri(@"images/question.png", UriKind.Relative)) };
                Content = "?";
                Foreground = Brushes.Blue;
                FontWeight = FontWeights.Bold;
            }
            else
            {
                Status = SquareStatus.Open;
                Content = "";
                Foreground = Brushes.Gray;
                FontWeight = FontWeights.Normal;
            }
        }

        public void LeftButton(MainWindow sender)
        {
            
            if (Status == SquareStatus.Open)
            {
                Unmine(sender);
                if (Field.ClearedSquares + Field.Mines == Field.Dimention)
                {
                    MessageBox.Show("Gewonnen");
                    sender.NewGame();
                }
            }
        }

        public void Unmine(MainWindow sender)
        {
            if (Mine)
            {

                /* selecteer een Pokemon
                 * Deze code moet nog aangepast met klasse pokemon voor verdere uitbreiding mogelijk te maken
                 */
                Random Pokeselect = new Random();

                int PokeNumberInt = Pokeselect.Next(385) + 1;
                string PokeNumber = PokeNumberInt.ToString();
                if (PokeNumberInt / 100 < 1)
                {
                    if (PokeNumberInt / 10 < 1)
                    {
                        PokeNumber = "0" + PokeNumber;
                    }
                    PokeNumber = "0" + PokeNumber;
                }
                PokeNumber = @"images/pokemon/" + PokeNumber + ".png";
                Content = new Image { Source = new BitmapImage(new Uri(@PokeNumber, UriKind.Relative)) };
                // Einde Pokemoncode


                Status = MineSquare.SquareStatus.Mine;
                Background = Brushes.Red;
                BorderBrush = Brushes.Red;
                IsEnabled = false;

                MessageBox.Show("One Escaped!!");
                sender.NewGame();

            }
            else if (Mines > 0)
            {
                Content = Mines;
                Status = MineSquare.SquareStatus.Cleared;
                Background = Brushes.White;
                BorderBrush = Brushes.White;
                IsEnabled = false;
            }
            else
            {
                Background = Brushes.White;
                BorderBrush = Brushes.White;
                Status = MineSquare.SquareStatus.Cleared;
                IsEnabled = false;
                foreach (MineSquare OtherSquare in (Field.Squares.Where
                    (s => (s.Row >= Row - 1) && (s.Row <= Row + 1) &&
                    (s.Column >= Column - 1) && (s.Column <= Column + 1) && (s.Status == MineSquare.SquareStatus.Open))
                    .ToList()))
                    OtherSquare.Unmine(sender);
                {

                }
            }
        }
        
	
    }
}
