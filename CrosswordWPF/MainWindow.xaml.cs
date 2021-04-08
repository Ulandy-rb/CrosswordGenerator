using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Crossword;

namespace CrosswordWPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private DrawingBlocks[,] drawingBlocks;
        public bool CrosswordFailed = false;
        public MainWindow()
		{
			InitializeComponent();
            if(drawingBlocks==null)
			{
                ButtonCheck.Visibility = Visibility.Hidden;
                ButtonDelete.Visibility = Visibility.Hidden;
			}
		}

		private void Button_Generate(object sender, RoutedEventArgs e)
		{
			try
			{
				var generator = new Generator();
				var blocks = generator.GenerateCrossword();
                ButtonCheck.Visibility = Visibility.Visible;
                ButtonDelete.Visibility = Visibility.Visible;
                DrawingGrid(blocks);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Button_Check(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < drawingBlocks.GetLength(0); i++)
			{
				for (int j = 0; j < drawingBlocks.GetLength(1); j++)
				{
                    if(drawingBlocks[i,j]!=null)
                        drawingBlocks[i, j].ShowTextBox();
				}
			}
		}

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < drawingBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < drawingBlocks.GetLength(1); j++)
                {
                    if (drawingBlocks[i, j] != null)
                        drawingBlocks[i, j].RemoveTextBox();
                }
            }
        }

        private void DrawingGrid(Block[,] blocks)
        {
            drawingBlocks = new DrawingBlocks[blocks.GetLength(0), blocks.GetLength(1)];
            MainGrid.Children.RemoveRange(0, MainGrid.Children.Count);
            Border background = new Border
            {
                Background = new SolidColorBrush(Colors.Transparent)
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
                    var drawingBlock = new DrawingBlocks(blocks[x, y].letter,blocks[x,y].direction, x, y);
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

		private void Ellipse_MouseLeftButtonDown_Close(object sender, MouseButtonEventArgs e)
		{
			try
			{
                Close();
			}
            catch (Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
		}

		private void Ellipse_MouseLeftButtonDown_Minimized(object sender, MouseButtonEventArgs e)
		{
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
	}
}
