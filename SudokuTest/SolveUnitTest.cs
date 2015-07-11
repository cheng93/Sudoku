using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;

namespace SudokuTest
{
    [TestClass]
    public class SolveUnitTest
    {

        [TestMethod]
        public void TestSolvedPropertyTrue()
        {
            var board = new Board("246975138589316274371248695498621753132754986657839421724183569865492317913567842");
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestSolvedPropertyFalse()
        {
            var board = new Board("246975138589316274371248695498621753132754986657839421724183569865492317913567844");
            Assert.AreEqual(false, board.Solved);
        }

        [TestMethod]
        public void TestSolvedPropertyEmptyCell()
        {
            var board = new Board("2469751.8589316274371248695498621753132754986657839421724183569865492317913567842");
            Assert.AreEqual(false, board.Solved);
        }

        [TestMethod]
        public void TestEasiestSudoku()
        {
            var board = new Board("...1.5...14....67..8...24...63.7..1.9.......3.1..9.52...72...8..26....35...4.9...");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestHiddenSingle()
        {
            var board = new Board("200070038000006070300040600008020700100000006007030400004080009060400000910060002");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestNakedPairs()
        {
            var board = new Board("4.....938.32.941...953..24.37.6.9..4529..16736.47.3.9.957..83....39..4..24..3.7.9");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestNakedTriples()
        {
            var board = new Board("294513..66..8423193..697254....56....4..8..6....47....73.164..59..735..14..928637");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestHiddenPairs()
        {
            var board = new Board("000000000904607000076804100309701080008000300050308702007502610000403208000000000");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestHiddenTriples()
        {
            var board = new Board("000001030231090000065003100678924300103050006000136700009360570006019843300000000");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestNakedQuads()
        {
            var board = new Board("000030086000020040090078520371856294900142375400397618200703859039205467700904132");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestPointingLineReduction()
        {
            var board = new Board("930050000200630095856002000003180570005020980080005000000800159508210004000560008");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestBoxLineReduction()
        {
            var board = new Board("020943715904000600750000040500480000200000453400352000042000081005004260090208504");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }

        [TestMethod]
        public void TestYWing()
        {

            var board = new Board("900240000050690231020050090090700320002935607070002900069020073510079062207086009");
            board.Solve();
            Assert.AreEqual(true, board.Solved);
        }
    }
}
