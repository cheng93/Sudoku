using System;
using Sudoku;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board("900240000050690231020050090090700320002935607070002900069020073510079062207086009");
            board.Solve();
            Console.ReadLine();
        }
    }
}
