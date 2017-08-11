using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AlchemyTowerDefense
{
    public class MenuState: GameState
    {
        private int buttonCursorIndex = 0;
        public Menu menu = new Menu();
        

        public void Initialize(GameStateManager g, List<string> buttonStringList)
        {
            menu.Initialize(buttonStringList);
            base.Initialize(g);
        }

        public override void Update()
        {
            ProcessInput();
            SelectButton();
            base.Update();
        }

        private void SelectButton()
        {
            //make the button selected
            for (int i = 0; i < menu.buttons.Count; i++)
            {
                var currentButton = menu.buttons[i];
                if (i == buttonCursorIndex) currentButton.Highlight();
                else currentButton.Dehighlight();
            }
        }

        public void ProcessInput()
        {
            //if the up button was pressed
            if(GlobalConfig.Input.currentButtonStates[Keys.Up] && 
                !GlobalConfig.Input.previousButtonStates[Keys.Up])
            {
                if (buttonCursorIndex != 0) buttonCursorIndex--;
            }
            //else if the down button was pressed
            else if(GlobalConfig.Input.currentButtonStates[Keys.Down] && 
                !GlobalConfig.Input.previousButtonStates[Keys.Down])
            {
                if (buttonCursorIndex != menu.buttons.Count - 1) buttonCursorIndex++;
            }
            //else if the enter button was pressed
            else if(GlobalConfig.Input.currentButtonStates[Keys.Enter] && 
                !GlobalConfig.Input.previousButtonStates[Keys.Enter])
            {
                menu.buttons[buttonCursorIndex].Click();
            }
        }



        /// <summary>
        /// Draw the menu
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
