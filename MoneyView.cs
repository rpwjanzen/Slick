using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slick
{
    class MoneyView : DrawableGameComponent
    {
        TurnManager _TurnManager;
        SpriteBatch _SpriteBatch;
        Texture2D _CircleTexture;
        List<Player> _PlayerList;
        SpriteFont _Font;

        public MoneyView(Game game, TurnManager turnManager, SpriteBatch spriteBatch, List<Player> players)
            :base(game)
        {
            _PlayerList = players;
            _TurnManager = turnManager;
            _SpriteBatch = spriteBatch;

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            _CircleTexture = Game.Content.Load<Texture2D>("WhiteCircle");
            _Font = Game.Content.Load<SpriteFont>("BoardFont");

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(Player p in _PlayerList)
            {
                Color circleDrawColor;
                if (p == _TurnManager.CurrentPlayer)
                    circleDrawColor = p.Color;
                else
                    circleDrawColor = Color.Lerp(p.Color, Color.Gray, 0.7f);

                _SpriteBatch.Draw(_CircleTexture, new Vector2(10, 10 + _PlayerList.IndexOf(p) * 35), null, circleDrawColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.10f);
                _SpriteBatch.DrawString(_Font, "$" + p.Money.ToString(), new Vector2(45, 20 + _PlayerList.IndexOf(p) * 35), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.09f);
                _SpriteBatch.DrawString(_Font, p.Name.ToString(), new Vector2(40, 5 + _PlayerList.IndexOf(p) * 35), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.09f);
            }

            base.Draw(gameTime);
        }
    }
}
