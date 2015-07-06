namespace Sudoku.Factory
{
    public class RowFactory : UnitFactory
    {
        public override Unit Factory(Cell[,] cells, int modifier)
        {
            var rowCells = new Cell[9];

            for (int i = 0; i < 9; i++)
            {
                rowCells[i] = cells[modifier, i];
            }

            return new Unit(rowCells, "Row" + (modifier + 1));
        }
    }
}