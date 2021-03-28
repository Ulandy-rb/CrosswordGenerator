using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	public class Word 
    {
        private readonly Letter[] letters;
        public int Length { get; set; }

        public bool Placed { get; set; }
        public Word(string word)
        {
            word = word.ToUpper();
            Length = word.Length;
            letters = new Letter[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                letters[i] = new Letter(word[i]);
            }
        }

        public Letter[] Letters
		{
            get => letters;
		}

        public string AsString()
		{
            string word = "";
			foreach (var item in letters)
			{
                word += item.Character;
			}
            return word;
		}
    }
}
