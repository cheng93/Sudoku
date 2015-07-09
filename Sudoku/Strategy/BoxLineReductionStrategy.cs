using System;
using System.Collections.Generic;
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
            var values = new List<int>();
            foreach (var column in Board.Columns)
            {
                if (column.Cells.Intersect(box.Cells).Any())
                {
                    values.AddRange(GetBoxLineReductionValues(column));
                }
            }
            return output;
        }

        private IReadOnlyCollection<int> GetBoxLineReductionValues(Unit column)
        {
            var output = new List<int>();

            return output;
        }
    }
}
