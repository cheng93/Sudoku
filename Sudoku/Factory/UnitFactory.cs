namespace Sudoku.Factory
{
    public abstract class UnitFactory
    {
        public abstract Unit Factory(Cell[,] cells, int modifier);
    }
}