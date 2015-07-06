using System.Linq;

namespace Sudoku.Strategy
{
    public class RemoveImpossibleStrategy : BaseSudokuStrategy
    {
        public RemoveImpossibleStrategy(Board board) 
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;
            foreach (var box in Board.Boxes)
            {
                if (RemoveImpossible(box))
                {
                    output = true;
                }
            }

            foreach (var column in Board.Columns)
            {
                if (RemoveImpossible(column))
                {
                    output = true;
                }
            }

            foreach (var row in Board.Rows)
            {
                if (RemoveImpossible(row))
                {
                    output = true;
                }
            }
            return output;
        }

        private bool RemoveImpossible(Unit unit)
        {
            var output = false;

            foreach (var cell in unit.Cells)
            {
                if (cell.HasValue)
                {
                    foreach (var emptyCell in unit.Cells.Where(c => c != cell))
                    {
                        if (emptyCell.PotentialValues.Contains(cell.Value.Value))
                        {
                            emptyCell.RemovePotentialValue(cell.Value.Value);
                            output = true;
                        }
                    }
                }
            }
            return output;
        }
    }
}
