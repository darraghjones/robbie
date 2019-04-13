using System;
using System.Linq;
using Lib.Domain;
using Lib.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HumanDna()
        {
            Console.WriteLine(HumanCleaner.Dna);
        }

        [TestMethod]
        public void HumanScore()
        {
            var c = new HumanCleaner();
            var s = new Simulator(c);
            var score = s.AverageScore();
            Console.WriteLine(score);
            Assert.IsTrue(score > 400, score.ToString());
        }

        [TestMethod]
        public void RandomMoveScore()
        {
            var c = new RandomCleaner();
            var s = new Simulator(c);
            var score = s.AverageScore();
            Console.WriteLine(score);
            Assert.IsTrue(score < 400, score.ToString());
        }

        [TestMethod]
        public void HumanDnaScore()
        {
            var c = new DnaCleaner(HumanCleaner.Dna);
            var s = new Simulator(c);
            var score = s.AverageScore();
            Console.WriteLine(score);
            Assert.IsTrue(score > 400, score.ToString());
        }

        [TestMethod]
        public void RandomDnaScore()
        {
            var c = new DnaCleaner();
            var s = new Simulator(c);
            var score = s.AverageScore();
            Console.WriteLine(score);
            Assert.IsTrue(score < 400, score.ToString());
        }

        [TestMethod]
        [DataRow("SPXWPPEPXEPPWPNEPWNPEWPXSPNSPEWPREPSEPPRPPERRRPWNWWWXNEPEEPWNRNEPEEPWNEWNPNWNRPPWNPENPEEPWNPXRPPEPWNPENPXSPXSNXRNXRSNNPNNPXEPXNPWWRXRXSNPPENNNPWNPWEREEPRNPXXRWSSEWPWWPPSPEWPXWPXESEWPEWPSXRSSPRWPESPEEPESRRSRRRSSRSPXRERSSEEXSWWNXSEENNRPPXWRSNSWS")]
        public void TestDna(string dnaString)
        {
            var dna = new Dna(dnaString.Select(x => (Move)x).ToArray());
            var s = new Simulator(new DnaCleaner(dna));
            var score = s.AverageScore();
            Console.WriteLine(score);
            Assert.IsTrue(score > 450, score.ToString());
        }
    }
}
