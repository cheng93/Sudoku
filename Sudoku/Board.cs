using System;
using System.Linq;
using Sudoku.Factory;
using Sudoku.Strategy;

namespace Sudoku
{
    public class Board
    {
        private readonly Cell[,] _cells = new Cell[9, 9];

        private readonly Unit[] _boxes = new Unit[9];

        private readonly Unit[] _columns = new Unit[9];

        private readonly Unit[] _rows = new Unit[9];

        public Unit[] Boxes
        {
            get { return _boxes; }
        }

        public Cell[,] Cells
        {
            get { return _cells; }
        }

        public Unit[] Columns
        {
            get { return _columns; }
        }

        public Unit[] Rows
        {
            get { return _rows; }
        }

        public bool Solved
        {
            get
            {
                foreach (var cell in _cells)
                {
                    if (!cell.HasValue)
                        return false;
                }
                return _boxes.All(x => x.Cells.Select(c => c.Value).Distinct().Count() == 9) &&
                       _columns.All(x => x.Cells.Select(c => c.Value).Distinct().Count() == 9) &&
                       _rows.All(x => x.Cells.Select(c => c.Value).Distinct().Count() == 9);
            }
        }

        public Board(string input)
        {
            Init(input);
        }

        private void Init(string input)
        {
            CreateCells(input);
            CreateUnits();
        }

        private void CreateCells(string input)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var cell = new Cell(input.Substring(i * 9 + j, 1), string.Format("r{0}c{1}", i + 1, j + 1));
                    _cells[i, j] = cell;
                }
            }
        }

        private void CreateUnits()
        {
            var boxFactory = new BoxFactory();
            var columnFactory = new ColumnFactory();
            var rowFactory = new RowFactory();
            for (int i = 0; i < 9; i++)
            {
                _boxes[i] = boxFactory.Factory(_cells, i);
                _columns[i] = columnFactory.Factory(_cells, i);
                _rows[i] = rowFactory.Factory(_cells, i);
            }
        }

        public void Display(bool current = true)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    string character;
                    switch (j)
                    {
                        case 2:
                        case 5:
                            character = "|";
                            break;

                        case 8:
                            character = Environment.NewLine;
                            break;

                        default:
                            character = " ";
                            break;
                    }
                    int? cell = null;
                    if (current)
                    {
                        if (_cells[i, j].HasValue)
                        {
                            cell = _cells[i, j].Value;
                        }
                    }
                    else
                    {
                        if (_cells[i, j].DefaultValue != null)
                        {
                            cell = _cells[i, j].DefaultValue;
                        }
                    }
                    Console.Write("{0}{1}", cell.HasValue ? cell.Value.ToString() : ".", character);
                }

                if (i == 2 || i == 5)
                {
                    var dashes = new string('-', 5);
                    Console.WriteLine("{0}|{0}|{0}", dashes);
                }
            }
            Console.WriteLine();
        }

        public void Solve()
        {
            var boardChanged = false;
            var iterations = 0;
            do
            {
                Console.WriteLine();
                Display();
                iterations++;
                boardChanged = false;

                if (Solve(new RemoveImpossibleStrategy(this)))
                {
                    boardChanged = true;
                }
                if (Solve(new SolvedCellStrategy(this)))
                {
                    boardChanged = true;
                }
                else if (Solve(new HiddenSinglesStrategy(this)))
                {
                    boardChanged = true;
                }
                else if (Solve(new NakedPairsStrategy(this)))
                {
                    boardChanged = true;
                }
                else if (Solve(new NakedTriplesStrategy(this)))
                {
                    boardChanged = true;
                }
                else if (Solve(new HiddenPairsStrategy(this)))
                {
                    boardChanged = true;
                }
                else if (Solve(new HiddenTriplesStrategy(this)))
                {
                    boardChanged = true;
                }
                else if (Solve(new NakedQuadsStrategy(this)))
                {
                    boardChanged = true;
                }
                else if (Solve(new PointingReductionStrategy(this)))
                {
                    boardChanged = true;
                }

            } while (boardChanged);

            if (Solved)
            {
                Console.WriteLine("Puzzle was solved. {0} iterations.", iterations);
            }
            else
            {
                Console.WriteLine("Puzzle not solved, error occured.");
            }
        }

        private bool Solve(ISudokuStrategy strategy)
        {
            return strategy.Run();
        }
    }
}