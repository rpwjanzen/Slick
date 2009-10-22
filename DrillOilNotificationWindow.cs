using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slick
{
    public delegate void DrillNotification(int boardX, int boardY, int depth);
    public delegate void DrillWindowClose();

    class DrillOilNotificationWindow : DrawableGameComponent, INotificationWindow
    {
        public DrillNotification PlayerChoseToDrill;
        public DrillWindowClose DrillWindowClosed;

        SpriteBatch _SpriteBatch;
        int _BoardX, _BoardY;
        Vector2 _WindowLocation;
        Texture2D _CircleTexture;
        Texture2D _CancelButtonTexture;
        Texture2D _NotificationBackground;
        SpriteFont _Font;
        Board _Board;
        Game _Game;

        readonly Vector2 _ButtonTextOffset = new Vector2(10, 3);
        readonly Vector2 _ButtonCostOffset = new Vector2(40, 3);

        List<Vector2> buttonPossitions;

        const int _ButtonOffsetX = 10;
        const int _ButtonOffsetY = 50;

        public DrillOilNotificationWindow(Game game, SpriteBatch spriteBatch, Board board, int boardX, int boardY)
            :base(game)
        {
            _Board = board;
            _Game = game;
            _SpriteBatch = spriteBatch;
            _BoardX = boardX;
            _BoardY = boardY;

            _WindowLocation = new Vector2(400, 250);

            game.Components.Add(this);
        }

        public void handleMouseClick(int mouseX, int mouseY)
        {
            int buttonIndex;

            if (_IfMouseOverButtonGetIndex(mouseX, mouseY, out buttonIndex))
            {
                // check to see if this button is active
                if ((buttonIndex + 1) > _Board.Cells[_BoardX, _BoardY].DrilledDepth)
                {
                    if (buttonIndex != 3)
                        PlayerChoseToDrill(_BoardX, _BoardY, buttonIndex + 1);
                    _Game.Components.Remove(this);
                    DrillWindowClosed();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //_SpriteBatch.DrawString(_Font, "Select the level to drill to:", new Vector2(40, 5 + _PlayerList.IndexOf(p) * 35), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            _SpriteBatch.Draw(_NotificationBackground, _WindowLocation + new Vector2(0, 40), null, Color.White, 0f, Vector2.Zero, new Vector2(1f, 1.7f), SpriteEffects.None, 0.03f);

            foreach (Vector2 v in buttonPossitions) 
            {
                String s = "";
                Texture2D image = _CircleTexture;
                Color color = Color.Gold;
                int buttonIndex = buttonPossitions.IndexOf(v);
                if (buttonIndex == 3)
                {
                    image = _CancelButtonTexture;
                    color = Color.White;
                }
                else
                    s = (buttonIndex + 1).ToString();

                Color textColor = Color.Black;
                if (!((buttonIndex + 1) > _Board.Cells[_BoardX, _BoardY].DrilledDepth))
                    textColor = Color.Gray;

                _SpriteBatch.Draw(image, v + _WindowLocation, null, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.02f);
                _SpriteBatch.DrawString(_Font, s, v + _WindowLocation + _ButtonTextOffset, textColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.01f);
                if(buttonIndex != 3)
                    _SpriteBatch.DrawString(_Font, "$" + _Board.DrillCost(_BoardX, _BoardY, (buttonIndex + 1)), v + _WindowLocation + _ButtonCostOffset, textColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.01f);
            }

            base.Draw(gameTime);
        }

        public override void  Initialize()
        {
            _CircleTexture = Game.Content.Load<Texture2D>("WhiteCircle");
            _Font = Game.Content.Load<SpriteFont>("BoardFont");
            _CancelButtonTexture = Game.Content.Load<Texture2D>("XMark");
            _NotificationBackground = Game.Content.Load<Texture2D>("HighlightBox");

            buttonPossitions = new[] 
                {
                    new Vector2(_ButtonOffsetX, _ButtonOffsetY),
                    new Vector2(_ButtonOffsetX, _ButtonOffsetY + (10+_CircleTexture.Height)),
                    new Vector2(_ButtonOffsetX, _ButtonOffsetY + 2*(10+_CircleTexture.Height)),
                    new Vector2(_ButtonOffsetX, _ButtonOffsetY + 3*(10+_CircleTexture.Height))
                }.ToList();

 	        base.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseX"></param>
        /// <param name="mouseY"></param>
        /// <param name="index">The index of the button the mouse was over.</param>
        /// <returns>True if the quordinates are over one of the buttons.</returns>
        bool _IfMouseOverButtonGetIndex(int mouseX, int mouseY, out int index) 
        {
            index = 0;

            mouseX -= (int)_WindowLocation.X;
            mouseY -= (int)_WindowLocation.Y;

            foreach (Vector2 v in buttonPossitions) 
            {
                if( mouseX > v.X
                    && mouseX < v.X + _CircleTexture.Width
                    && mouseY > v.Y
                    && mouseY < v.Y + _CircleTexture.Height)
                {
                    index = buttonPossitions.IndexOf(v);
                    return true;
                }
            }

            return false;
        }
    }
}
