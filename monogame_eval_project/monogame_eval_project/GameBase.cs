using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace monogame_eval_project
{
    public class GameBase : Game
    {
        private static GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private readonly ScreenManager _screenManager;

        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        World _world = new WorldBuilder()
            .AddSystem(new Systems.TestSystem(_spriteBatch))
            .Build();

        public GameBase(int width = 1920, int height = 1080)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height
            };

            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            LoadMainScreen();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _world.Update(gameTime);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _world.Draw(gameTime);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void LoadMainScreen()
        {
            _screenManager.LoadScreen(new GameScreens.MainScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}