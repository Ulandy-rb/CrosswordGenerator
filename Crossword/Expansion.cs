using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	internal class Expansion
	{
		public int Up { get; set; } = 0;
		public int Down { get; set; } = 0;
		public int Left { get; set; } = 0;
		public int Right { get; set; } = 0;

		public int TotalY => Up + Down;
		public int TotalX => Right + Left;
	}
}
