using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crossword
{
	public class Generator
	{
		private readonly Regex regex = new Regex(@"^[А-Я]+$");
		private readonly List<Word> words = new List<Word>() { new Word("mola"), new Word("LOL"), new Word("holla") };
		private Block[,] blocks;
		public bool AllWordsValid(List<Word> list)
		{
			foreach (var item in list)
			{
				if (!regex.Match(item.AsString()).Success || string.IsNullOrEmpty(item.ToString()))
					throw new FormatException("Some words are not valid!");
			}
			return true;
		}

		public bool Generate()
		{
			return true;
		}
	}
}
