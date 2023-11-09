using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using monogame_eval_project.Components;
using monogame_eval_project.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.GameScreens
{
    public class MainMenuScreen : GameScreen
    {
        private new GameBase Game => (GameBase)base.Game;

        public MainMenuScreen(GameBase game) : base(game) { }

        private World _world;

        public override void Initialize()
        {
            base.Initialize();

            _world = new WorldBuilder()
            .Build();
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Game._spriteBatch.Begin();

            Game._spriteBatch.End();

            _world.Draw(gameTime);
        }
    }
}
