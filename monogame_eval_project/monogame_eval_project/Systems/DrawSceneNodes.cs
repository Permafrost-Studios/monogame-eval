using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.SceneGraphs;

namespace monogame_eval_project.Systems
{
    internal class DrawSceneNodes: EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<SceneNode> _sceneNodeMapper;

        public DrawSceneNodes(SpriteBatch spriteBatch)
        : base(Aspect.All(typeof(SceneGraph)))
        {
            _spriteBatch = spriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            foreach (var entity in ActiveEntities)
            {
                var sprite = _sceneNodeMapper.Get(entity);

                
            }

            _spriteBatch.End();
        }

        public override void Initialize(IComponentMapperService mapperService)
        {

        }
    }
}
