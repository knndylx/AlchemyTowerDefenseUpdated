using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense
{
    public class GameState
    {
        private GameStateManager ParentManager;

        public virtual void Update()
        {

        }

        public virtual void LoadContent(ContentManager c)
        {
            
        }

        public virtual void Initialize(GameStateManager g)
        {
            ParentManager = g;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void ChangeState(GameStateEnum state)
        {
            ParentManager.ChangeActiveState(state);
        }

    }
}
