using System;
using Sudoku;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board("016007803090800000870001260048000300650009082039000650060900020080002936924600510");
            board.Solve();
            Console.ReadLine();
        }
    }
}
