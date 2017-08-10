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
        private Texture2D background = GlobalConfig.Textures.Toolbox["background"];
        public List<EditorTileButton> tileButtons = new List<EditorTileButton>();
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
            List<Texture2D> tileList = new List<Texture2D>(GlobalConfig.Textures.Tiles.Values);
            List<Texture2D> decoList = new List<Texture2D>(GlobalConfig.Textures.Decos.Values);

            //combine the two lists together
            tileList = tileList.Concat(decoList).ToList();

            int padding = 20;

            int i = 0;
            int k = 0;
            //place each of these textures as a button onto the tool pallete with a predefined amount of padding on each side
            foreach(Texture2D t in tileList)
            {
                tileButtons.Add(new EditorTileButton(t, new Rectangle((rect.Left + padding + k * 84), (rect.Top + padding + i * 84), 64, 64)));
                i++;
                if(i % (tileList.Count / 2) == 0)
                {
                    i = 0;
                    k++;
                }
            }
        }

        /// <summary>
        /// Handles scroll input and updates the buttons if highlighted
        /// </summary>
        /// TODO: make something more elegant rather than active or inactive state in the editor, maybe its own toolbox state in the game state manager?
        public void Update()
        {
            if (active) HandleInput(); 
        }

        /// <summary>
        /// Handles the input for scrolling in the box and highlighting buttons
        /// </summary>
        private void HandleInput()
        {
            int scrollAmount = 20;
            //scroll down
            if (GlobalConfig.Input.currentScrollWheel < GlobalConfig.Input.previousScrollWheel)
            {
                if (!(tileButtons[tileButtons.Count - 1].Rect.Bottom < (960 - 20) ))
                {
                    foreach (EditorTileButton b in tileButtons)
                    {
                        b.ChangeRect(new Rectangle(b.Rect.X, b.Rect.Y - scrollAmount, b.Rect.Width, b.Rect.Height));
                    }
                }
            }

            //scroll up
            else if(GlobalConfig.Input.currentScrollWheel > GlobalConfig.Input.previousScrollWheel)
            {
                if(!(tileButtons[0].Rect.Top > 20))
                {
                    foreach (EditorTileButton b in tileButtons)
                    {
                        b.ChangeRect(new Rectangle(b.Rect.X, b.Rect.Y + scrollAmount, b.Rect.Width, b.Rect.Height));
                    }
                }
                
            }

            //highlight buttons if the mouse is over them
            foreach (EditorTileButton b in tileButtons)
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

        /// <summary>
        /// Clicks a tile button if there is one highlighted
        /// </summary>
        /// <returns>Returns the texture of the tile button that was clicked</returns>
        public Texture2D ClickTileButton()
        {
            Button hButton = null;
            foreach (EditorTileButton b in tileButtons)
            {
                if (b.IsHighlighted == true) hButton = b;
            }
            return hButton?.Texture;
        }

        /// <summary>
        /// Draws the background and tile buttons of the toolbox
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!active) return;
            spriteBatch.Draw(background, rect, Color.White);
            foreach(EditorTileButton b in tileButtons)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
