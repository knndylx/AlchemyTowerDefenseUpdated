using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using AlchemyTowerDefense.Util;
using AlchemyTowerDefense.Util.Buttons;

namespace AlchemyTowerDefense.States.Menus
{
    public class Menu
    {
        //buttons on the screen
        public List<MenuButton> buttons = new List<MenuButton>();

        /// <summary>
        /// Buttons are constructed from a list of strings that are the title and texture of the button
        /// </summary>
        /// <param name="buttonStringList">List of strings that are buttons names and textures</param>
        public void Initialize(List<string> buttonStringList)
        {
            foreach (var s in buttonStringList)
            {
                buttons.Add(MakeButton(s, 300, 100));
            }
            FinalizeButtons();
        }

        /// <summary>
        /// Preliminarily add button to the list of buttons. Must also be called with finalize buttons
        /// once all buttons are added
        /// </summary>
        /// <param name="buttonName">Name of the button</param>
        /// <param name="buttonWidth">Width of the button</param>
        /// <param name="buttonHeight">Height of the button</param>
        /// <returns></returns>
        public MenuButton MakeButton(string buttonName, int buttonWidth, int buttonHeight)
        {
            Texture2D texture = GlobalConfig.Textures.Buttons[buttonName];
            MenuButton button = new MenuButton(texture, new Rectangle(0, 0, buttonWidth, buttonHeight));
            return button;
        }

        /// <summary>
        /// Called once all of the buttons are added to the list using MakeButton.
        /// Centers the buttons horizontally on the screen and spaces them evenly vertically
        /// </summary>
        public void FinalizeButtons()
        {
            for(var i = 0; i < buttons.Count; i++)
            {
                var b = buttons[i];
                b.ChangeRect(new Rectangle(GlobalConfig.GameDimensions.Width / 2 - b.Rect.Width / 2,
                                          (GlobalConfig.GameDimensions.Height / (buttons.Count + 1)) + ((GlobalConfig.GameDimensions.Height / (buttons.Count + 1)) * i) - (b.Rect.Height / 2),
                                          b.Rect.Width,
                                          b.Rect.Height));
            }
        }

        /// <summary>
        /// Draw all buttons
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
