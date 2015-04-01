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

namespace PokemonSweeper.Game.Field
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        public MineField mijnveld;
        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NewGame(81);
        }

        public void NewGame(int dimention)
        {
            MineFieldGrid.Children.Clear();
            
            mijnveld = new MineField(dimention);
            MineFieldGrid.Rows = mijnveld.Rows;
            MineFieldGrid.Columns = mijnveld.Columns;
            foreach (MineSquare vakje in mijnveld.Squares)
            {
                vakje.Click += MineSquare_Click;
                vakje.MouseRightButtonDown += MineSquare_MouseRightButtonDown;
                MineFieldGrid.Children.Add(vakje);
            }
        }

        void MineSquare_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((MineSquare)sender).RightButton(this);
        }

        void MineSquare_Click(object sender, RoutedEventArgs e)
        {
            ((MineSquare)sender).LeftButton(this);
            
        }
    }
}
