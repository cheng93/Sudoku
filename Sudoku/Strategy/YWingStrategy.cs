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
                var x = cell.PotentialValues;
                var y = cell.PotentialValues;
            }
            return output;
        }
    }
}