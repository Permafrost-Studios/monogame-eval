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
using Microsoft.Xna.Framework.Input;

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

            /*_world = new WorldBuilder()
            .AddSystem(new PlayerBehaviourSystem())
            .AddSystem(new EnemyBehaviourSystem(_sceneGraph, enemyTexture))
            .Build();*/
        }
        public override void LoadContent()
        {
            base.LoadContent();

            //Load Player Texture
            var playerTexture = Content.Load<Texture2D>("PrototypeArt/pokeball");

            //Load Player START
            _sceneGraph = new SceneGraph();

            _playerNode = new SceneNode("Player", GraphicsDevice.Viewport.Bounds.Center.ToVector2());
            var playerSprite = new Sprite(playerTexture);
            _playerNode.Entities.Add(new SpriteEntity(playerSprite));

            _sceneGraph.RootNode.Children.Add(_playerNode);
            //Load Player END

            _world = new WorldBuilder()
            .AddSystem(new PlayerBehaviourSystem(_sceneGraph, Content))
            .AddSystem(new EnemySpawnSystem(_sceneGraph, Content))
            .AddSystem(new EnemyBehaviourSystem())
            .AddSystem(new CollisionSystem())
            .Build();

            SpawnPlayer();
        }

        bool tempBool = false;

        public override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                //_sceneGraph.RootNode.Children.Remove((Player.Get<SceneNode>()));
                if (tempBool == false) //It will try to run for multiple frames from default input otherwise
                {
                    Player.Get<Player>()._Health = -1f;
                    tempBool = true;
                }
            }
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
            Player.Attach(new Player { _Health = 15f, _WalkSpeed = 10f});
            Player.Attach(_playerNode);
            Player.Attach(new Components.Collider { _CollisionLayer = Components.Collider.CollisionLayer.Player, _Height = 250f, _Width = 250f });
        }
    }
}
