using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	public class Block
	{
		public readonly Letter letter;
		public readonly char direction;
		public Block(Letter letter, char direction = '\0')
		{
			this.letter = letter;
			this.direction = direction;
		}
	}
}
