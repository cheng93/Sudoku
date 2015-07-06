namespace Sudoku.Factory
{
    public class BoxFactory : UnitFactory
    {
        public override Unit Factory(Cell[,] cells, int modifier)
        {
            var boxCells = new Cell[9];
            var modulus = modifier % 3;
            var count = 0;

            for (int i = modifier - modulus; i < modifier - modulus + 3; i++)
            {
                for (int j = 3 * modulus; j < 3 * modulus + 3; j++)
                {
                    boxCells[count] = cells[i, j];
                    count++;
                }
            }

            return new Unit(boxCells, "Box" + (modifier + 1));
        }
    }
}