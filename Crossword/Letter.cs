using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	class Letter
	{
        public BlockCoordinates Coordinates;
        public char Character;

        public Letter(char character)
        {
            Character = character;
        }
    }
}
