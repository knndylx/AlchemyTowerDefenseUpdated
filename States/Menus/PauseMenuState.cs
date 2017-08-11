using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AlchemyTowerDefense.States.Menus
{
    public class PauseMenuState: MenuState
    {
        private EditorState ParentEditorState;

        /// <summary>
        /// Initialize the Pause Menu
        /// </summary>
        /// <param name="g">Parent Game State Manager</param>
        public void Initialize(GameStateManager g, EditorState e)
        {
            ParentEditorState = e;

            //initialize the menu
            base.Initialize(g);

            //initialize the menu's buttons
            base.menu.Initialize(new List<string>()
                                 {
                                    "save",
                                    "load",
                                    "mainmenu"
                                 });

            //wire up the buttons
            menu.buttons[0].ClickEvent += OnSaveButtonClick;
            menu.buttons[1].ClickEvent += OnLoadButtonClick;
            menu.buttons[2].ClickEvent += OnMainMenuButtonClick;
        }

        /// <summary>
        /// Saves the editor's map
        /// </summary>
        /// TODO: make the save and load variable so you can select a file
        public void OnSaveButtonClick()
        {
            ParentEditorState.Map.LoadFromFile("map.txt");
        }

        /// <summary>
        /// Loads the editor's map
        /// </summary>
        public void OnLoadButtonClick()
        {
            ParentEditorState.Map.SaveToFile("map.txt");
        }

        /// <summary>
        /// Changes the state back to the main menu
        /// </summary>
        public void OnMainMenuButtonClick()
        {
            ChangeState(GameStateEnum.MainMenu);
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
