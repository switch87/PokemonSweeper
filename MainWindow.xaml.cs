using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MineSweeper1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MineField mijnveld;
        public MainWindow()
        {
            InitializeComponent();

            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mijnveld = new MineField(81);

            

            int count = 0;
            foreach (MineSquare vakje in mijnveld.Squares)
            {
                vakje.Click += vakje_Click;
                vakje.MouseRightButtonDown += vakje_MouseRightButtonDown;
                MineFieldGrid.Children.Add(vakje);
            }
        }

        void vakje_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MineSquare knopje = ((MineSquare)sender);
            if (knopje.Status == MineSquare.SquareStatus.Open)
            {
                knopje.Status = MineSquare.SquareStatus.Flagged;
                knopje.Content = "!";
                knopje.Foreground = Brushes.Red;
                knopje.FontWeight = FontWeights.Bold;
            }
            else if (knopje.Status == MineSquare.SquareStatus.Flagged)
            {
                knopje.Status = MineSquare.SquareStatus.Question;
                knopje.Content = "?";
                knopje.Foreground = Brushes.Blue;
                knopje.FontWeight = FontWeights.Bold;
            }
            else
            {
                knopje.Status = MineSquare.SquareStatus.Open;
                knopje.Content = "";
            }
        }

        void vakje_Click(object sender, RoutedEventArgs e)
        {
            OntmijnKnopje ((MineSquare)sender);
            
            //
            
        }

        public void OntmijnKnopje(MineSquare knopje)
        {
            if (knopje.Mine)
            {
                knopje.Content = "boe";
                knopje.Status = MineSquare.SquareStatus.Mine;
                knopje.Background = Brushes.Red;
                knopje.BorderBrush = Brushes.Red;
                knopje.IsEnabled = false;

            }
            else if (knopje.Mines > 0)
            {
                knopje.Content = knopje.Mines;
                knopje.Status = MineSquare.SquareStatus.Cleared;
                knopje.Background = Brushes.White;
                knopje.BorderBrush = Brushes.White;
                knopje.IsEnabled = false;
            }
            else
            {
                knopje.Background = Brushes.White;
                knopje.BorderBrush = Brushes.White;
                knopje.Status = MineSquare.SquareStatus.Cleared;
                knopje.IsEnabled = false;
                foreach (MineSquare anderKnopje in (mijnveld.Squares.Where
                    (s => (s.Row >= knopje.Row - 1) && (s.Row <= knopje.Row + 1) &&
                    (s.Column >= knopje.Column - 1) && (s.Column <= knopje.Column + 1) && (s.Status == MineSquare.SquareStatus.Open))
                    .ToList()))
                    OntmijnKnopje(anderKnopje);
                {

                }
            }
        }
    }
}
