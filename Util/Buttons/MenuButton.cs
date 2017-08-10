using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Util
{
    public class MenuButton : Button
    {
        /// <summary>
        /// Constructor for the menu button.
        /// </summary>
        /// <param name="texture">Texture of the menu button.</param>
        /// <param name="rect">Binding rectangle of the menu button.</param>
        public MenuButton(Texture2D texture, Rectangle rect) : base(texture, rect) { }

        /// <summary>
        /// Draws the button to the screen
        /// </summary>
        /// <param name="spriteBatch">Still not really sure what this thing does... Need it to draw to the screen.</param>
        /// TODO - figure out what exactly the spritebatch does...
        public void Draw(SpriteBatch spriteBatch)
        {
            //draws the button a different color if it is highlighted in the menu
            spriteBatch.Draw(Texture, Rect, IsHighlighted ? Color.LightGreen : Color.White);
        }
    }
}
