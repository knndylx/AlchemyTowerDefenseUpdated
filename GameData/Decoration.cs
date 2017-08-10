using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AlchemyTowerDefense.GameData
{
    public class Decoration
    {
        public Rectangle rect { get; private set; }
        public Texture2D texture { get; private set; }
        public float rotation { get; private set; }
        public Vector2 origin { get; private set; }

        public Decoration(Rectangle r, Texture2D t)
        {
            rect = r;
            texture = t;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Random rand = new Random();
            int d = rand.Next(0, 360);
            rotation = MathHelper.ToRadians(d);
        }

        public Decoration(Rectangle r, Texture2D t, float rot)
        {
            rect = r;
            texture = t;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            rotation = rot;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, null, Color.White, rotation, origin, SpriteEffects.None, 0);
        }
    }
}
