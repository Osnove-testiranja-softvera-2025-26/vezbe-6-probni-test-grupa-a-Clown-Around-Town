using NUnit.Framework;
using OTS2026_ProbniTest.Exceptions;
using OTS2026_ProbniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_ProbniTest.Test
{
    [TestFixture]
    public class GameTest
    {
        [TestCase(0, 0,30,30)]
        [TestCase(1,29,29, 1)]
        public void TestStartConditions_Success(int px, int py, int tx, int ty) {
            Position playerPos = new Position(px,py);
            Position targetPos = new Position(tx,ty);
            Game newGame = new Game(targetPos, playerPos);

            Assert.That(newGame.Player.Position.X, Is.EqualTo(playerPos.X));
            Assert.That(newGame.Player.Position.Y, Is.EqualTo(playerPos.Y));
            Assert.That(newGame.Target.X, Is.EqualTo(targetPos.X));
            Assert.That(newGame.Target.Y, Is.EqualTo(targetPos.Y));
        }

        [TestCase(15,ExpectedResult = 30)]
        public int TestMaxMoves_Success(int moveCount)
        {
            Position playerPos = new Position(10, 10);
            Position targetPos = new Position(20, 20);
            Game newGame = new Game(targetPos, playerPos);
            for (int i = 0; i < moveCount; i++) {
                newGame.MoveUp();
                newGame.MoveDown();
            }
            return newGame.NumberOfMoves;
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void TestMovingOutOfBounds_Success(int move)
        {
            Position targetPos = new Position(20, 20);
            Position playerPos = new Position(0, 0);
            Game newGame;
            if (move == 1)
            {
                playerPos = new Position(0, 0);
                newGame = new Game(targetPos, playerPos);
                newGame.MoveUp();
            }
            else if (move == 2)
            {
                playerPos = new Position(0, 0);
                newGame = new Game(targetPos, playerPos);
                newGame.MoveLeft();
            }
            else if (move == 3)
            {
                playerPos = new Position(30, 30);
                newGame = new Game(targetPos, playerPos);
                newGame.MoveDown();
            }
            else if (move == 4)
            {
                playerPos = new Position(30, 30);
                newGame = new Game(targetPos, playerPos);
                newGame.MoveRight();
            }
            else {
                playerPos = new Position(30, 30);
                newGame = new Game(targetPos, playerPos);
            }
            Assert.That(newGame.Player.Position.X, Is.EqualTo(playerPos.X));
            Assert.That(newGame.Player.Position.Y, Is.EqualTo(playerPos.Y));
        }

        [Test]
        public void TestCoinCollection()
        {
            Position playerPos = new Position(10, 10);
            Position targetPos = new Position(10, 15);
            Game newGame = new Game(targetPos, playerPos);
            newGame.Player.NumberOfCoins = 0;
            newGame.Map.AddTile(TileContent.Coin, 10, 11);
            newGame.Map.AddTile(TileContent.Coin, 10, 12);
            newGame.Map.AddTile(TileContent.Coin, 10, 13);
            newGame.Map.AddTile(TileContent.Coin, 10, 14);
            newGame.MoveDown();
            newGame.MoveDown();
            newGame.MoveDown();
            newGame.MoveDown();
            Assert.That(newGame.Player.NumberOfCoins, Is.EqualTo(4));
        }
        /*
        [TestCase(-1, -1, 30, 30)]
        [TestCase(1, 29, 31, 1)]
        public void TestStartConditions_Failure(int px, int py, int tx, int ty)
        {
            Position playerPos = new Position(px, py);
            Position targetPos = new Position(tx, ty);
            Game newGame;
            //Exception ex = Assert.Throws<Exception>(() => newGame = new Game(targetPos, playerPos));
            //Assert.That(ex, Is.TypeOf(InvalidPositionException));
            Assert.Throws<Exception>(() => newGame = new Game(targetPos, playerPos));
        }*/

        [TestCase(5, 4, 15, 15, Rating.Good)]
        [TestCase(9, 5, 10, 10, Rating.Good)]
        [TestCase(11, 4, 10, 10, Rating.Excellent)]
        [TestCase(10, 4, 15, 15, Rating.Good)]
        [TestCase(5, 6, 10, 10, Rating.Good)]
        [TestCase(10, 5, 15, 15, Rating.Excellent)]
        [TestCase(5, 5, 15, 15, Rating.Excellent)]
        [TestCase(4, 5, 10, 10, Rating.Bad)]
        [TestCase(4, 5, 15, 15, Rating.Excellent)]
        [TestCase(4, 6, 10, 10, Rating.Good)]
        [TestCase(9, 4, 10, 10, Rating.Bad)]
        [TestCase(9, 6, 10, 10, Rating.Good)]
        [TestCase(11, 4, 15, 15, Rating.Good)]
        [TestCase(10, 6, 10, 10, Rating.Bad)]
        [TestCase(9, 5, 15, 15, Rating.Excellent)]
        [TestCase(11, 5, 10, 10, Rating.Bad)]
        [TestCase(11, 6, 15, 15, Rating.Excellent)]
        [TestCase(5, 5, 10, 10, Rating.Bad)]
        [TestCase(4, 4, 15, 15, Rating.Good)]
        public void TestGameResultCondition_Success(int Moves, int Coins, int x, int y, Rating Result)
        {
            Position playerPos = new Position(x, y);
            Position targetPos = new Position(15,16);
            Game newGame = new Game(targetPos, playerPos);
            newGame.MoveDown();
            if (newGame.Player.ReachedTarget)
            {
                if (newGame.NumberOfMoves < 20)
                {
                    Assert.That(Result, Is.EqualTo(Rating.Excellent));
                }
                if (newGame.Player.NumberOfCoins > 5)
                {
                    Assert.That(Result, Is.EqualTo(Rating.Excellent));
                }
                Assert.That(Result, Is.EqualTo(Rating.Good));

            }
            if (newGame.NumberOfMoves < 10 && newGame.Player.NumberOfCoins > 5)
            {
                Assert.That(Result, Is.EqualTo(Rating.Good));
            }

            Assert.That(Result, Is.EqualTo(Rating.Bad));
        }
    }
}
