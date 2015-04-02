using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PokemonSweeper.Game.Field.Pokemon;
using System.Drawing;

namespace PokemonSweeper.Game.Field
{
    public class Square : Button
    {
        private Pokemon.Pokemon pokemonValue;



        public enum SquareStatus
        {
            Cleared,
            Pokemon,
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
            get { return nrOfRowsValue; }
            set { nrOfRowsValue = value; }
        }

        public int NrOfColumns
        {
            get { return nrOfRowsValue; }
            set { nrOfRowsValue = value; }
        }

        private Field fieldValue;

        public Field Field
        {
            get { return fieldValue; }
            set { fieldValue = value; }
        }

        public Pokemon.Pokemon Pokemon
        {
            get { return pokemonValue; }
            set { pokemonValue = value; }
        }


        public int Mines
        {
            get
            {
                int mines = 0;
                foreach ( Square Square in ( Field.Squares.Where
                    ( s => ( s.Row >= this.Row - 1 ) && ( s.Row <= this.Row + 1 ) &&
                    ( s.Column >= this.Column - 1 ) && ( s.Column <= this.Column + 1 ) )
                    .ToList() ) )
                {
                    if ( Square.Pokemon != null ) mines++;
                }
                return mines;
            }
        }

        public Square( Field field, int rows, int columns, int row, int column )
        {
            Field = field;
            NrOfRows = rows;
            NrOfColumns = columns;
            Row = row;
            Column = column;
            Pokemon = null;
            Status = SquareStatus.Open;
        }

        public void RightButton( GameWindow sender )
        {

            if ( Status == SquareStatus.Open )
            {
                Status = SquareStatus.Flagged;
                Content = new Image { Source = new BitmapImage( new Uri( @"/Game/images/pokeball.png", UriKind.Relative ) ) };
                List<Square> FlaggedSqaures = Field.Squares.Where( square => square.Status == Square.SquareStatus.Flagged ).ToList();
                if ( FlaggedSqaures.Count() == sender.Game.FieldLevels[sender.Game.Level].Pokemon )
                {
                    bool win = true;
                    foreach ( Square flaggedSquare in FlaggedSqaures )
                    {
                        if ( flaggedSquare.Pokemon == null )
                        {
                            win = false;
                        }
                    }
                    if ( win ) Game.Messages.Score.ShowScore( sender, Field );

                }
            }
            else if ( Status == SquareStatus.Flagged )
            {
                Status = SquareStatus.Question;
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

        public void LeftButton( GameWindow window )
        {

            if ( Status == SquareStatus.Open )
            {
                SwipeSquare( window );
                if ( Field.ClearedSquares + window.Game.FieldLevels[window.Game.Level].Pokemon == window.Game.FieldLevels[window.Game.Level].Dimention ) Game.Messages.Score.ShowScore( window, Field );
            }
        }

        public void SwipeSquare( GameWindow window )
        {
            this.Field.NrOfClicks++;
            if ( Pokemon != null )
            {

                Content = new Image { Source = Pokemon.Picture };
                Status = Square.SquareStatus.Pokemon;
                Background = Brushes.Red;
                BorderBrush = Brushes.Red;
                IsEnabled = false;
                Game.Messages.FailMessage.ShowMessage( window, Pokemon );

            }
            else if ( Mines > 0 )
            {
                Content = Mines;
                Status = Square.SquareStatus.Cleared;
                Background = Brushes.White;
                BorderBrush = Brushes.White;
                IsEnabled = false;
            }
            else
            {
                Background = Brushes.White;
                BorderBrush = Brushes.White;
                Status = Square.SquareStatus.Cleared;
                IsEnabled = false;
                foreach ( Square OtherSquare in ( Field.Squares.Where
                    ( s => ( s.Row >= Row - 1 ) && ( s.Row <= Row + 1 ) &&
                    ( s.Column >= Column - 1 ) && ( s.Column <= Column + 1 ) && ( s.Status == Square.SquareStatus.Open ) )
                    .ToList() ) )
                    OtherSquare.SwipeSquare( window );
            }
        }
    }
}
