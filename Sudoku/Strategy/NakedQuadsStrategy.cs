using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.Strategy
{
    public class NakedQuadsStrategy : BaseSudokuStrategy
    {
        public NakedQuadsStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (NakedQuads(box))
                {
                    output = true;
                }
            }

            foreach (var column in Board.Columns)
            {
                if (NakedQuads(column))
                {
                    output = true;
                }
            }

            foreach (var row in Board.Rows)
            {
                if (NakedQuads(row))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool NakedQuads(Unit unit)
        {
            var output = false;
            var nakedQuadsCells = GetNakedQuads(unit);

            if (nakedQuadsCells.Count == 4)
            {
                var values = Logic.ExtractValues(nakedQuadsCells);
                foreach (var cell in unit.Cells.Where(cell => !nakedQuadsCells.Contains(cell)))
                {
                    foreach (var value in values.Where(value => cell.PotentialValues.Contains(value)))
                    {
                        cell.RemovePotentialValue(value);
                        Console.WriteLine("(Naked Quads)({2}) Removed value: {0} from cell: {1}", value, cell.Name, unit.Name);
                        output = true;
                    }
                }
            }

            return output;
        }

        private IReadOnlyCollection<Cell> GetNakedQuads(Unit unit)
        {
            var list = new List<Cell>();

            foreach (var cell in unit.Cells)
            {
                if (cell.PotentialValues.Count <= 4 && !list.Contains(cell))
                {
                    var potentialQuadCells = new List<Cell> { cell };
                    var values = potentialQuadCells.SelectMany(c => c.PotentialValues).Distinct();
                    foreach (var otherCell in unit.Cells.Where(c => c != cell && c.PotentialValues.Count <= 4))
                    {

                        var intersectValues = values.Intersect(otherCell.PotentialValues);
                        if (intersectValues.Any())
                        {
                            if (values.Count() > 3)
                            {
                                if (!otherCell.PotentialValues.Except(values).Any())
                                {
                                    potentialQuadCells.Add(otherCell);
                                }
                            }
                            else
                            {
                                potentialQuadCells.Add(otherCell);
                            }
                        }
                    }
                    if (values.Count() == 4 && potentialQuadCells.Count == 4)
                    {
                        list.AddRange(potentialQuadCells);
                    }
                }
            }

            return new ReadOnlyCollection<Cell>(list);
        }
    }
}