using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.States.Menus
{
    public class MainMenuState : MenuState
    {
        private EditorState ParentEditorState;

        /// <summary>
        /// Initialize the Pause Menu
        /// </summary>
        /// <param name="g">Parent Game State Manager</param>
        /// <param name="e">Parent Editor State</param>
        public void Initialize(GameStateManager g, EditorState e)
        {
            ParentEditorState = e;

            //initialize the menu
            base.Initialize(g);

            //initialize the menu's buttons
            base.menu.Initialize(new List<string>()
            {
                "gamebutton",
                "editorbutton",
            });

            //wire up the buttons
            menu.buttons[0].ClickEvent += OnGameButtonClick;
            menu.buttons[1].ClickEvent += OnEditorButtonClick;
        }

        /// <summary>
        /// Changes the state to the game
        /// </summary>
        public void OnGameButtonClick()
        {
            ChangeState(GameStateEnum.Playing);
        }

        /// <summary>
        /// Changes the state to the editor
        /// </summary>
        public void OnEditorButtonClick()
        {
            ChangeState(GameStateEnum.Editor);
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
