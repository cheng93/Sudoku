using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Strategy
{
    public class PointingReductionStrategy : BaseSudokuStrategy
    {
        public PointingReductionStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (PointingReduction(box))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool PointingReduction(Unit unit)
        {
            var output = false;

            for (int i = 1; i < 10; i++)
            {
                var cellsWithValue = unit.Cells.Where(c => c.PotentialValues.Contains(i)).ToList();
                var pointingLines = GetPointingLines(cellsWithValue);
                if (pointingLines != null)
                {
                    foreach (var cell in pointingLines.Cells.Where(c => !unit.Cells.Contains(c) && c.PotentialValues.Contains(i)))
                    {
                        cell.RemovePotentialValue(i);
                        Console.WriteLine("(Pointing Reduction)({2})[{3}] Removed value: {0} from cell: {1}", i,
                            cell.Name, unit.Name, pointingLines.Name);
                        output = true;
                    }
                }
            }

            return output;
        }

        private Unit GetPointingLines(IReadOnlyCollection<Cell> cells)
        {
            var rowsWithCells = Board.Rows.Where(r => r.Cells.Intersect(cells).Any()).ToList();
            if (rowsWithCells.Count() == 1)
            {
                return rowsWithCells.First();
            }

            var columnWithCells = Board.Columns.Where(c => c.Cells.Intersect(cells).Any()).ToList();
            if (columnWithCells.Count() == 1)
            {
                return columnWithCells.First();
            }
            return null;
        }
    }
}