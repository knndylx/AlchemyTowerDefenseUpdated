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


namespace AlchemyTowerDefense
{
    public class PauseMenuState: MenuState
    {
        private EditorState ParentEditorState;

        public void Initialize(GameStateManager g, List<string> buttonStringList, EditorState e)
        {
            ParentEditorState = e;

            //initialize the menu
            base.Initialize(g, buttonStringList);

            //wire up the buttons
            menu.buttons[0].ClickEvent += OnSaveButtonClick;
            menu.buttons[1].ClickEvent += OnLoadButtonClick;
            menu.buttons[2].ClickEvent += OnBackButtonClick;
        }

        //events for the buttons

        public void OnSaveButtonClick()
        {
            ParentEditorState.Map.LoadFromFile("map.txt");
        }

        public void OnLoadButtonClick()
        {
            ParentEditorState.Map.SaveToFile("map.txt");
        }

        public void OnBackButtonClick()
        {
            ChangeState(GameStateEnum.Editor);
        }

        //Draw all buttons
        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
