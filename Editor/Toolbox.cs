using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlchemyTowerDefense.Util;
using AlchemyTowerDefense.Util.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Editor
{
    public class Toolbox
    {
        private Texture2D background = Textures.Toolbox["background"];
        public List<Button> tileButtons = new List<Button>();
        private Rectangle rect;
        public bool active = false;

        /// <summary>
        /// Default constructor for the toolbox
        /// </summary>
        public Toolbox()
        {
            rect = new Rectangle(1280 - background.Width, 0, background.Width, background.Height);
            PopulateToolbox();
        }

        /// <summary>
        /// Places all of the textures that can be used in a map, decorations and tiles, as buttons on the toolbox pallete
        /// </summary>
        private void PopulateToolbox()
        {
            //get all of the possible tile and decoration textures from the texture dictionaries
            List<Texture2D> tileList = new List<Texture2D>(Textures.Tiles.Values);
            List<Texture2D> decoList = new List<Texture2D>(Textures.Decos.Values);

            //combine the two lists together
            tileList = tileList.Concat(decoList).ToList();

            int padding = 20;

            int i = 0;
            int k = 0;
            //place each of these textures as a button onto the tool pallete with a predefined amount of padding on each side
            foreach(Texture2D t in tileList)
            {
                tileButtons.Add(new Button(t, new Rectangle((rect.Left + padding + k * 84), (rect.Top + padding + i * 84), 64, 64)));
                i++;
                if(i % (tileList.Count / 2) == 0)
                {
                    i = 0;
                    k++;
                }
            }
        }

        public void Update(InputProcessor ip)
        {
            if (active)
            {
                //TODO - might be able to remove the line below that is commented depending on what is happening in the editor state's update function
                //HandleInput(ip);
                foreach (Button b in tileButtons)
                {
                    if (b.Rect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        b.Highlight();
                    }
                    else
                    {
                        b.Dehighlight();
                    }
                }
            }
        }

        private void HandleInput(InputProcessor ip)
        {
            int scrollAmount = 20;
            //scroll down
            if (ip.currentScrollWheel < ip.previousScrollWheel)
            {
                if (!(tileButtons[tileButtons.Count - 1].Rect.Bottom < (960 - 20) ))
                {
                    foreach (Button b in tileButtons)
                    {
                        b.ChangeRect(new Rectangle(b.Rect.X, b.Rect.Y - scrollAmount, b.Rect.Width, b.Rect.Height));
                    }
                }
            }
            //scroll up
            else if(ip.currentScrollWheel > ip.previousScrollWheel)
            {
                if(!(tileButtons[0].Rect.Top > 20))
                {
                    foreach (Button b in tileButtons)
                    {
                        b.ChangeRect(new Rectangle(b.Rect.X, b.Rect.Y + scrollAmount, b.Rect.Width, b.Rect.Height));
                    }
                }
                
            }
        }

        public Texture2D Click()
        {
            Button hButton = null;
            foreach (Button b in tileButtons)
            {
                if (b.IsHighlighted == true) hButton = b;
            }
            if (hButton != null)
            {
                return hButton.Texture;
            }
            else
            {
                return null;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(background, rect, Color.White);
                foreach(Button b in tileButtons)
                {
                    b.Draw(spriteBatch);
                }
            }
        }
    }
}
