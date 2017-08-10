using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace AlchemyTowerDefense.Util
{
    //processes and stores the current and previous state of the buttons on the mouse and keyboard
    public class InputProcessor
    {

        public Dictionary<Keys, bool> previousButtonStates = new Dictionary<Keys, bool>();
        public Dictionary<Keys, bool> currentButtonStates = new Dictionary<Keys, bool>();
        public int currentScrollWheel, previousScrollWheel;

        public Dictionary<Util.MouseButtons, ButtonState> previousMouseState = new Dictionary<Util.MouseButtons, ButtonState>();
        public Dictionary<Util.MouseButtons, ButtonState> currentMouseState = new Dictionary<Util.MouseButtons, ButtonState>();

        KeyboardState keyState;
        MouseState mouseState;

        //get the keys that you want to process in this instance and put them into the dictionary
        //loads the default button states when the input processor is created
        //initialize with the argument that is the list of the keys that you want to process
        //BUG: only the left and right mouse button are taken into consideration right now
        public void Initialize(List<Keys> keyButtons)
        {
            //Console.Write("initializing" + keyButtons.ToString());
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            foreach (Keys k in keyButtons)
            {
                previousButtonStates.Add(k, keyState.IsKeyDown(k));
            }
            currentButtonStates = new Dictionary<Keys, bool>(previousButtonStates);
            previousMouseState.Add(Util.MouseButtons.Right, mouseState.RightButton);
            previousMouseState.Add(Util.MouseButtons.Left, mouseState.LeftButton);
            previousScrollWheel = mouseState.ScrollWheelValue;
            currentScrollWheel = previousScrollWheel;
            currentMouseState = new Dictionary<Util.MouseButtons, ButtonState>(previousMouseState);
        }

        public void Update()
        {
            previousScrollWheel = currentScrollWheel;
            previousMouseState = new Dictionary<Util.MouseButtons, ButtonState>(currentMouseState);
            previousButtonStates = new Dictionary<Keys, bool>(currentButtonStates);

            KeyboardState keyState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            List<Keys> kl = new List<Keys>(currentButtonStates.Keys);

            foreach(Keys k in kl)
            {
                currentButtonStates[k] = keyState.IsKeyDown(k);
            }
            currentScrollWheel = mState.ScrollWheelValue;
            currentMouseState[Util.MouseButtons.Right] = mState.RightButton;
            currentMouseState[Util.MouseButtons.Left] = mState.LeftButton;
            //Console.WriteLine(string.Format("Current mouse state: {0} // Previous mouse state: {1}", currentMouseState[Util.MouseButtons.Left], previousMouseState[Util.MouseButtons.Left]));
        }

    }
}
