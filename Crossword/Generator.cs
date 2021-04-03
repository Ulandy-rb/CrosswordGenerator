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
		private const int MINRANDOM = 5;
		private const int MAXRANDOM = 20;
		private readonly Regex regex = new Regex(@"^[А-Я]+$");
		private readonly List<Word> words;
		private Block[,] blocks;
		private int placedWordsCount = 0;
		private int Count { get; set; }

		private bool AllWordsValid(List<Word> list)
		{
			foreach (var item in list)
			{
				if (!regex.Match(item.AsString()).Success || string.IsNullOrEmpty(item.ToString()))
					throw new FormatException("Some words are not valid!");
			}
			return true;
		}

		public Generator()
		{
			try
			{
				var data = new Data();
				words = data.Words;
				if (AllWordsValid(words))
				{
					Random rand = new Random();
					for (int i = words.Count - 1; i >= 1; i--)
					{
						int j = rand.Next(i + 1);

						var tmp = words[j];
						words[j] = words[i];
						words[i] = tmp;
					}
					Count = rand.Next(MINRANDOM, MAXRANDOM);
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
		}

		public Block[,] GenerateCrossword()
		{
			foreach (var item in words)
			{
				item.Placed = false;
			}
			if (!PlaceAllWord())
				throw new Exception("Из этих слов нельзя составить сканворд");
			else
			return blocks;
		}

		private bool PlaceAllWord()
		{
			for (int i = 0; i < words.Count; i++)
			{
				placedWordsCount = 0;
				if (PlaceFirstWord(words[i]))
					return true;
			}
			return false;
		}

		private bool PlaceFirstWord(Word word)
		{
			blocks = new Block[word.Length + 1, 1];
			var placement = new Placement(word, 'H');
			for (int i = 0; i < word.Length + 1; i++)
			{
				placement.Coordinates[i] = new BlockCoordinates(i, 0);
			}
			PlaceWordOnBoard(placement);
			if (!PlaceNextWord())
			{
				blocks = null;
				word.Placed = false;
				placedWordsCount--;
				return false;
			}	
			return true;
		}

		private void PlaceWordOnBoard(Placement placement)
		{
			if (placement.Expansion.TotalX > 0 || placement.Expansion.TotalY > 0)
				ExpandBlock(placement);
			for (int i = 1; i < placement.Coordinates.Length; i++)
			{
				var item = placement.Coordinates[i];
				blocks[item.X, item.Y] = new Block(placement.Word.Letters[i]);
			}
			var it = placement.Coordinates[0];
			blocks[it.X, it.Y] = new Block(placement.Word.Letters[0], placement.direction);
			placement.Word.Placed = true;
			placedWordsCount++;
		}

		private void ExpandBlock(Placement placement)
		{
			var newBlock = new Block[Math.Abs(placement.Expansion.TotalX) + blocks.GetLength(0), Math.Abs(placement.Expansion.TotalY) + blocks.GetLength(1)];
			for (int x = 0; x < blocks.GetLength(0); x++)
			{
				for (int y = 0; y < blocks.GetLength(1); y++)
				{
					newBlock[x + placement.Expansion.Left, y + placement.Expansion.Up] = blocks[x, y];
				}
			}
			blocks = newBlock;
		}

		private bool PlaceNextWord()
		{
			List<Placement> placements = new List<Placement>();
			for (int i = 0; i < words.Count; i++)
			{
				if (words[i].Placed) continue;
				placements.AddRange(FindPossiblePlacements(words[i]));
			}
			placements.Sort(new PlacementComp());
			foreach (var item in placements)
			{
				Block[,] blockState = (Block[,])blocks.Clone();
				PlaceWordOnBoard(item);
				if (placedWordsCount == Count || placedWordsCount == words.Count || PlaceNextWord())
				{
					return true;
				}
				blocks = blockState;
				placedWordsCount--;
				item.Word.Placed = false;
			}
			return false;
		}

		private IEnumerable<Placement> FindPossiblePlacements(Word word)
		{
			List<Placement> placements = new List<Placement>();
			for (int x = 0; x < blocks.GetLength(0); x++)
			{
				for (int y = 0; y < blocks.GetLength(1); y++)
				{
					for (int i = 1; i < word.Length + 1; i++) 
					{
						var indexLetter = i;
						var block = blocks[x, y];
						if (block != null && block.letter.Character == word.Letters[i].Character)
						{
							Placement placement;
							if (CanPlacedVertical(word, new BlockCoordinates(x,y), indexLetter, out placement))
							{
								if(placement!=null)
									placements.Add(placement);
							}
							else if (CanPlaceHorizantal(word, new BlockCoordinates(x, y), indexLetter, out placement))
							{
								if (placement != null)
									placements.Add(placement);
							}
						}
					}
				}
			}
			return placements;
		}

		private bool CanPlaceHorizantal(Word word, BlockCoordinates blockCoordinates, int indexLetter, out Placement placement)
		{
			placement = new Placement(word, 'H');
			var currentBlock = new BlockCoordinates(blockCoordinates.X, blockCoordinates.Y);

			placement.Coordinates = new BlockCoordinates[word.Length + 1];
			placement.Coordinates[indexLetter] = blockCoordinates;

			for (int i = indexLetter - 1; i >= 0; i--)
			{
				currentBlock.X--;
				if (currentBlock.X < 0)
				{
					placement.Expansion.Left++;
				}
				else
				{
					if (!LetterCanPlace(currentBlock, word, i, 'L'))
						return false;
				}
				placement.Coordinates[i] = new BlockCoordinates(currentBlock.X, currentBlock.Y);
			}

			currentBlock = new BlockCoordinates(blockCoordinates.X, blockCoordinates.Y);
			for (int i = indexLetter + 1; i < word.Length + 1; i++)
			{
				currentBlock.X++;
				if (currentBlock.X > blocks.GetLength(0) - 1)
				{
					placement.Expansion.Right++;
				}
				else
				{
					if (!LetterCanPlace(currentBlock, word, i,'R'))
						return false;
				}
				placement.Coordinates[i] = new BlockCoordinates(currentBlock.X, currentBlock.Y);
			}

			if (placement.Expansion.Left > 0)
			{
				foreach (var coordinate in placement.Coordinates)
				{
					coordinate.X += placement.Expansion.Left;
				}
			}

			return true;
		}

		private bool LetterCanPlace(BlockCoordinates currentBlock, Word word, int indexLetter, char direction)
		{
			var block = OutOfBounds(currentBlock.X, currentBlock.Y) ? blocks[currentBlock.X, currentBlock.Y ] : null;
			Block nextBlock = null, sideBlockL, sideBlockR, sideBlockU,sideBlockD;

			if (direction == 'U' || direction == 'D')
			{
				if (direction == 'U') nextBlock = OutOfBounds(currentBlock.X, currentBlock.Y - 1) ? blocks[currentBlock.X, currentBlock.Y - 1]: null;
				if (direction == 'D') nextBlock = OutOfBounds(currentBlock.X, currentBlock.Y + 1) ? blocks[currentBlock.X, currentBlock.Y + 1] : null;
				sideBlockL = OutOfBounds(currentBlock.X - 1, currentBlock.Y) ? blocks[currentBlock.X - 1, currentBlock.Y] : null;
				sideBlockR = OutOfBounds(currentBlock.X + 1, currentBlock.Y) ? blocks[currentBlock.X + 1, currentBlock.Y] : null;
				if (word.Letters[indexLetter].Character == word.Desccription)
				{
					if (block != null) return false;
				}
				else
				{ 
					if (block != null && block.letter.Character != word.Letters[indexLetter].Character
					|| sideBlockL != null || sideBlockR != null)
						return false;
					if (word.Letters.Length - 1 == indexLetter && nextBlock != null)
						return false;
				}

			}

			if (direction == 'R' || direction == 'L')
			{
				if (direction == 'R') nextBlock = OutOfBounds(currentBlock.X + 1, currentBlock.Y) ? blocks[currentBlock.X + 1, currentBlock.Y] : null;
				if (direction == 'L') nextBlock = OutOfBounds(currentBlock.X - 1, currentBlock.Y) ? blocks[currentBlock.X - 1, currentBlock.Y] : null;
				sideBlockU = OutOfBounds(currentBlock.X, currentBlock.Y-1) ? blocks[currentBlock.X, currentBlock.Y - 1] : null;
				sideBlockD = OutOfBounds(currentBlock.X, currentBlock.Y+1) ? blocks[currentBlock.X, currentBlock.Y+ 1] : null;
				if (word.Letters[indexLetter].Character == word.Desccription)
				{
					if (block != null) return false;
				}
				else
				{
					if (block != null && block.letter.Character != word.Letters[indexLetter].Character
					  || sideBlockU != null || sideBlockD != null)
						return false;
					if (word.Letters.Length - 1 == indexLetter && nextBlock != null)
						return false;
				}
			}
			return true;
		}

		private bool CanPlacedVertical(Word word, BlockCoordinates blockCoordinates, int indexLetter, out Placement placement)
		{
			placement = new Placement(word, 'V');
			var currentBlock = new BlockCoordinates(blockCoordinates.X, blockCoordinates.Y);

			placement.Coordinates = new BlockCoordinates[word.Length + 1];
			placement.Coordinates[indexLetter] = blockCoordinates;

			for (int i = indexLetter-1; i >= 0; i--) 
			{
				currentBlock.Y--;
				if (currentBlock.Y < 0)
				{
					placement.Expansion.Up++;
				}
				else
				{
					if (!LetterCanPlace(currentBlock, word,i,'U'))
						return false;
				}
				placement.Coordinates[i] = new BlockCoordinates(currentBlock.X, currentBlock.Y);
			}

			currentBlock = new BlockCoordinates(blockCoordinates.X, blockCoordinates.Y);
			for (int i = indexLetter+1; i < word.Length + 1 ; i++)
			{
				currentBlock.Y++;
				if (currentBlock.Y > blocks.GetLength(1) - 1)
				{
					placement.Expansion.Down++;
				}
				else
				{
					if (!LetterCanPlace(currentBlock, word, i,'D'))
						return false;
				}
				placement.Coordinates[i] = new BlockCoordinates(currentBlock.X, currentBlock.Y);
			}

			if (placement.Expansion.Up > 0)
			{
				foreach (var coordinate in placement.Coordinates)
				{
					coordinate.Y += placement.Expansion.Up;
				}
			}

			return true;
		}

		private bool OutOfBounds(int x, int y)
		{
			return (x < blocks.GetLength(0) && x >= 0 && y < blocks.GetLength(1) && y >= 0);
		}
	}
}
