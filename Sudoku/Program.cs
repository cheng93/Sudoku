using System;

namespace Sudoku
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var board = new Board("930050000200630095856002000003180570005020980080005000000800159508210004000560008");
            board.Solve();
            Console.ReadLine();
        }
    }
}