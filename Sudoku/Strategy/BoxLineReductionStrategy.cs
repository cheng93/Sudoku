using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Strategy
{
    class BoxLineReductionStrategy : BaseSudokuStrategy
    {
        public BoxLineReductionStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;

            foreach (var box in Board.Boxes)
            {
                if (BoxLineReduction(box))
                {
                    output = true;
                }
            }

            return output;
        }

        private bool BoxLineReduction(Unit box)
        {
            var output = false;
            foreach (var column in Board.Columns)
            {
                var intersect = column.Cells.Intersect(box.Cells).ToList();
                if (intersect.Any())
                {
                    var values = GetBoxLineReductionValues(intersect, column);
                    foreach (var value in values)
                    {
                        foreach (var cell in box.Cells.Where(c => !intersect.Contains(c)))
                        {
                            cell.RemovePotentialValue(value);
                            Console.WriteLine("(BoxLine Reduction)({2})[{3}] Removed value: {0} from cell: {1}", value,
                                cell.Name, box.Name, column.Name);
                            output = true;
                        }
                    }
                }
            }
            foreach (var row in Board.Columns)
            {
                var intersect = row.Cells.Intersect(box.Cells).ToList();
                if (intersect.Any())
                {
                    var values = GetBoxLineReductionValues(intersect, row);
                    foreach (var value in values)
                    {
                        foreach (var cell in box.Cells.Where(c => !intersect.Contains(c)))
                        {
                            cell.RemovePotentialValue(value);
                            Console.WriteLine("(BoxLine Reduction)({2})[{3}] Removed value: {0} from cell: {1}", value,
                                cell.Name, box.Name, row.Name);
                            output = true;
                        }
                    }
                }
            }
            return output;
        }

        private IReadOnlyCollection<int> GetBoxLineReductionValues(List<Cell> intersect, Unit columnOrRow)
        {
            var output = new List<int>();

            for (int i = 1; i < 10; i++)
            {
                var intersectCount = intersect.Count(c => c.PotentialValues.Contains(i));
                var unitCount = intersect.Count(c => c.PotentialValues.Contains(i));
                if (intersectCount == unitCount && intersectCount > 0)
                {
                    output.Add(i);
                }
            }
            return output;
        }
    }
}
