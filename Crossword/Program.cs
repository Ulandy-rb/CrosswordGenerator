using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Crossword
{
	class Program
	{
		static void Main(string[] args)
		{
			Generator generator = new Generator();
			var block = generator.GenerateCrossword();

			for (int x = 0; x < block.GetLength(0); x++)
			{
				for (int i = 0; i < block.GetLength(1); i++)
				{
					if (block[x, i] != null)
						Console.Write(block[x, i].letter.Character + " ");
					else
						Console.Write("* ");
				}
				Console.WriteLine();
			}
		}
	}
}
