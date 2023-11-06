using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.SceneGraphs;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace monogame_eval_project.Systems
{
    public class EnemySpawnSystem : EntityUpdateSystem
    {
        private SceneGraph _sceneGraph;

        private ComponentMapper<Components.Enemy> _enemyMapper;
        private ComponentMapper<SceneNode> _sceneNodeMapper;

        private Texture2D _enemyTexture;

        public EnemySpawnSystem(SceneGraph sceneGraph, Texture2D enemyTexture)
        : base(Aspect.All(typeof(Components.Enemy), typeof(SceneNode)))
        {
            _sceneGraph = sceneGraph;
            _enemyTexture = enemyTexture;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {

            var enemy = CreateEntity();
            SceneNode newEnemyNode = new SceneNode("Enemy", new Vector2(258, 258));
            Sprite enemySprite = new Sprite(_enemyTexture);

            newEnemyNode.Entities.Add(new SpriteEntity(enemySprite));

            _sceneGraph.RootNode.Children.Add(newEnemyNode);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
