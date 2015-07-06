using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.Strategy
{
    public class NakedTriplesStrategy : BaseSudokuStrategy
    {
        public NakedTriplesStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (NakedTriples(box))
                {
                    output = true;
                }
            }

            foreach (var column in Board.Columns)
            {
                if (NakedTriples(column))
                {
                    output = true;
                }
            }

            foreach (var row in Board.Rows)
            {
                if (NakedTriples(row))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool NakedTriples(Unit unit)
        {
            var output = false;
            var nakedTripleCells = GetNakedTriples(unit);

            if (nakedTripleCells.Count == 3)
            {
                var values = Logic.ExtractValues(nakedTripleCells);
                foreach (var cell in unit.Cells.Where(cell => !nakedTripleCells.Contains(cell)))
                {
                    foreach (var value in values.Where(value => cell.PotentialValues.Contains(value)))
                    {
                        cell.RemovePotentialValue(value);
                        Console.WriteLine("(Naked Triples)({2}) Removed value: {0} from cell: {1}", value, cell.Name, unit.Name);
                        output = true;
                    }
                }
            }

            return output;
        }

        public IReadOnlyCollection<Cell> GetNakedTriples(Unit unit)
        {
            var list = new List<Cell>();

            foreach (var cell in unit.Cells)
            {
                if (cell.PotentialValues.Count <= 3 && !list.Contains(cell))
                {
                    var potentialTripleCells = new List<Cell> { cell };
                    var values = potentialTripleCells.SelectMany(c => c.PotentialValues).Distinct();
                    foreach (var otherCell in unit.Cells.Where(c => c != cell && c.PotentialValues.Count <= 3))
                    {

                        var intersectValues = values.Intersect(otherCell.PotentialValues);
                        if (intersectValues.Any())
                        {
                            if (values.Count() > 2)
                            {
                                if (!otherCell.PotentialValues.Except(values).Any())
                                {
                                    potentialTripleCells.Add(otherCell);
                                }
                            }
                            else
                            {
                                potentialTripleCells.Add(otherCell);
                            }
                        }
                    }
                    if (values.Count() == 3 && potentialTripleCells.Count == 3)
                    {
                        list.AddRange(potentialTripleCells);
                    }
                }
            }

            return new ReadOnlyCollection<Cell>(list);
        }
    }
}