using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_ProbniTest.Models
{
    public class Map
    {
        public Tile[,] Tiles { get; set; }
        public int MapSize { get; set; }

        public Map(int mapSize)
        {
            MapSize = mapSize;
            Tiles = new Tile[mapSize, mapSize];
        }

        public void CreateEmptyMap()
        {
            for(int i=0; i<MapSize; i++)
            {
                for(int j=0; j<MapSize; j++)
                {
                    Tiles[i, j] = new Tile();
                }
            }
        }

        public void AddTile(TileContent content, int x, int y)
        {
            Tiles[x, y] = new Tile(content);
        }
    }
}
