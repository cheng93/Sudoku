using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Strategy
{
    public class YWingStrategy : BaseSudokuStrategy
    {
        public YWingStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;
            foreach (var cell in Board.Cells)
            {
                if (YWing(cell))
                {
                    output = true;
                }
            }
            return output;
        }

        private bool YWing(Cell cell)
        {
            var output = false;
            if (cell.PotentialValues.Count == 2)
            {
                var allPincerCells = GetYWing(cell);
                foreach (var pincerCells in allPincerCells)
                {
                    var cellA = pincerCells.Item1;
                    var cellB = pincerCells.Item2;
                    var value = cellA.PotentialValues.Intersect(cellB.PotentialValues).First();
                    var intersectingCells = Logic.GetIntersectingCells(Board, cellA, cellB);
                    foreach (var intersect in intersectingCells.Where(c => c.PotentialValues.Contains(value)))
                    {
                        intersect.RemovePotentialValue(value);
                        Console.WriteLine("(YWing)({2})[{3}, {4}] Removed value: {0} from cell: {1}", value, intersect.Name, cell.Name, cellA.Name, cellB.Name);
                        output = true;
                    }
                }
            }
            return output;
        }

        private IReadOnlyCollection<Tuple<Cell, Cell>> GetYWing(Cell cell)
        {
            var output = new List<Tuple<Cell, Cell>>();

            var box = Logic.GetBox(Board, cell);
            var column = Logic.GetColumn(Board, cell);
            var row = Logic.GetRow(Board, cell);

            foreach (var boxCell in box.Cells.Where(c => c.PotentialValues.Count == 2))
            {
                var intersect = boxCell.PotentialValues.Intersect(cell.PotentialValues).ToList();
                if (intersect.Count() == 1)
                {
                    var pincerPotentialValue = boxCell.PotentialValues.First(v => !intersect.Contains(v));
                    var pivotPotentialValueA = intersect.First();
                    var pivotPotentialValueB = cell.PotentialValues.First(v => v != pivotPotentialValueA);
                    foreach (var columnCell in column.Cells.Where(c => c.PotentialValues.Count == 2))
                    {
                        if (columnCell.PotentialValues.Contains(pincerPotentialValue) &&
                            columnCell.PotentialValues.Contains(pivotPotentialValueB))
                        {
                            output.Add(new Tuple<Cell,Cell>(boxCell, columnCell));
                        }
                    }
                    foreach (var rowCell in row.Cells.Where(c => c.PotentialValues.Count == 2))
                    {
                        if (rowCell.PotentialValues.Contains(pincerPotentialValue) &&
                            rowCell.PotentialValues.Contains(pivotPotentialValueB))
                        {
                            output.Add(new Tuple<Cell, Cell>(boxCell, rowCell));
                        }
                    }
                }
            }
            foreach (var columnCell in column.Cells.Where(c => c.PotentialValues.Count == 2))
            {
                var intersect = columnCell.PotentialValues.Intersect(cell.PotentialValues).ToList();
                if (intersect.Count() == 1)
                {
                    var pincerPotentialValue = columnCell.PotentialValues.First(v => !intersect.Contains(v));
                    var pivotPotentialValueA = intersect.First();
                    var pivotPotentialValueB = cell.PotentialValues.First(v => v != pivotPotentialValueA);
                  
                    foreach (var rowCell in row.Cells.Where(c => c.PotentialValues.Count == 2))
                    {
                        if (rowCell.PotentialValues.Contains(pincerPotentialValue) &&
                            rowCell.PotentialValues.Contains(pivotPotentialValueB))
                        {
                            output.Add(new Tuple<Cell, Cell>(columnCell, rowCell));
                        }
                    }
                }
            }

            return output;
        }
    }
}