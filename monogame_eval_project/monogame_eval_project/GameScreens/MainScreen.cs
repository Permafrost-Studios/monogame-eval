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
using monogame_eval_project.Systems.Abilities;

namespace monogame_eval_project.GameScreens
{

    public class MainScreen : GameScreen
    {
        private new GameBase Game => (GameBase)base.Game;

        public MainScreen(GameBase game) : base(game) { }

        private SceneGraph _sceneGraph;

        private World _world;

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

            _sceneGraph = new SceneGraph();

            _sceneGraph.RootNode.Children.Add(new SceneNode("empty-root", GraphicsDevice.Viewport.Bounds.Center.ToVector2()));

            _world = new WorldBuilder()
            .AddSystem(new EnemyBehaviourSystem())
            .AddSystem(new CollisionSystem())
            .AddSystem(new MovementSystem())
            .AddSystem(new PlayerBehaviourSystem(_sceneGraph, Content))
            .AddSystem(new EnemySpawnSystem(_sceneGraph, Content))
            .AddSystem(new StraightProjectileAbilitySystem(_sceneGraph, Content))
            .Build();
        }

        bool tempBool = false;

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
    }
}
