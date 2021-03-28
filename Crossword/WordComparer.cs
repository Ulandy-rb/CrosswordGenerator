using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	class WordComp :IComparer<Word>
	{
		public int Compare(Word w1, Word w2)
		{
			var w1Lengt = w1.Length;
			var w2Lenth = w2.Length;
			if (w1Lengt > w2Lenth)
				return -1;
			if (w1Lengt < w2Lenth)
				return 1;
			return 0;
		}
	}
}
