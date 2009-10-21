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
        int screenWidth;
        int screenHeight;

        public MouseInputBehaviour(NotificationBox notificationBox, Board board, MouseInputHandler mouseInputHandler, int screenWidth, int screenHeight)
        {
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
                
            }
        }

        public void getBoardTileQuoordsFromMouse(int mouseX, int mouseY, out int boardX, out int boardY)
        {
            
            boardX = (int)Math.Floor(mouseX / (screenWidth / (double)board.Width));
            boardY = (int)Math.Floor(mouseY / (screenHeight / (double)board.Height));
        }
    }
}
