using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slick
{
    class BoardView : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D image;

        public BoardView(Game game, SpriteBatch spriteBatch)
            :base(game)
        {
            this.spriteBatch = spriteBatch;
            game.Components.Add(this);
        }

        

        public override void Initialize()
        {
            image = Game.Content.Load<Texture2D>(@"Map");

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }
    }
}
