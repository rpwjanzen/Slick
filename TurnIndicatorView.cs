using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slick
{
    class TurnIndicatorView : DrawableGameComponent
    {
        TurnManager turnManager;
        SpriteBatch spriteBatch;
        Texture2D texture;
        int boardWidth, boardHeight;

        public TurnIndicatorView(Game game, SpriteBatch spriteBatch, TurnManager turnManager, int boardWidth, int boardHeight)
            :base(game)
        {
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            this.spriteBatch = spriteBatch;
            this.turnManager = turnManager;

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            texture = Game.Content.Load<Texture2D>(@"BorderOutline");

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, new Rectangle(0, 0, boardWidth, boardHeight), turnManager.CurrentPlayer.color);

            base.Draw(gameTime);
        }
    }
}
