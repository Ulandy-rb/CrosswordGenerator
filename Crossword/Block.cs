﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	class Block
	{
		public Letter letter { get; set; }

		public Block(Letter letter)
		{
			this.letter = letter;
		}
	}
}
