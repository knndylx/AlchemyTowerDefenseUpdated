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
    public class ToolboxState: GameState
    {
        private EditorState ParentEditor;
        private Texture2D background = GlobalConfig.Textures.Toolbox["toolboxbackground"];
        public List<EditorTileButton> tileButtons = new List<EditorTileButton>();
        private Rectangle rect;

        public void Initialize(GameStateManager g, EditorState e)
        {
            ParentEditor = e;
            rect = new Rectangle(GlobalConfig.GameDimensions.Width - background.Width, 0, background.Width, background.Height);
            PopulateToolbox();
            base.Initialize(g);
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
        public override void Update()
        {
            HandleInput(); 
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

            //if T is pressed go back to the editor
            if (GlobalConfig.Input.currentButtonStates[Keys.T] &&
                !GlobalConfig.Input.previousButtonStates[Keys.T])
            {
                ChangeState(GameStateEnum.Editor);
            }

            //check for click on button
            if (GlobalConfig.Input.currentMouseState[Util.MouseButtonsEnum.Left] == ButtonState.Pressed)
            {
                 ParentEditor.brushTexture = ClickTileButton();
            }

            //highlight buttons if the mouse is over them
            foreach (EditorTileButton b in tileButtons)
            {
                if (b.Rect.Contains(GlobalConfig.Input.cursorx, GlobalConfig.Input.cursory))
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
        /// <returns>Returns the texture of the tile button that was clicked or a blank texture
        /// if there was no button highlighted</returns>
        public Texture2D ClickTileButton()
        {
            Button hButton = null;
            foreach (EditorTileButton b in tileButtons)
            {
                if (b.IsHighlighted) hButton = b;
            }
            if (hButton == null) return GlobalConfig.Textures.Tiles["blank"];
            return hButton.Texture;
        }

        /// <summary>
        /// Draws the background and tile buttons of the toolbox
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            ParentEditor.Draw(spriteBatch);
            spriteBatch.Draw(background, rect, Color.White);
            foreach (EditorTileButton b in tileButtons)
            {
                b.Draw(spriteBatch);
            }
            Texture2D mouseTexture = GlobalConfig.Textures.Icons["cursor"];
            spriteBatch.Draw(mouseTexture,new Rectangle(GlobalConfig.Input.cursorx,GlobalConfig.Input.cursory, mouseTexture.Width, mouseTexture.Height),Color.White);
        }
    }
}
