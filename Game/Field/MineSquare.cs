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
    public class MineSquare : Button
    {
        private Pokemon.Pokemon pokemonValue;



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
            get { return nrOfRowsValue; }
            set { nrOfRowsValue = value; }
        }

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
                foreach ( MineSquare Square in ( Field.Squares.Where
                    ( s => ( s.Row >= this.Row - 1 ) && ( s.Row <= this.Row + 1 ) &&
                    ( s.Column >= this.Column - 1 ) && ( s.Column <= this.Column + 1 ) )
                    .ToList() ) )
                {
                    if ( Square.Pokemon != null ) mines++;
                }
                return mines;
            }
        }

        public MineSquare( MineField field, int rows, int columns, int row, int column )
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
                List<MineSquare> FlaggedSqaures = Field.Squares.Where( square => square.Status == MineSquare.SquareStatus.Flagged ).ToList();
                if ( FlaggedSqaures.Count() == sender.Game.FieldLevels[sender.Game.Level].Pokemon )
                {
                    bool win = true;
                    foreach ( MineSquare flaggedSquare in FlaggedSqaures )
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

        public void LeftButton( GameWindow sender )
        {

            if ( Status == SquareStatus.Open )
            {
                Unmine( sender );
                if ( Field.ClearedSquares + sender.Game.FieldLevels[sender.Game.Level].Pokemon == sender.Game.FieldLevels[sender.Game.Level].Dimention ) Game.Messages.Score.ShowScore( sender, Field );
            }
        }

        public void Unmine( GameWindow sender )
        {
            this.Field.NrOfClicks++;
            if ( Pokemon != null )
            {

                Content = new Image { Source = Pokemon.Picture };
                Status = MineSquare.SquareStatus.Mine;
                Background = Brushes.Red;
                BorderBrush = Brushes.Red;
                IsEnabled = false;
                Game.Messages.FailMessage Fail = new Game.Messages.FailMessage();
                Fail.EscapedPokemon.Source = Pokemon.Picture;
                Fail.Message.Text = Pokemon.Number + " - " + Pokemon.Name + " managed to escape!";
                Fail.Title = "Game over!";
                Fail.Owner = sender;
                Fail.ShowDialog();
                sender.Game.NewField( sender );

            }
            else if ( Mines > 0 )
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
                foreach ( MineSquare OtherSquare in ( Field.Squares.Where
                    ( s => ( s.Row >= Row - 1 ) && ( s.Row <= Row + 1 ) &&
                    ( s.Column >= Column - 1 ) && ( s.Column <= Column + 1 ) && ( s.Status == MineSquare.SquareStatus.Open ) )
                    .ToList() ) )
                    OtherSquare.Unmine( sender );
            }
        }


    }
}
