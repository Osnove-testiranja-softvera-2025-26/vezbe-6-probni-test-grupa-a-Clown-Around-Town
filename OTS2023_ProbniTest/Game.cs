using OTS2026_ProbniTest.Exceptions;
using OTS2026_ProbniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_ProbniTest
{

    public enum Direction
    {
        Up,
        Down,
        Left, 
        Right
    }

    public enum Rating
    {
        Bad,
        Good,
        Excellent
    }

    public class Game
    {
        public Player Player { get; set; }
        public Position Target { get; set; }
        public Map Map { get; set; }
        public static readonly int MapSize = 50;
        public static readonly int MaxNumberOfMoves = 30;
        public int NumberOfMoves = 0;

        public Game(Position targetPosition, Position playerPosition)
        {
            
            Map = new Map(MapSize);
            Map.CreateEmptyMap();
            if(targetPosition == null || playerPosition == null)
                throw new InvalidPositionException("Target and player positions must be provided!");
            if (!ValidatePosition(targetPosition) || !ValidatePosition(playerPosition))
                throw new InvalidPositionException("Target and player cannot be outside the map boundaries!");
            if (targetPosition.Equals(playerPosition))
                throw new InvalidPositionException("Target and player cannot have the same position!");

            Player = new Player(playerPosition);
            Target = targetPosition;

        }

        public void MoveUp()
        {
            if (ValidateMove(Direction.Up))
            {
                Player.Position.Y--;
                NumberOfMoves++;
                UpdatePlayerWithTileContent();
            }
        }

        public void MoveDown()
        {
            if (ValidateMove(Direction.Down))
            {
                Player.Position.Y++;
                NumberOfMoves++;
                UpdatePlayerWithTileContent();
            }
        }

        public void MoveLeft()
        {
            if (ValidateMove(Direction.Left))
            {
                Player.Position.X--;
                NumberOfMoves++;
                UpdatePlayerWithTileContent();
            }
        }


        public void MoveRight()
        {
            if (ValidateMove(Direction.Right))
            {
                Player.Position.X++;
                NumberOfMoves++;
                UpdatePlayerWithTileContent();
            }

        }

        public bool ValidateMove(Direction direction)
        {

            if(NumberOfMoves + 1 > MaxNumberOfMoves)
            {
                throw new NumberOfMovesExceededException();
            }

            Position newPosition;
            switch (direction) {
                case Direction.Up:
                    newPosition = new Position(Player.Position.X, Player.Position.Y - 1);
                    break;
                case Direction.Down:
                    newPosition = new Position(Player.Position.X, Player.Position.Y + 1);
                    break;
                case Direction.Left:
                    newPosition = new Position(Player.Position.X - 1, Player.Position.Y);
                    break;
                case Direction.Right:
                    newPosition = new Position(Player.Position.X + 1, Player.Position.Y);
                    break;
                default:
                    return false;
            }

            return ValidatePlayerPosition(newPosition);
        }

        public bool ValidatePlayerPosition(Position position)
        {
            if(!ValidatePosition(position))
                return false;
            if (Map.Tiles[position.X, position.Y].Content.Equals(TileContent.Barrier))
                return false;
            return true;
        }

        public bool ValidatePosition(Position position)
        {
            return !(position.X < 0 || position.X > MapSize - 1 || position.Y < 0 || position.Y > MapSize - 1);
        }

        public void UpdatePlayerWithTileContent()
        {
            if (PlayerShouldCollectCoin())
            {
                Player.NumberOfCoins++;
                Map.Tiles[Player.Position.X, Player.Position.Y].Content = TileContent.Empty;
            }
            if (PlayerReachedTarget())
            {
                Player.ReachedTarget = true;
            }

        }

        public bool PlayerShouldCollectCoin()
        {
            return Map.Tiles[Player.Position.X, Player.Position.Y].Content.Equals(TileContent.Coin);
        }

        public bool PlayerReachedTarget()
        {
            return Player.Position.Equals(Target);
        }

        public Rating CalculateRating()
        {
            
            if (Player.ReachedTarget)
            {
                if(NumberOfMoves < 20)
                {
                    return Rating.Excellent;
                }
                if(Player.NumberOfCoins > 5)
                {
                    return Rating.Excellent;
                }
                return Rating.Good;

            }
            if (NumberOfMoves < 10 && Player.NumberOfCoins > 5)
            {
                return Rating.Good;
            }

            return Rating.Bad;
        }

    }
}
