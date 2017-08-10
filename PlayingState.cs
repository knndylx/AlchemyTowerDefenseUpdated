using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AlchemyTowerDefense
{
    public class PlayingState: GameState
    {
        public bool Active { get; private set; }

        private GameData.Map map;

        public PlayingState()
        {
            map = new GameData.Map();
        }

        public override void LoadContent(ContentManager c)
        {
            map.LoadContent(c, "Map.txt");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
        }
    }
}
