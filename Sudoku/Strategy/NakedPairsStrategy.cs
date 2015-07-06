using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.Strategy
{
    public class NakedPairsStrategy : BaseSudokuStrategy
    {
        public NakedPairsStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (NakedPairs(box))
                {
                    output = true;
                }
            }

            foreach (var column in Board.Columns)
            {
                if (NakedPairs(column))
                {
                    output = true;
                }
            }

            foreach (var row in Board.Rows)
            {
                if (NakedPairs(row))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool NakedPairs(Unit unit)
        {
            var output = false;
            var nakedPairCells = GetNakedPairs(unit);

            if (nakedPairCells.Count == 2)
            {
                var values = Logic.ExtractValues(nakedPairCells);
                foreach (var cell in unit.Cells.Where(cell => !nakedPairCells.Contains(cell)))
                {
                    foreach (var value in values.Where(value => cell.PotentialValues.Contains(value)))
                    {
                        cell.RemovePotentialValue(value);
                        Console.WriteLine("(Naked Pairs)({2}) Removed value: {0} from cell: {1}", value, cell.Name, unit.Name);
                        output = true;
                    }
                }
            }

            return output;
        }

        private IReadOnlyCollection<Cell> GetNakedPairs(Unit unit)
        {
            var list = new List<Cell>();

            foreach (var cell in unit.Cells)
            {
                if (cell.PotentialValues.Count == 2  && !list.Contains(cell))
                {
                    var potentialNakedPairCells =
                        unit.Cells.Where(
                            c =>
                                c != cell &&
                                c.PotentialValues.OrderBy(i => i).SequenceEqual(cell.PotentialValues.OrderBy(i => i))).ToList();
                    if (potentialNakedPairCells.Any())
                    {
                        list.Add(cell);
                        list.Add(potentialNakedPairCells.Single());
                    }
                }
            }

            return new ReadOnlyCollection<Cell>(list);
        }
    }
}