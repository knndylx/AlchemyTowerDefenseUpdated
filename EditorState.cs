using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using AlchemyTowerDefense.Util;

namespace AlchemyTowerDefense
{
    public class EditorState: GameState
    {
        public GameData.Map Map { get; set; }

        TextureDict tileTextures;
        TextureDict mouseTextures;
        TextureDict decorationTextures;

        Texture2D brushTexture;
        Texture2D mouseTexture;
        Texture2D highlightGridTexture;

        InputProcessor mInputProcessor = new InputProcessor();

        int cursorx, cursory, gridx, gridy;
        bool canvasActive = true;

        Editor.Toolbox toolbox = new Editor.Toolbox();

        public delegate void PressDelegate(int i);
        public event PressDelegate StateChangeDelegate;


        public override void Initialize()
        {
            //Console.Write("initializing the editor state");
            Map = new GameData.Map();
            //set up the input processor so that it looks for the only key that we need right now which is the t key for the toolbox menu pop-up
            List<Keys> listkeys = new List<Keys>
            {
                Keys.T,
                Keys.Escape
            };
            mInputProcessor.Initialize(listkeys);
        }

        private void UpdateCursor()
        {
            //snaps the cursor x and y coordinates to a grid so that you can draw the cursor highlight over the top of the grid to show which tile you are highlighting
            cursorx = Mouse.GetState().X;
            cursory = Mouse.GetState().Y;

            //bind mouse to the screen
            if (cursorx < 0)
                cursorx = 0;
            else if (cursorx > 1279)
                cursorx = 1279;
            if (cursory < 0)
                cursory = 0;
            else if (cursory > 959)
                cursory = 959;

            gridx = (int)Math.Floor(cursorx / 64.0);
            gridy = (int)Math.Floor(cursory / 64.0);

            cursorx = gridx * 64;
            cursory = gridy * 64;

            //Console.WriteLine(string.Format("Actual X: {0} Actual Y: {1} Grid X: {2} Grid Y: {3}", Mouse.GetState().X, Mouse.GetState().Y, cursorx, cursory));
        }

        public override void LoadContent(ContentManager c)
        {
            tileTextures = new TextureDict(c, "tiles");
            mouseTextures = new TextureDict(c, "icons");
            decorationTextures = new TextureDict(c, "decos");
            brushTexture = tileTextures.dict["blank"];
            mouseTexture = mouseTextures.dict["cursor"];
            highlightGridTexture = mouseTextures.dict["highlight"];
            toolbox.LoadContent(c, mouseTextures);
            Map.LoadContent(c);
            base.LoadContent(c);
        }

        public override void Update()
        {
            mInputProcessor.Update();
            if (canvasActive)
            {
                UpdateCursor();
            }
            HandleInput();
            toolbox.Update(mInputProcessor);
            base.Update();
        }

        private void HandleInput()
        {
            //if the left mouse button is pressed then paint the tile onto the Map
            if (mInputProcessor.currentMouseState[Util.MouseButtons.Left] == ButtonState.Pressed && canvasActive == true)
            {
                //Console.WriteLine(string.Format("changing tile at x:{0} y:{1}",gridx, gridy));

                //if the brush is a tile
                if (tileTextures.dict.ContainsValue(brushTexture))
                {
                    if (Map.terrainTiles[gridy, gridx].texture != brushTexture)
                        Map.ChangeTile(gridx, gridy, brushTexture);
                }
                //else if the brush is a decoration
                else if (decorationTextures.dict.ContainsValue(brushTexture))
                {
                    if(mInputProcessor.previousMouseState[Util.MouseButtons.Left] == ButtonState.Released)
                    {
                        Map.PaintDecoration(Mouse.GetState().X, Mouse.GetState().Y, brushTexture);
                    }
                }
            }
            //if the toolbox is active and the user clicked
            else if (mInputProcessor.currentMouseState[Util.MouseButtons.Left] == ButtonState.Pressed && toolbox.active == true)
            {
                Texture2D textureClick = toolbox.Click();
                if (textureClick != null)
                {
                    brushTexture = textureClick;
                }
            }

            //transition from the canvas being active to the toolbox being active
            if(canvasActive == true && mInputProcessor.currentButtonStates[Keys.T] == true && mInputProcessor.previousButtonStates[Keys.T] == false)
            {
                canvasActive = false;
                toolbox.active = true;
            }

            //transition from the toolbox being active to the canvas being active
            else if (canvasActive == false && mInputProcessor.currentButtonStates[Keys.T] == true && mInputProcessor.previousButtonStates[Keys.T] == false)
            {
                canvasActive = true;
                toolbox.active = false;
                //Map.SaveToFile("Map.txt");
            }

            if (mInputProcessor.currentButtonStates[Keys.Escape] && !mInputProcessor.previousButtonStates[Keys.Escape])
            {
                StateChangeDelegate?.Invoke(3);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            if(canvasActive)
                spriteBatch.Draw(highlightGridTexture, new Vector2(cursorx, cursory), Color.White);
            toolbox.Draw(spriteBatch);
            spriteBatch.Draw(mouseTexture, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, mouseTexture.Width, mouseTexture.Height), Color.White);
            base.Draw(spriteBatch);
        }


    }
}
