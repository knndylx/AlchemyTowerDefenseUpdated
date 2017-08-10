using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Util
{
    public class EditorTileButton : Button
    {
        //internal textures
        private Texture2D highlightTexture = GlobalConfig.Textures.Icons["highlight"];

        /// <summary>
        /// Constructor for the Editor Tile Button
        /// </summary>
        /// <param name="texture">Tile Texture that the button will show</param>
        /// <param name="rect">Binding square of the button</param>
        public EditorTileButton(Texture2D texture, Rectangle rect) : base(texture, rect) { }

        /// <summary>
        /// Draws the button to the screen
        /// </summary>
        /// <param name="spriteBatch">Still not really sure what this thing does... Need it to draw to the screen.</param>
        /// TODO - figure out what exactly the spritebatch does...
        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsHighlighted)
                spriteBatch.Draw(highlightTexture, Rect, Color.White);
            spriteBatch.Draw(Texture, Rect, Color.White);
        }
    }
}
