using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
	public class PlacementComp : IComparer<Placement>
	{
        public int Compare(Placement a, Placement b)
        {
            if (a.Word.Length - (a.Expansion.TotalY + a.Expansion.TotalX) < b.Word.Length - (b.Expansion.TotalY + b.Expansion.TotalX))
            {
                return 1;
            }

            if (a.Word.Length - (a.Expansion.TotalY + a.Expansion.TotalX) > b.Word.Length - (b.Expansion.TotalY + b.Expansion.TotalX))
            {
                return -1;
            }

            return 0;
        }
    }
}
