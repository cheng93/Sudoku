using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku
{
    public static class Logic
    {
        public static IReadOnlyCollection<int> ExtractValues(IReadOnlyCollection<Cell> cells)
        {
            var output = cells.SelectMany(cell => cell.PotentialValues).Distinct().ToList();

            return new ReadOnlyCollection<int>(output);
        }
    }
}