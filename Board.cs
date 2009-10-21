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

        public Player Owner { get; set; }
        public int DrilledDepth { get; private set; }
        public int PurchaseCost { get; private set; }

        public int DrillCost(int depth)
        {
            return baseDrillCost * depth;
        }
        int baseDrillCost;

        public bool StruckOil {
            get { return DrilledDepth >= OilDepth; }
        }

        public Cell(Player playerOwner, int oilDepth, bool isWater, int drilledDepth, int cost, int baseDrillCost) {
            this.Owner = playerOwner;
            this.OilDepth = oilDepth;
            this.IsWater = isWater;
            this.DrilledDepth = drilledDepth;
            this.PurchaseCost = cost;
            this.baseDrillCost = baseDrillCost;
        }

        public bool DrillTo(int depth) {
            DrilledDepth = depth;
            return StruckOil;
        }

        public bool IsOwner(Player player)
        {
            return player == Owner;
        }
    }

    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        Cell[,] Cells;
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
                    Player owner = null;
                    var oilDepth = Random.Next(0, 4);
                    var isWater = Random.NextDouble() > 0.5;
                    var drilledDepth = 0;
                    var purchaseCost = Random.Next(100, 1100);
                    var baseDrillCost = Random.Next(10, purchaseCost / 10);
                    if (isWater) baseDrillCost *= Random.Next(2, 3);
                    Cells[x, y] = new Cell(owner, oilDepth, isWater, drilledDepth, purchaseCost, baseDrillCost);
                }
            }
        }

        public int PurchaseCost(int x, int y)
        {
            return Cells[x, y].PurchaseCost;
        }

        public void PurchaseCell(int x, int y, Player player)
        {
            Cells[x,y].Owner = player;
        }

        public int DrillCost(int x, int y, int depth)
        {
            return Cells[x, y].DrillCost(depth);
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
