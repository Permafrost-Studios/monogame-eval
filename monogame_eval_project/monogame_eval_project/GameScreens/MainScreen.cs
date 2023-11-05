using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Entities;

using monogame_eval_project.Systems;
using monogame_eval_project.Components;
using System.Diagnostics;

namespace monogame_eval_project.GameScreens
{

    public class MainScreen : GameScreen
    {
        private new GameBase Game => (GameBase)base.Game;

        public MainScreen(GameBase game) : base(game) { }

        private SceneGraph _sceneGraph;
        private SceneNode _playerNode;

        private World _world;

        Entity Player;

        public override void Initialize()
        {
            base.Initialize();

            _world = new WorldBuilder()
            .AddSystem(new PlayerBehaviourSystem())
            .Build();
        }
        public override void LoadContent()
        {
            base.LoadContent();

            //ballTexture = Content.Load<Texture2D>("Art/pokeBall");
            _sceneGraph = new SceneGraph();

            _playerNode = new SceneNode("Player", GraphicsDevice.Viewport.Bounds.Center.ToVector2());
            var playerTexture = Content.Load<Texture2D>("Art/pokeball");
            var playerSprite = new Sprite(playerTexture);
            _playerNode.Entities.Add(new SpriteEntity(playerSprite));

            _sceneGraph.RootNode.Children.Add(_playerNode);

            SpawnPlayer();
        }

        public override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Game._spriteBatch.Begin();

            Game._spriteBatch.Draw(_sceneGraph);

            Game._spriteBatch.End();

            _world.Draw(gameTime);
        }

        void SpawnPlayer()
        {
            Player = _world.CreateEntity();
            Player.Attach(new Player());
            Player.Attach(_playerNode);
        }
    }
}
