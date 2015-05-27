using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PokemonSweeper.Game.Messages;
using PokemonSweeper.Game.Pokemon;

namespace PokemonSweeper
{
    public class Square : Button
    {
        public enum SquareStatus
        {
            Cleared,
            Pokemon,
            Open,
            Flagged,
            Question
        }

        public Square(Field field, int rows, int columns, int row, int column)
        {
            Field = field;
            NrOfRows = rows;
            NrOfColumns = columns;
            Row = row;
            Column = column;
            Pokemon = null;
            Status = SquareStatus.Open;
        }

        public SquareStatus Status { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int NrOfRows { get; set; }

        public int NrOfColumns
        {
            get { return NrOfRows; }
            set { NrOfRows = value; }
        }

        public Field Field { get; set; }
        public Pokemon Pokemon { get; set; }

        public int Mines
        {
            get
            {
                var mines = 0;
                foreach (var Square in (Field.Squares.Where
                    (s => (s.Row >= Row - 1) && (s.Row <= Row + 1) &&
                          (s.Column >= Column - 1) && (s.Column <= Column + 1))
                    .ToList()))
                {
                    if (Square.Pokemon != null) mines++;
                }
                return mines;
            }
        }

        public void RightButton(GameWindow sender)
        {
            if (Status == SquareStatus.Open)
            {
                Status = SquareStatus.Flagged;
                Content = new Image {Source = new BitmapImage(new Uri(@"/Game/images/pokeball.png", UriKind.Relative))};
                var FlaggedSqaures = Field.Squares.Where(square => square.Status == SquareStatus.Flagged).ToList();
                if (FlaggedSqaures.Count() == sender.Game.FieldLevels[sender.Game.Level].Pokemon)
                {
                    var win = true;
                    foreach (var flaggedSquare in FlaggedSqaures)
                    {
                        if (flaggedSquare.Pokemon == null)
                        {
                            win = false;
                        }
                    }
                    if (win) Score.ShowScore(sender, Field);
                }
            }
            else if (Status == SquareStatus.Flagged)
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

        public void LeftButton(GameWindow window)
        {
            if (Status == SquareStatus.Open)
            {
                SwipeSquare(window);
                if (Field.ClearedSquares + window.Game.FieldLevels[window.Game.Level].Pokemon ==
                    window.Game.FieldLevels[window.Game.Level].Dimention) Score.ShowScore(window, Field);
            }
        }

        public void SwipeSquare(GameWindow window)
        {
            Field.NrOfClicks++;
            if (Pokemon != null)
            {
                Content = new Image {Source = Pokemon.Picture};
                Status = SquareStatus.Pokemon;
                Background = Brushes.Red;
                BorderBrush = Brushes.Red;
                IsEnabled = false;
                FailMessage.ShowMessage(window, Pokemon);
            }
            else if (Mines > 0)
            {
                Content = Mines;
                Status = SquareStatus.Cleared;
                Background = Brushes.White;
                BorderBrush = Brushes.White;
                IsEnabled = false;
            }
            else
            {
                Background = Brushes.White;
                BorderBrush = Brushes.White;
                Status = SquareStatus.Cleared;
                IsEnabled = false;
                foreach (var OtherSquare in (Field.Squares.Where
                    (s => (s.Row >= Row - 1) && (s.Row <= Row + 1) &&
                          (s.Column >= Column - 1) && (s.Column <= Column + 1) && (s.Status == SquareStatus.Open))
                    .ToList()))
                    OtherSquare.SwipeSquare(window);
            }
        }
    }
}