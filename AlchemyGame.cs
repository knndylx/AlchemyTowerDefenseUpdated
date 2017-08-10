using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AlchemyTowerDefense.GameData;

namespace AlchemyTowerDefense
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AlchemyGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameStateManager gsm;

        //all of the keys required for all states of the game
        List<Keys> keys = new List<Keys>()
        {
            Keys.Up,
            Keys.Down,
            Keys.Escape,
            Keys.T,
            Keys.Enter
        };

        public AlchemyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gsm = new GameStateManager(Content);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferHeight = gsm.currentState.ScreenHeight;
            graphics.PreferredBackBufferWidth = gsm.currentState.ScreenWidth;
            graphics.ApplyChanges();
            gsm.Initialize();
            GlobalConfig.InitializeConfig(Content,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, keys);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to Draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gsm.LoadContent(Content);

            //Map.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            gsm.Update();
            GlobalConfig.Input.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should Draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            gsm.Draw(spriteBatch);
            //Map.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
