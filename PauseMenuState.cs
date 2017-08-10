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
    public class PauseMenuState: GameState
    {
        //Menu menu;
        private int buttonCursorIndex = 0;

        private List<Keys> kList = new List<Keys>
        {
            Keys.Up,
            Keys.Down,
            Keys.Enter,
            Keys.Escape
        };

        private bool isFirstFrame = true;

        public Menu Menu { get; private set; }

        private EditorState _previousState;
        //Util.Button gamebutton;
        //Util.Button editorbutton;

        public PauseMenuState(EditorState ps)
        {
            _previousState = ps;
        }


        public delegate void PressDelegate(int i);
        public event PressDelegate StateChangeEvent;

        public void Press()
        {
            switch (buttonCursorIndex)
            {
                case 2:
                    StateChangeEvent?.Invoke(0);
                    break;
                case 1:
                    Console.Write("Loading");
                    _previousState.Map.LoadFromFile("Map.txt");
                    break;
                default:
                    _previousState.Map.SaveToFile("Map.txt");
                    break;
            }
        }

        public override void Initialize()
        {
            Menu = new Menu(ScreenWidth, ScreenHeight);
            //menu.Initialize();
            base.LoadButtonStates(kList);
            base.Initialize();
        }


        public override void LoadContent(ContentManager c)
        {
            //Console.Write("load content in menu state class");
            Menu.LoadContent(c, new List<string>
            {
                "save",
                "load",
                "mainmenu"
            });
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
            for (var i = 0; i < Menu.buttons.Count; i++)
            {
                var currentButton = Menu.buttons[i];
                if (i == buttonCursorIndex) currentButton.Select();
                else currentButton.Deselect();
            }
        }

        public void ProcessInput()
        {
            previousButtonStates = new Dictionary<Keys, bool>(currentButtonStates);
            var keyState = Keyboard.GetState();
            currentButtonStates[Keys.Up] = keyState.IsKeyDown(Keys.Up);
            currentButtonStates[Keys.Down] = keyState.IsKeyDown(Keys.Down);
            currentButtonStates[Keys.Enter] = keyState.IsKeyDown(Keys.Enter);
            currentButtonStates[Keys.Escape] = keyState.IsKeyDown(Keys.Escape);

            //if the up button was pressed
            if(currentButtonStates[Keys.Up] && !previousButtonStates[Keys.Up])
            {
                if (buttonCursorIndex != 0) buttonCursorIndex--;
            }
            //else if the down button was pressed
            else if(currentButtonStates[Keys.Down] && !previousButtonStates[Keys.Down])
            {
                if (buttonCursorIndex != Menu.buttons.Count - 1) buttonCursorIndex++;
            }
            //else if the enter button was pressed
            else if(currentButtonStates[Keys.Enter] && !previousButtonStates[Keys.Enter])
            {
                Press();
            }

            if (currentButtonStates[Keys.Escape] && !previousButtonStates[Keys.Escape] && !isFirstFrame)
            {
                Console.Write("escape pressed");
                StateChangeEvent?.Invoke(2);
            }
            isFirstFrame = false;
        }



        //draw all buttons
        public override void Draw(SpriteBatch spriteBatch)
        {
            Menu.Draw(spriteBatch);
        }
    }
}
