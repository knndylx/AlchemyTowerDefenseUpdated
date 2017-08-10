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
        //tile content vars
        public Texture2D texture { get; private set; }
        public Rectangle rect { get; private set; }

        /// <summary>
        /// Constructor for the tile
        /// </summary>
        /// <param name="r">Rectangle that binds the tile</param>
        /// <param name="t">Texture of the tile</param>
        public Tile(Rectangle r,Texture2D t)
        {
            texture = t;
            rect = r;
        }

        /// <summary>
        /// Draws the tile to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
