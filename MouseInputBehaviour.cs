using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slick
{
    class MouseInputBehaviour
    {
        INotificationWindow notificationBox;
        Board _Board;
        TurnManager turnManager;
        int screenWidth;
        int screenHeight;
        Game _Game;
        SpriteBatch _SpriteBatch;

        public MouseInputBehaviour(Game game, SpriteBatch spriteBatch, INotificationWindow notificationBox, Board board, TurnManager turnManager, MouseInputHandler mouseInputHandler, int screenWidth, int screenHeight)
        {
            _SpriteBatch = spriteBatch;
            _Game = game;
            this.turnManager = turnManager;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;      
            this.notificationBox = notificationBox;
            this._Board = board;

            mouseInputHandler.LeftMouseClick += handleMouseClick;
        }

        void handleMouseClick(int mouseX, int mouseY) 
        {
            if (notificationBox != null)
                notificationBox.handleMouseClick(mouseX, mouseY);
            else 
            {
                int boardX, boardY;
                getBoardTileQuoordsFromMouse(mouseX, mouseY, out boardX, out boardY);

                if (boardX > 0 && boardX < _Board.Width
                    && boardY > 0 && boardY < _Board.Height)
                {
                    if (_Board.IsOwner(boardX, boardY, turnManager.CurrentPlayer) && !_Board.Cells[boardX, boardY].StruckOil && !(_Board.Cells[boardX, boardY].DrilledDepth == 3))
                    {
                        var drillWindow = new DrillOilNotificationWindow(_Game, _SpriteBatch, _Board, boardX, boardY);
                        notificationBox = (INotificationWindow)drillWindow;
                        drillWindow.PlayerChoseToDrill += drill;
                        drillWindow.DrillWindowClosed += notificationWindowClosed;
                        //turnManager.DrillLand(boardX, boardY, 1);
                    }
                    else if (_Board.IsOwner(boardX, boardY, null))
                        turnManager.BuyLand(boardX, boardY);
                }
            }
        }

        void getBoardTileQuoordsFromMouse(int mouseX, int mouseY, out int boardX, out int boardY)
        {
            boardX = (int)Math.Floor(mouseX / (screenWidth / (double)_Board.Width));
            boardY = (int)Math.Floor(mouseY / (screenHeight / (double)_Board.Height));
        }

        void drill(int x, int y, int depth) 
        {
            // grab current player now becuase it will get incremented when we drill
            Player currentPlayer = turnManager.CurrentPlayer;
            if (turnManager.DrillLand(x, y, depth))
                currentPlayer.Money += 1000;
        }

        void notificationWindowClosed() 
        {
            notificationBox = null;
        }
    }
}
