using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku
{
    public static class Logic
    {
        public static IReadOnlyCollection<int> ExtractValues(IReadOnlyCollection<Cell> cells)
        {
            var output = cells.SelectMany(cell => cell.PotentialValues).Distinct().ToList();

            return new ReadOnlyCollection<int>(output);
        }

        public static Unit GetBox(Board board, Cell cell)
        {
            return board.Boxes.First(b => b.Cells.Contains(cell));
        }

        public static Unit GetColumn(Board board, Cell cell)
        {
            return board.Columns.First(c => c.Cells.Contains(cell));
        }

        public static Unit GetRow(Board board, Cell cell)
        {
            return board.Rows.First(r => r.Cells.Contains(cell));
        }

        public static IReadOnlyCollection<Cell> GetAssociatedCells(Board board, Cell cell)
        {
            var output = new List<Cell>();

            output.AddRange(GetBox(board, cell).Cells);
            output.AddRange(GetColumn(board, cell).Cells);
            output.AddRange(GetRow(board, cell).Cells);
            output.RemoveAll(c => c == cell);

            return output.Distinct().ToList();
        }

        public static IReadOnlyCollection<Cell> GetIntersectingCells(Board board, Cell cellA, Cell cellB)
        {
            var associatedA = GetAssociatedCells(board, cellA);
            var asoociatedB = GetAssociatedCells(board, cellB);

            return associatedA.Intersect(asoociatedB).ToList();
        }
    }
}