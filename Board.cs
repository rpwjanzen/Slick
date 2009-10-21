using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Slick
{
    class Cell {
        int PlayerOwner;
        int OilDepth;
        bool IsWater;

        public Cell(int playerOwner, int oilDepth, bool isWater) {
            this.PlayerOwner = playerOwner;
            this.OilDepth = oilDepth;
            this.IsWater = isWater;
        }
    }

    class Board
    {
        public int Width;
        public int Height;
        public Cell[,] Cells;
        public Random Random;

        public Board(int width, int height, Random random)
        {
            this.Width = width;
            this.Height = height;
            this.Random = random;

            PopulateBoard();
        }

        void PopulateBoard()
        {
            Cells = new Cell[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cells[x, y] = new Cell(0, Random.Next(0, 4), Random.NextDouble() > 0.5);
                }
            }
        }
    }
}
