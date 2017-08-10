using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlchemyTowerDefense.Util.Buttons
{
    public class Button
    {
        //content vars
        public Texture2D Texture { get; private set; }
        public Rectangle Rect { get; private set; }

        //status vars
        public bool IsHighlighted { get; private set; }

        /// <summary>
        /// Constructs the button.
        /// </summary>
        /// <param name="texture">This is the Texture that the button will show.</param>
        /// <param name="rect">This is the binding rectangle for the button.</param>
        public Button(Texture2D texture, Rectangle rect)
        {
            this.Texture = texture;
            this.Rect = rect;
        }

        /// <summary>
        /// Highlights the button
        /// </summary>
        public void Highlight()
        {
            IsHighlighted = true;
        }

        /// <summary>
        /// Makes the button not highlighted
        /// </summary>
        public void Dehighlight()
        {
            IsHighlighted = false;
        }

        /// <summary>
        /// Changes the rectangle of the button.
        /// </summary>
        /// <param name="rect">The new rectangle that will bind the button. </param>
        public void ChangeRect(Rectangle rect)
        {
            this.Rect = rect;
        }
    }
}
