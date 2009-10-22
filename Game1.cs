using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Slick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		NotificationBox notificationBox;

        const int ScreenWidth = 1280;
        const int ScreenHeight = 720;

        const int BoardWidth = 10;
        const int BoardHeight = 10;

        const int startingMoney = 10000;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var random = new Random(0);
            var board = new Board(BoardWidth, BoardHeight, random);

            var playerList = new[]
                {
                    new Player("Ian", startingMoney, Color.Orange),
                    new Player("Ryan", startingMoney, Color.DarkBlue),
                    new Player("Stefan", startingMoney, Color.Pink),
                    new Player("Jeremy", startingMoney, Color.Green)
                }.ToList();

            var turnManager = new TurnManager(board, playerList);

            notificationBox = null;
            var mouseInputHandler = new MouseInputHandler(this);
            var mouseInputBehaviour = new MouseInputBehaviour(notificationBox, board, turnManager, mouseInputHandler, ScreenWidth, ScreenHeight);

            //var turnIndicatorView = new TurnIndicatorView(this, spriteBatch, turnManager, BoardWidth, BoardHeight);

            for(int i = 0; i < BoardWidth; i++) {
                board.Cells[i, 0].Owner = playerList[0];
            }

            var boardView = new BoardView(this, board, spriteBatch, ScreenWidth, ScreenHeight);
            Components.Add(boardView);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Pink);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
