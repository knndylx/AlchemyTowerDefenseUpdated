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
using AlchemyTowerDefense.GameData;

namespace AlchemyTowerDefense
{
    public enum DrawingState
    {
        Tile = 0,
        Decoration = 1,
        Path = 2
    }

    public class EditorState: GameState
    {
        public Map Map { get; set; }
        public Texture2D brushTexture { get; set; }
        Texture2D mouseTexture;
        Texture2D highlightGridTexture;
        private DrawingState drawState;

        /// <summary>
        /// Initialize the map and parent game state manager
        /// </summary>
        /// <param name="g"></param>
        public override void Initialize(GameStateManager g)
        {
            Map = new Map(GlobalConfig.GameDimensions.Size);
            brushTexture = GlobalConfig.Textures.Tiles["blank"];
            highlightGridTexture = GlobalConfig.Textures.Icons["highlight"];
            mouseTexture = GlobalConfig.Textures.Icons["cursor"];
            UpdateDrawState();
            base.Initialize(g);
        }

        public override void Update()
        {
            UpdateDrawState();
            HandleInput();
        }

        /// <summary>
        /// Updates the drawing state depending on what kind of tool is selected
        /// </summary>
        private void UpdateDrawState()
        {
            if (GlobalConfig.Textures.Tiles.ContainsValue(brushTexture))
            {
                drawState = DrawingState.Tile;
            } else if (GlobalConfig.Textures.Decos.ContainsValue(brushTexture))
            {
                drawState = DrawingState.Decoration;
            } else if (GlobalConfig.Textures.Icons.ContainsValue(brushTexture))
            {
                drawState = DrawingState.Path;
            }
        }

        //TODO: make it so the highlight texture doesn't show up unless you're drawing a tile
        //TODO: load a blank map when switching from the main menu to the editor
        private void HandleInput()
        {
            //if the left mouse button is pressed then paint the tile onto the Map
            if (GlobalConfig.Input.currentMouseState[Util.MouseButtonsEnum.Left] == ButtonState.Pressed)
            {
                //if the brush is a tile
                if (drawState == DrawingState.Tile)
                {
                    int gridX = GlobalConfig.Input.gridx;
                    int gridY = GlobalConfig.Input.gridy;
                    if (Map.TerrainTiles[gridY, gridX].texture != brushTexture)
                        Map.ChangeTile(gridX, gridY, brushTexture);
                }
                //else if the brush is a decoration
                else if (drawState == DrawingState.Decoration)
                {
                    if(GlobalConfig.Input.previousMouseState[Util.MouseButtonsEnum.Left] == ButtonState.Released)
                    {
                        Map.PaintDecoration(GlobalConfig.Input.cursorx, GlobalConfig.Input.cursory, brushTexture);
                    }
                }
                else if (drawState == DrawingState.Path)
                {
                    if (GlobalConfig.Input.previousMouseState[Util.MouseButtonsEnum.Left] == ButtonState.Released)
                    {
                        Map.AddPathNode(GlobalConfig.Input.smallgridx, GlobalConfig.Input.smallgridy);
                    }
                }
            }

            if ((GlobalConfig.Input.currentButtonStates[Keys.Z] &&
                !GlobalConfig.Input.previousButtonStates[Keys.Z]) && drawState == DrawingState.Path)
            {
                Map.path.DeleteLastNode();
            }

            //transition from the canvas being active to the toolboxState being active
            if (GlobalConfig.Input.currentButtonStates[Keys.T] && 
                !GlobalConfig.Input.previousButtonStates[Keys.T])
            {
                ChangeState(GameStateEnum.Toolbox);
            }

            if (GlobalConfig.Input.currentButtonStates[Keys.Escape] && 
                !GlobalConfig.Input.previousButtonStates[Keys.Escape])
            {
                ChangeState(GameStateEnum.EditorPauseMenu);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            Map.path.Draw(spriteBatch);
            if(drawState == DrawingState.Tile)
                spriteBatch.Draw(highlightGridTexture, new Vector2(GlobalConfig.Input.gridx * GlobalConfig.GameDimensions.Size, GlobalConfig.Input.gridy * GlobalConfig.GameDimensions.Size), Color.White);
            else if(drawState == DrawingState.Path)
                spriteBatch.Draw(highlightGridTexture, new Rectangle((GlobalConfig.Input.smallgridx * GlobalConfig.GameDimensions.Size /2) - 16 ,
                                                                     (GlobalConfig.Input.smallgridy * GlobalConfig.GameDimensions.Size /2) - 16,
                                                                     GlobalConfig.GameDimensions.Size /2,
                                                                     GlobalConfig.GameDimensions.Size/2), Color.White);
            spriteBatch.Draw(mouseTexture, new Rectangle(GlobalConfig.Input.cursorx, GlobalConfig.Input.cursory, mouseTexture.Width, mouseTexture.Height), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
