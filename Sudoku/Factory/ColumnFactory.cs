namespace Sudoku.Factory
{
    public class ColumnFactory : UnitFactory
    {
        public override Unit Factory(Cell[,] cells, int modifier)
        {
            var columnCells = new Cell[9];

            for (int i = 0; i < 9; i++)
            {
                columnCells[i] = cells[i, modifier];
            }

            return new Unit(columnCells, "Column" + (modifier + 1));
        }
    }
}