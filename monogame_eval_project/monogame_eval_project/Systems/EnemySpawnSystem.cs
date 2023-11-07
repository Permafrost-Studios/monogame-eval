using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        private ContentManager _content;

        public EnemySpawnSystem(SceneGraph sceneGraph, ContentManager content)
        : base(Aspect.All(typeof(Components.Enemy), typeof(SceneNode)))
        {
            _sceneGraph = sceneGraph;
            _content = content;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            var _enemyTexture = _content.Load<Texture2D>("PrototypeArt/Enemy");

            SceneNode newEnemyNode = new SceneNode("Enemy", new Vector2(258, 258));
            Sprite enemySprite = new Sprite(_enemyTexture);

            newEnemyNode.Entities.Add(new SpriteEntity(enemySprite));

            _sceneGraph.RootNode.Children.Add(newEnemyNode);

            var enemy = CreateEntity();
            enemy.Attach(new Components.Enemy { _WalkSpeed = 2f, _MeleeDamage = 10f, _Health = 30f});
            enemy.Attach(newEnemyNode);
            enemy.Attach(new Components.Collider { _CollisionLayer = Components.Collider.CollisionLayer.Enemy, _Height = 500f, _Width = 500f });

            /*
            SceneNode newEnemyNode2 = new SceneNode("Enemy", new Vector2(1300, 800));

            newEnemyNode2.Entities.Add(new SpriteEntity(enemySprite));

            _sceneGraph.RootNode.Children.Add(newEnemyNode2);

            
            var enemy2 = CreateEntity();
            enemy2.Attach(new Components.Enemy { _WalkSpeed = 2f, _MeleeDamage = 10f, _Health = 30f });
            enemy2.Attach(newEnemyNode2);
            enemy2.Attach(new Components.Collider { _CollisionLayer = Components.Collider.CollisionLayer.Enemy, _Height = 10f, _Width = 10f });
            */
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
