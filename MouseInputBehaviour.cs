using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slick
{
    class MouseInputBehaviour
    {
        NotificationBox notificationBox;
        Board board;
        TurnManager turnManager;
        int screenWidth;
        int screenHeight;

        public MouseInputBehaviour(NotificationBox notificationBox, Board board, TurnManager turnManager, MouseInputHandler mouseInputHandler, int screenWidth, int screenHeight)
        {
            this.turnManager = turnManager;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;      
            this.notificationBox = notificationBox;
            this.board = board;

            mouseInputHandler.leftMouseClick += handleMouseClick;
        }

        void handleMouseClick(int mouseX, int mouseY) 
        {
            if (notificationBox != null)
                notificationBox.handleMouseClick(mouseX, mouseY);
            else 
            {
                int boardX, boardY;
                getBoardTileQuoordsFromMouse(mouseX, mouseY, out boardX, out boardY);

                if (board.IsOwner(boardX, boardY, turnManager.CurrentPlayer))
                    turnManager.DrillLand(boardX, boardY, 1);
                else if (board.IsOwner(boardX, boardY, null))
                    turnManager.BuyLand(boardX, boardY);
            }
        }

        void getBoardTileQuoordsFromMouse(int mouseX, int mouseY, out int boardX, out int boardY)
        {
            
            boardX = (int)Math.Floor(mouseX / (screenWidth / (double)board.Width));
            boardY = (int)Math.Floor(mouseY / (screenHeight / (double)board.Height));
        }
    }
}
