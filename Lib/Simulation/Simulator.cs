using System;
using Lib.Domain;
using Random = Lib.Domain.Random;
using System.Linq;

namespace Lib.Simulation
{
    public class Simulator
    {
        private readonly Cell[,] board = new Cell[12, 12];
        private readonly ICleaner cleaner;
        private int score;
        private int x;
        private int y;

        public Simulator(ICleaner cleaner)
        {
            this.cleaner = cleaner;
        }

        private void Reset()
        {
            score = 0;
            x = Random.Next(10) + 1;
            y = Random.Next(10) + 1;
            for (var i = 0; i < 12; i++)
            {
                for (var j = 0; j < 12; j++)
                {
                    if (i == 0 || i == 11 || j == 0 || j == 11) board[i, j] = Cell.Wall;
                    else board[i, j] = Random.NextDouble() < 0.5 ? Cell.Can : Cell.Empty;
                }
            }
        }

        public int Simulate(int numMoves = 200)
        {
            Reset();
            numMoves.Times(MakeMove);
            return score;
        }

        public double AverageScore(int testRuns = 100)
        {
            return testRuns.Times(_ => Simulate()).Average();
        }

        private void MakeMove()
        {
            var neighourhood = new Neighbourhood(board[x, y + 1], board[x, y - 1], board[x + 1, y], board[x - 1, y], board[x, y]);
            void MakeMove(Move move)
            {
                switch (move)
                {
                    case Move.North:
                        if (neighourhood.North == Cell.Wall) score -= 5; else y += 1;
                        return;
                    case Move.East:
                        if (neighourhood.East == Cell.Wall) score -= 5; else x += 1;
                        return;
                    case Move.South:
                        if (neighourhood.South == Cell.Wall) score -= 5; else y -= 1;
                        return;
                    case Move.West:
                        if (neighourhood.West == Cell.Wall) score -= 5; else x -= 1;
                        return;
                    case Move.Pickup:
                        if (neighourhood.Current == Cell.Can) score += 10; else score -= 1;
                        board[x, y] = Cell.Empty;
                        return;
                    case Move.Stay:
                        return;
                    case Move.Random:
                        MakeMove(MoveHelpers.GetRandomDirection());
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(move), move, null);
                }
            }
            MakeMove(cleaner.Move(neighourhood));
        }
    }
}
