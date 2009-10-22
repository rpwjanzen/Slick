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
        public MouseLocation LeftMouseClick;

        MouseState _PreviousMouseState;

        public MouseInputHandler(Game game) 
            :base(game)
        {
            game.Components.Add(this);

            _PreviousMouseState = Mouse.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Released
                && _PreviousMouseState.LeftButton == ButtonState.Pressed)
                LeftMouseClick(mouseState.X, mouseState.Y);

            _PreviousMouseState = mouseState;

            base.Update(gameTime);
        }
    }
}
