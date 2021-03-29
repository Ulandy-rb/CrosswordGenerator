using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	public class Data
	{
		public List<Word> Words { get; set; }
		private const string pathDescriptions = "C:\\Users\\mi\\Documents\\Visual Studio\\C#\\CrosswordGenerator\\Crossword\\BDDescriptions.txt";
		private const string pathWords = "C:\\Users\\mi\\Documents\\Visual Studio\\C#\\CrosswordGenerator\\Crossword\\BDWords.txt";
		public Data()
		{
			Words = new List<Word>();
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
			}
			catch
			{
				throw new Exception("Отстутсвует база слов");
			}
		}
	}
}
