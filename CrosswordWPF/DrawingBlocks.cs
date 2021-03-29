using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Crossword;

namespace CrosswordWPF
{
	public class DrawingBlocks
	{
        private double blockSize = 30;
        private Border foreground;
        private Border border;
        public Grid Grid;

        private Letter Letter { get; set; }

        public TextBox textBox;
        public TextBlock textBlock;

        public DrawingBlocks(Letter letter, int x, int y)
        {
            Grid = new Grid();
            Grid.SetRow(Grid, y);
            Grid.SetColumn(Grid, x);
            Grid.Width = Grid.Height = blockSize;
            border = new Border
            {
                Background = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(-1, -1, 0, 0)
            };
            Grid.Children.Add(border);
            foreground = new Border
            {
                Background = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, 0, 1, 1)
            };
            Grid.Children.Add(foreground);
            this.Letter = letter;
            textBox = new TextBox
            {

                Text = "",
                MaxLength = 1,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            foreground.Child = textBox;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;


            textBlock = new TextBlock
            {
                Text = letter.Character.ToString(),
                Foreground = new SolidColorBrush(Colors.Black)
            };
            foreground.Child = textBlock;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
        }


        public double BlockSize
        {
            get => blockSize;
            set
            {
                blockSize = value;
                Grid.Height = Grid.Width = blockSize;
            }
        }
    }
}
