using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace AlchemyTowerDefense.Util
{
    /// <summary>
    /// Processes the previous frame and current frame input for all required buttons and the mouse
    /// </summary>
    public class InputProcessor
    {
        //keyboard button state dictionaries
        public Dictionary<Keys, bool> previousButtonStates = new Dictionary<Keys, bool>();
        public Dictionary<Keys, bool> currentButtonStates = new Dictionary<Keys, bool>();

        //mouse button state dictionaries
        public Dictionary<Util.MouseButtonsEnum, ButtonState> previousMouseState = new Dictionary<Util.MouseButtonsEnum, ButtonState>();
        public Dictionary<Util.MouseButtonsEnum, ButtonState> currentMouseState = new Dictionary<Util.MouseButtonsEnum, ButtonState>();

        //mouse scroll wheel values
        public int currentScrollWheel, previousScrollWheel;

        //states of the mouse and keyboard
        KeyboardState keyState;
        MouseState mouseState;

        /// <summary>
        /// Puts the keys that you want to process into the dictionary. Loads the default button states.
        /// </summary>
        /// <param name="keyButtons">List of Keyboard keys that are processed and updated.</param>
        public void Initialize(List<Keys> keyButtons)
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            //initialize keyboard
            foreach (Keys k in keyButtons)
            {
                previousButtonStates.Add(k, keyState.IsKeyDown(k));
            }
            currentButtonStates = new Dictionary<Keys, bool>(previousButtonStates);

            //initialize mouse
            previousMouseState.Add(Util.MouseButtonsEnum.Right, mouseState.RightButton);
            previousMouseState.Add(Util.MouseButtonsEnum.Left, mouseState.LeftButton);
            previousScrollWheel = mouseState.ScrollWheelValue;
            currentScrollWheel = previousScrollWheel;
            currentMouseState = new Dictionary<Util.MouseButtonsEnum, ButtonState>(previousMouseState);
        }

        /// <summary>
        /// Update the previous and current state of the mouse and keyboard
        /// </summary>
        public void Update()
        {
            //update the previous state
            previousScrollWheel = currentScrollWheel;
            previousMouseState = new Dictionary<Util.MouseButtonsEnum, ButtonState>(currentMouseState);
            previousButtonStates = new Dictionary<Keys, bool>(currentButtonStates);

            //get the new state
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            List<Keys> kl = new List<Keys>(currentButtonStates.Keys);

            //update keyboard
            foreach(Keys k in kl)
            {
                currentButtonStates[k] = keyState.IsKeyDown(k);
            }

            //update mouse
            currentScrollWheel = mouseState.ScrollWheelValue;
            currentMouseState[Util.MouseButtonsEnum.Right] = mouseState.RightButton;
            currentMouseState[Util.MouseButtonsEnum.Left] = mouseState.LeftButton;
        }

    }
}
