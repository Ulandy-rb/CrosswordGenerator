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
using Crossword;
using Block = Crossword.Block;

namespace CrosswordWPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private DrawingBlocks[,] drawingBlocks;
        private double SizeFactor = 1;
        private double BaseSize = 30;
        public bool CrosswordFailed = false;
        private Color backgroundColor = Colors.Black;
        public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Generate(object sender, RoutedEventArgs e)
		{
			try
			{
				var generator = new Generator();
				var blocks = generator.GenerateCrossword();
                DrawingGrid(blocks);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}

        private void DrawingGrid(Block[,] blocks)
        {
            drawingBlocks = new DrawingBlocks[blocks.GetLength(0), blocks.GetLength(1)];
            MainGrid.Children.RemoveRange(0, MainGrid.Children.Count);
            Border background = new Border
            {
                Background = new SolidColorBrush(backgroundColor)
            };
            MainGrid.Children.Add(background);
            Grid.SetRowSpan(background, drawingBlocks.GetLength(1));
            Grid.SetColumnSpan(background, drawingBlocks.GetLength(0));

            GenerateGridRowsAndColumns();
            for (int y = 0; y < blocks.GetLength(1); y++)
            {
                for (int x = 0; x < blocks.GetLength(0); x++)
                {
                    if (blocks[x, y] == null) continue;
                    var drawingBlock = new DrawingBlocks(blocks[x, y].letter, x, y);
                    drawingBlocks[x, y] = drawingBlock;
                    MainGrid.Children.Add(drawingBlock.Grid);
                }
            }
            MainGrid.Width = double.NaN;
            MainGrid.Height = double.NaN;
        }

        private void GenerateGridRowsAndColumns()
        {
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < drawingBlocks.GetLength(0); i++)
            {
                ColumnDefinition column = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                MainGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < drawingBlocks.GetLength(1); i++)
            {
                RowDefinition row = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                MainGrid.RowDefinitions.Add(row);
            }
        }
    }
}
