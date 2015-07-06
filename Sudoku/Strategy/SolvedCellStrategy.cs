using System;
using System.Linq;

namespace Sudoku.Strategy
{
    public class SolvedCellStrategy : BaseSudokuStrategy
    {
        public SolvedCellStrategy(Board board) : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;
            foreach (var cell in Board.Cells)
            {
                if (cell.PotentialValues.Count == 1)
                {
                    cell.Value = cell.PotentialValues.First();
                    Console.WriteLine("Solved value {0} in cell: {1}", cell.Value, cell.Name);
                    output = true;
                }
            }
            return output;
        }
    }
}
