using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Crossword;
using System.Collections.Generic;

namespace CrosswordTest
{
	[TestClass]
	public class CrosswordTest
	{
		[TestMethod]
		public void TestAllWordsValid_False()
		{
			List<Word> words = new List<Word>
			{
				new Word("Кот"),
				new Word("Собака"),
				new Word("Змея.В/")
			};
			Generator generator = new Generator();
			Assert.ThrowsException<FormatException>(() => generator.AllWordsValid(words));
		}
		[TestMethod]
		public void TestAllWordsValid_True()
		{
			List<Word> words = new List<Word>
			{
				new Word("Кот"),
				new Word("Собака"),
				new Word("Змея")
			};
			Generator generator = new Generator();
			Assert.IsTrue(generator.AllWordsValid(words));
		}
		[TestMethod]
		public void TestAllWordsValid_Empty()
		{
			List<Word> words = new List<Word>
			{
				new Word(""),
				new Word("КОТ")
			};
			Generator generator = new Generator();
			Assert.ThrowsException<FormatException>(() => generator.AllWordsValid(words));
		}
	}
}
