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
        //all of the vars needed to Draw the decoration with a Rotation
        public Rectangle Rect { get; private set; }
        public Texture2D Texture { get; private set; }
        public float Rotation { get; private set; }
        public Vector2 Origin { get; private set; }

        /// <summary>
        /// Constructor for the decoration with random Rotation
        /// </summary>
        /// <param name="r">Rectangle that is the binding rectangle of the decoration</param>
        /// <param name="t">Texture of the decoration</param>
        public Decoration(Rectangle r, Texture2D t)
        {
            Rect = r;
            Texture = t;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Random random = new Random();
            int degrees = random.Next(0, 360);
            Rotation = MathHelper.ToRadians(degrees);
        }

        /// <summary>
        /// Constructor for the decoration without random Rotation
        /// </summary>
        /// <param name="r">Rectangle that binds the decoration</param>
        /// <param name="t">Texture of the decoration</param>
        /// <param name="rot">Rotation, in RADIANS, of the decoration between 0 and 2*Pi</param>
        public Decoration(Rectangle r, Texture2D t, float rot)
        {
            Rect = r;
            Texture = t;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Rotation = rot;
        }

        /// <summary>
        /// Draws the decoration to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rect, null, Color.White, Rotation, Origin, SpriteEffects.None, 0);
        }
    }
}
