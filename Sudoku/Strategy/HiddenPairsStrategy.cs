using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.Strategy
{
    public class HiddenPairsStrategy : BaseSudokuStrategy
    {
        public HiddenPairsStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (HiddenPairs(box))
                {
                    output = true;
                }
            }

            foreach (var column in Board.Columns)
            {
                if (HiddenPairs(column))
                {
                    output = true;
                }
            }

            foreach (var row in Board.Rows)
            {
                if (HiddenPairs(row))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool HiddenPairs(Unit unit)
        {
            var output = false;
            var hiddenPairCellValues = GetHiddenPairValues(unit);
            if (hiddenPairCellValues.Count > 1)
            {
                foreach (var cell in unit.Cells.Where(c => c.PotentialValues.Intersect(hiddenPairCellValues).Any()))
                {
                    foreach (var value in cell.PotentialValues.ToList())
                    {
                        if (!hiddenPairCellValues.Contains(value))
                        {
                            cell.RemovePotentialValue(value);
                            Console.WriteLine("(Hidden Pairs)({2}) Removed value: {0} from cell: {1}", value, cell.Name, unit.Name);
                            output = true;
                        }
                    }
                }
            }

            return output;
        }

        private IReadOnlyCollection<int> GetHiddenPairValues(Unit unit)
        {
            var list = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                if (unit.Cells.Count(c => c.PotentialValues.Contains(i)) == 2)
                {
                    list.Add(i);
                }
            }
            foreach (int value in list.ToList())
            {
                var potentialHiddenPairs = unit.Cells.Where(c => c.PotentialValues.Contains(value)).ToArray();
                if (
                    potentialHiddenPairs[0].PotentialValues.Intersect(potentialHiddenPairs[1].PotentialValues)
                        .Intersect(list)
                        .Count() != 2)
                {
                    list.Remove(value);
                }
            }

            return new ReadOnlyCollection<int>(list);
        }
    }
}