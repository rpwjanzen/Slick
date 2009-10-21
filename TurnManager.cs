using System;
using System.Collections.Generic;
using System.Text;

namespace Slick
{
    class TurnManager
    {
        // 1. Buy land plot
        // 2. Drill
        // 3. Nothing
        // 4. End Turn

        List<Player> Players;
        int currentPlayerIndex = 0;

        public Player CurrentPlayer
        {
            get { return Players[currentPlayerIndex]; }
        }

        Board board;

        public TurnManager(Board board, List<Player> players)
        {
            this.board = board;
            this.Players = players;
        }

        public void BuyLand(int x, int y)
        {
            board.PurchaseCell(x, y, CurrentPlayer);
        }

        public bool DrillLand(int x, int y, int depth)
        {
            return board.DrillCell(x, y, depth);
        }

        public void NextTurn()
        {
            currentPlayerIndex++;
            currentPlayerIndex %= Players.Count;
        }
    }
}
