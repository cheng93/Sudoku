namespace Sudoku
{
    public class Unit
    {
        private readonly Cell[] _cells;

        public Cell[] Cells { get { return _cells; } }

        public string Name { get; private set; }

        public Unit(Cell[] cells, string name = null)
        {
            _cells = cells;
            Name = name;
        }
    }
}