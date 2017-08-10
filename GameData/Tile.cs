using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlchemyTowerDefense.GameData
{
    public class Tile
    {
        public Texture2D texture { get; private set; }
        public Rectangle rect { get; private set; }

        public Tile(Rectangle r,Texture2D t)
        {
            texture = t;
            rect = r;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
