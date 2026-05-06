using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_ProbniTest.Models
{
    public class Player
    {
        public Position Position { get; set; }
        public int NumberOfCoins { get; set; }
        public bool ReachedTarget { get; set; }

        public Player(Position position)
        {
            Position = position;
        }
    }
}
