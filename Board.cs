using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Slick
{
    class Cell {
        public readonly int OilDepth;
        public readonly bool IsWater;

        public Player Owner { get; private set; }
        public int DrilledDepth { get; private set; }

        public bool StruckOil {
            get { return DrilledDepth >= OilDepth; }
        }

        public Cell(Player playerOwner, int oilDepth, bool isWater, int drilledDepth) {
            this.Owner = playerOwner;
            this.OilDepth = oilDepth;
            this.IsWater = isWater;
            this.DrilledDepth = drilledDepth;
        }

        public bool DrillTo(int depth) {
            DrilledDepth = depth;
            return StruckOil;
        }

        public void Purchase(Player playerOwner) {
            this.Owner = playerOwner;
        }

        public bool IsOwner(Player player)
        {
            return player == Owner;
        }
    }

    class Board
    {
        public int Width;
        public int Height;
        public Cell[,] Cells;
        Random Random;

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
                    Cells[x, y] = new Cell(null, Random.Next(0, 4), Random.NextDouble() > 0.5, 0);
                }
            }
        }

        public void PurchaseCell(int x, int y, Player player)
        {
            Cells[x,y].Purchase(player);
        }

        public bool DrillCell(int x, int y, int depth)
        {
            return Cells[x, y].DrillTo(depth);
        }

        public bool IsOwner(int x, int y, Player player)
        {
            return Cells[x, y].IsOwner(player);
        }
    }
}
