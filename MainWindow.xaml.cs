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
        public MainWindow()
        {
            InitializeComponent();
            MineField mijnveld = new MineField(256);
            int count = 0;
            foreach (MineSquare vakje in mijnveld.Squares)
            {
                if (count%Math.Sqrt(256)==0) MyText.Text+="\n";
                if (vakje.Mine) MyText.Text += "x";
                else MyText.Text += vakje.Mines.ToString();
                count++;
            }
        }
    }
}
