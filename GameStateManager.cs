using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.States.Menus;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using AlchemyTowerDefense.Editor;

namespace AlchemyTowerDefense
{
    public class GameStateManager
    {
        //public int State { get; private set; }

        private List<GameState> states = new List<GameState>();
        private GameState currentState;

        MainMenuState mainMenuState = new MainMenuState();
        PlayingState playingState = new PlayingState();
        PauseMenuState gamePausePlaceholder = new PauseMenuState();
        PauseMenuState editorPauseMenuState = new PauseMenuState();
        ToolboxState toolboxState = new ToolboxState();
        EditorState editorState = new EditorState();

        /// <summary>
        /// Initialize the game states. The order that these are added is very important
        /// and has to correspond to the GameStateEnum for proper state changing behavior
        /// </summary>
        public GameStateManager()
        {
            states.Add(mainMenuState);
            states.Add(playingState);
            states.Add(gamePausePlaceholder);
            states.Add(editorPauseMenuState);
            states.Add(toolboxState);
            states.Add(editorState);
            currentState = states[(int)GameStateEnum.MainMenu];
        }

        public void Initialize()
        {
            mainMenuState.Initialize(this);
            playingState.Initialize(this);
            gamePausePlaceholder.Initialize(this, editorState);
            editorPauseMenuState.Initialize(this, editorState);
            toolboxState.Initialize(this);
            editorState.Initialize(this);
        }
        public void LoadContent(ContentManager c)
        {
            //foreach(GameState gs in states)
            //{
            //    gs.LoadContent(c);
            //}
        }

        /// <summary>
        /// Update the current game state
        /// </summary>
        public void Update()
        {
            currentState.Update();
        }

        /// <summary>
        /// Draws the current game State to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
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

        /// <summary>
        /// Change the active state
        /// </summary>
        /// <param name="i"></param>
        public void ChangeActiveState(GameStateEnum i)
        {
            currentState = states[(int)i];
        }

    }
}
