using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.Strategy
{
    public class HiddenTriplesStrategy : BaseSudokuStrategy
    {
        public HiddenTriplesStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (HiddenTriples(box))
                {
                    output = true;
                }
            }

            foreach (var column in Board.Columns)
            {
                if (HiddenTriples(column))
                {
                    output = true;
                }
            }

            foreach (var row in Board.Rows)
            {
                if (HiddenTriples(row))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool HiddenTriples(Unit unit)
        {
            var output = false;
            var hiddenTripleCellValues = GetHiddenTripleValues(unit);
            if (hiddenTripleCellValues.Count > 2)
            {
                foreach (var cell in unit.Cells.Where(c => c.PotentialValues.Intersect(hiddenTripleCellValues).Any()))
                {
                    foreach (var value in cell.PotentialValues.ToList())
                    {
                        if (!hiddenTripleCellValues.Contains(value))
                        {
                            cell.RemovePotentialValue(value);
                            Console.WriteLine("(Hidden Triples)({2}) Removed value: {0} from cell: {1}", value, cell.Name, unit.Name);
                            output = true;
                        }
                    }
                }
            }

            return output;
        }

        private IReadOnlyCollection<int> GetHiddenTripleValues(Unit unit)
        {
            var list = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                var count = unit.Cells.Count(c => c.PotentialValues.Contains(i));
                if (count > 0 && count <= 3)
                {
                    list.Add(i);
                }
            }

            if (list.Count > 2)
            {
                foreach (var cell in unit.Cells)
                {
                    var potentialHiddenTripleValues = cell.PotentialValues.Intersect(list).ToList();
                    if (potentialHiddenTripleValues.Any())
                    {
                        var potentialHiddenTriple =
                            unit.Cells.Where(
                                c => c != cell && c.PotentialValues.Intersect(potentialHiddenTripleValues).Any())
                                .ToArray();
                        if (potentialHiddenTriple.Count() != 2)
                        {
                            list.RemoveAll(l => potentialHiddenTripleValues.Contains(l));
                        }
                        else
                        {
                            if (
                                !potentialHiddenTriple[0].PotentialValues.Intersect(list)
                                    .Intersect(potentialHiddenTriple[1].PotentialValues)
                                    .Any())
                            {
                                list.RemoveAll(l => potentialHiddenTripleValues.Contains(l));
                            }
                        }
                    }
                }
            }

            return new ReadOnlyCollection<int>(list);
        }
    }
}