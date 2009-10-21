using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Slick
{
    public delegate void MouseLocation(int x, int y);

    class MouseInputHandler : GameComponent
    {
        public MouseLocation leftMouseClick;

        MouseState previousMouseState;

        public MouseInputHandler(Game game) 
            :base(game)
        {
            game.Components.Add(this);

            previousMouseState = Mouse.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Released
                && previousMouseState.LeftButton == ButtonState.Pressed)
                leftMouseClick(mouseState.X, mouseState.Y);

            previousMouseState = mouseState;

            base.Update(gameTime);
        }
    }
}
