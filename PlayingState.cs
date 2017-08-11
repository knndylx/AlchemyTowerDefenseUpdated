using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.GameData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AlchemyTowerDefense
{
    public class PlayingState: GameState
    {

        private Map map = new Map(GlobalConfig.GameDimensions.Size);

        /// <summary>
        /// Initializes the playing state to load the map from the default file
        /// </summary>
        /// TODO: make a loading dialog so that the file doesn't have to be default
        public PlayingState()
        {
            map = new Map(GlobalConfig.GameDimensions.Size,"map.txt");
        }

        /// <summary>
        /// Draws the map to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
        }
    }
}
