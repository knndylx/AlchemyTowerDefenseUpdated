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
using AlchemyTowerDefense.Editor;

namespace AlchemyTowerDefense
{
    public class EditorState: GameState
    {
        public Map Map { get; set; }
        public Texture2D brushTexture { get; set; }
        Texture2D mouseTexture;
        Texture2D highlightGridTexture;

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
            base.Initialize(g);
        }

        public override void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            //if the left mouse button is pressed then paint the tile onto the Map
            if (GlobalConfig.Input.currentMouseState[Util.MouseButtonsEnum.Left] == ButtonState.Pressed)
            {
                //if the brush is a tile
                if (GlobalConfig.Textures.Tiles.ContainsValue(brushTexture))
                {
                    int gridX = GlobalConfig.Input.gridx;
                    int gridY = GlobalConfig.Input.gridy;
                    if (Map.TerrainTiles[gridY, gridX].texture != brushTexture)
                        Map.ChangeTile(gridX, gridY, brushTexture);
                }
                //else if the brush is a decoration
                else if (GlobalConfig.Textures.Decos.ContainsValue(brushTexture))
                {
                    if(GlobalConfig.Input.previousMouseState[Util.MouseButtonsEnum.Left] == ButtonState.Released)
                    {
                        Map.PaintDecoration(GlobalConfig.Input.cursorx, GlobalConfig.Input.cursory, brushTexture);
                    }
                }
            }

            //transition from the canvas being active to the toolboxState being active
            if(GlobalConfig.Input.currentButtonStates[Keys.T] && 
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
            spriteBatch.Draw(highlightGridTexture, new Vector2(GlobalConfig.Input.gridx * GlobalConfig.GameDimensions.Size, GlobalConfig.Input.gridy * GlobalConfig.GameDimensions.Size), Color.White);
            spriteBatch.Draw(mouseTexture, new Rectangle(GlobalConfig.Input.cursorx, GlobalConfig.Input.cursory, mouseTexture.Width, mouseTexture.Height), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
