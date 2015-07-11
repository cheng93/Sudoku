using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Strategy
{
    public class XWingStrategy : BaseSudokuStrategy
    {
        public XWingStrategy(Board board)
            : base(board)
        {
        }

        public override bool Run()
        {
            var output = false;
            for (int i= 1; i < 10; i++)
            {
                if (XWing(i))
                {
                    output = true;
                }
            }
            return output;
        }

        private bool XWing(int value)
        {
            var output = false;
            var potentialXWingUnit = new List<Unit>();

            foreach (var column in Board.Columns)
            {
                if (column.Cells.Count(c => c.PotentialValues.Contains(value)) == 2)
                {
                    potentialXWingUnit.Add(column);
                }
            }
            return output;
        }
    }
}