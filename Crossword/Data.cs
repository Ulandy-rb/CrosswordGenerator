using System.Collections.Generic;
using System.IO;

namespace Crossword
{
	internal class Data
	{
		private readonly List<Word> Words;
		private const string pathDescriptions = "C:\\Users\\mi\\Documents\\Visual Studio\\C#\\CrosswordGenerator\\Crossword\\BDDescriptions.txt";
		private const string pathWords = "C:\\Users\\mi\\Documents\\Visual Studio\\C#\\CrosswordGenerator\\Crossword\\BDWords.txt";
		public Data()
		{
			Words = new List<Word>();
		}

		public List<Word> AddWords()
		{
			try
			{
				using (StreamReader readWords = new StreamReader(pathWords, System.Text.Encoding.UTF8))
				using (StreamReader readDescriptions = new StreamReader(pathDescriptions, System.Text.Encoding.UTF8))
				{
					string lineWords, lineDescriptions;
					while ((lineWords = readWords.ReadLine()) != null && (lineDescriptions = readDescriptions.ReadLine()) != null)
					{
						Words.Add(new Word(lineWords, lineDescriptions));
					}
				}
				return Words;
			}
			catch
			{
				throw new IOException("Отстутсвует база слов");
			}
		}
	}
}
