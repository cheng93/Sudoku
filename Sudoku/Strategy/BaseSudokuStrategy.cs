namespace Sudoku.Strategy
{
    public abstract class BaseSudokuStrategy : ISudokuStrategy
    {
        private readonly Board _board;

        public Board Board { get { return _board; } }

        protected BaseSudokuStrategy(Board board)
        {
            _board = board;
        }

        public abstract bool Run();
    }
}
