using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense
{
    public class GameStateManager
    {
        //public int State { get; private set; }

        private List<GameState> states = new List<GameState>();
        private GameState currentState;

        /// <summary>
        /// Initialize the game states. The order that these are added is very important
        /// and has to correspond to the GameStateEnum for proper state changing behavior
        /// </summary>
        public GameStateManager()
        {
            //add the three states to the states list
            MenuState mainmenu = new MenuState();
            states.Add(mainmenu);
            //mainmenu.StateChangeEvent += OnStateChange;

            states.Add(new PlayingState());

            EditorState editor = new EditorState();
            //editor.StateChangeDelegate += OnStateChange;
            states.Add(editor);

            PauseMenuState editorPauseMenu = new PauseMenuState();
            states.Add(editorPauseMenu);
            //editorPauseMenu.StateChangeEvent += OnStateChange;

            currentState = states[(int)GameStateEnum.MainMenu];
        }

        public void Initialize()
        {
            foreach(GameState gs in states)
            {
                gs.Initialize(this);
            }
        }
        public void LoadContent(ContentManager c)
        {
            foreach(GameState gs in states)
            {
                gs.LoadContent(c);
            }
        }

        public void Update()
        {
            currentState.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch);
        }

        //public void OnStateChange(int i)
        //{
        //    Console.Write("state changed " + i + "//");
        //    State = i;
        //    currentState = states[State];
        //}

        public void ChangeActiveState(GameStateEnum i)
        {
            currentState = states[(int)i];
        }

    }
}
