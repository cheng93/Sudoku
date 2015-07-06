using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.Strategy
{
    public class HiddenSinglesStrategy : BaseSudokuStrategy
    {
        public HiddenSinglesStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (HiddenSingles(box))
                {
                    output = true;
                }
            }

            foreach (var column in Board.Columns)
            {
                if (HiddenSingles(column))
                {
                    output = true;
                }
            }

            foreach (var row in Board.Rows)
            {
                if (HiddenSingles(row))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool HiddenSingles(Unit unit)
        {
            var output = false;
            var solvedCells = CheckForLastRemainingNumber(unit);

            if (solvedCells.Count > 0)
            {
                output = true;
                foreach (var cell in solvedCells)
                {
                    Console.WriteLine("(Hidden single)({2}): {0} in cell: {1}", cell.Value, cell.Name, unit.Name);
                }
            }

            return output;
        }

        public IReadOnlyCollection<Cell> CheckForLastRemainingNumber(Unit unit)
        {
            var list = new List<Cell>();
            for (int i = 1; i < 10; i++)
            {
                if (unit.Cells.All(c => c.Value != i) && unit.Cells.Count(c => c.PotentialValues.Contains(i)) == 1)
                {
                    var cell = unit.Cells.Select(c => c).Single(c => c.PotentialValues.Contains(i));
                    cell.Value = i;
                    list.Add(cell);
                }
            }

            return new ReadOnlyCollection<Cell>(list);
        }
    }
}
