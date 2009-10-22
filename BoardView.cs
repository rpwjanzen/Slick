using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slick
{
    class BoardView : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        Texture2D texture;
        SpriteFont font;
        Color landColor = Color.Brown;
        Color waterColor = Color.LightBlue;

        int tileWidth;
        int tileHeight;
        readonly Rectangle ScreenRectangle;
        
        Board board;

        public BoardView(Game game, Board board, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
            :base(game)
        {
            this.board = board;
            this.spriteBatch = spriteBatch;

            tileWidth = screenWidth / board.Width;
            tileHeight = screenHeight / board.Height;
            ScreenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
        }

        public override void Initialize()
        {
            texture = Game.Content.Load<Texture2D>("Board");
            font = Game.Content.Load<SpriteFont>("BoardFont");

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        { 
            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    var cell = board.Cells[x,y];
                    var rect = CalculateWindowRectangle(x, y);
                    var color = cell.IsWater ? waterColor : landColor;
                    spriteBatch.Draw(texture, rect, null, color, 0, Vector2.Zero, SpriteEffects.None, 0.5f);

                    if (cell.Owner != null) {
                        rect = CalculatePlayerWindowRectangle(x, y);
                        spriteBatch.Draw(texture, rect, null, cell.Owner.Color, 0, Vector2.Zero, SpriteEffects.None, 0.55f);
                    }

                    var depthIndicator = cell.DrilledDepth.ToString();
                    color = cell.StruckOil ? Color.Black : Color.Gray;
                    var pos = new Vector2(rect.X, rect.Y);
                    if (!board.IsOwner(x, y, null))
                        spriteBatch.DrawString(font, depthIndicator, pos + new Vector2(20, 20), color, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.25f);
                    if(board.IsOwner(x, y, null))
                        spriteBatch.DrawString(font, "$" + board.PurchaseCost(x, y).ToString(), pos + new Vector2(20, 20), Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.25f);
                }
            }

            base.Draw(gameTime);
        }

        Rectangle CalculateWindowRectangle(int x, int y)
        {
            var rect = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
            if (board.Cells[x, y].Owner != null)
            {
                rect.Inflate(-10, -10);
            }

            return rect;
        }

        Rectangle CalculatePlayerWindowRectangle(int x, int y)
        {
            return new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
        }
    }
}
