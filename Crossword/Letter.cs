using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	public class Letter
	{
        public BlockCoordinates Coordinates;
        public string Character;

        public Letter(string character)
        {
            Character = character;
        }
    }
}
