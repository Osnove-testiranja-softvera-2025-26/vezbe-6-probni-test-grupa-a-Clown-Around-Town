using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_ProbniTest.Models
{
    public enum TileContent
    {
        Empty,
        Coin,
        Barrier
    }

    public class Tile
    {
        public TileContent Content { get; set; }

        public Tile()
        {
            Content = TileContent.Empty;
        }

        public Tile(TileContent content)
        {
            Content = content;
        }
    }
}
