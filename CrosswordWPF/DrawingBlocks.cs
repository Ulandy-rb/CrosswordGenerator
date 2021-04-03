using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Crossword;

namespace CrosswordWPF
{
	public class DrawingBlocks
	{
        private double blockSize = 60;
        private Border foreground;
        private Border border;
        public Grid Grid;
        public TextBox textBox;
        public TextBlock textBlock;
        private Viewbox viewbox;

        public DrawingBlocks(Letter letter, char direction, int x, int y)
        {   
            Grid = new Grid();
            Grid.SetRow(Grid, y);
            Grid.SetColumn(Grid, x);
            Grid.Width = Grid.Height = blockSize;

            border = new Border
            {
                Background = new SolidColorBrush(Colors.Black),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(1)
            };
            Grid.Children.Add(border);

            foreground = new Border
            {
                Background = new SolidColorBrush(Colors.White),
                CornerRadius = new CornerRadius(10),
                UseLayoutRounding = true,
                Margin = new Thickness(1)
            };
            Grid.Children.Add(foreground);

            textBox = new TextBox
            {
                BorderThickness = new Thickness(0),
                Text = null,
                MaxLength = 1,
                FontSize = blockSize/2,
                Height=blockSize,
                Width = blockSize,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Colors.Black),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent)
            };
			textBox.PreviewTextInput += TextBox_PreviewTextInput;

            textBlock = new TextBlock
            {
                Padding = new Thickness(3),
                MaxWidth = 60,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Text = letter.Character.ToString(),
                Foreground = new SolidColorBrush(Colors.Black)
            };
            if (letter.Character.Length > 1)
            { 
                if (direction == 'H')
                    textBlock.Text += "\n\u2192";
                if (direction == 'V')
                    textBlock.Text += "\n\u2193";
             };

            viewbox = new Viewbox();
            if (letter.Character.Length > 1)
                viewbox.Child = textBlock;
            else
                viewbox.Child = textBox;
            foreground.Child = viewbox;
        }

		private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
            string Symbol = e.Text.ToString();

            if (!Regex.Match(Symbol, @"[а-яА-Я]").Success)
            {
                e.Handled = true;
            }
        }
        public void ShowTextBox()
		{
            if ((textBlock.Text.Length == 1 || textBox.Text == null) && textBox.Text.ToUpper() != textBlock.Text.ToUpper())
                textBlock.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
            viewbox.Child = textBlock;
        }

        public void RemoveTextBox()
		{
            if (textBlock.Text.Length == 1)
                textBox.Text = null;
		}
    }
}
