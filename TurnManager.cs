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
            CurrentPlayer.Money -= board.PurchaseCost(x, y);
            board.PurchaseCell(x, y, CurrentPlayer);
            NextTurn();
        }

        /// <summary>
        /// Drills for oil at given location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="depth">How deep to drill(1-3)</param>
        /// <returns>True, if oil was found.</returns>
        public bool DrillLand(int x, int y, int depth)
        {
            CurrentPlayer.Money -= board.DrillCost(x, y, depth);
            NextTurn();
            return board.DrillCell(x, y, depth);
        }

        private void NextTurn()
        {
            currentPlayerIndex++;
            currentPlayerIndex %= Players.Count;
        }
    }
}
