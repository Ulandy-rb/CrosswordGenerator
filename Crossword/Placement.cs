using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	public class Placement
	{
		public Word Word { get; set; }

		public readonly char direction;
		public BlockCoordinates[] Coordinates { get; set; }

		public Expansion Expansion { get; set; } = new Expansion();

		public Placement(Word word, char direction)
		{
			this.Word = word;
			this.direction = direction;
			Coordinates = new BlockCoordinates[word.Length + 1];
		}
		
	}
}
