using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	public class Word 
    {
        private readonly string description;
        private readonly Letter[] letters;
        public char Direction { get;set; }
        public int Length { get; set; }

        public bool Placed { get; set; }
        public Word(string word, string description)
        {
            this.description = description;
            word = word.ToUpper();
            Length = word.Length;
            letters = new Letter[word.Length+1];
            for (int i = 1; i < word.Length+1; i++)
            {
                letters[i] = new Letter(word[i-1].ToString());
            }
            letters[0] = new Letter(this.description);
        }

        public Letter[] Letters
		{
            get => letters;
		}

        public string Desccription
        {
            get => description;
        }

        public string AsString()
		{
            string word = "";
			for (int i = 1; i < letters.Length; i++)
			{
                word += letters[i].Character;
			}
            return word;
		}
    }
}
