using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AlchemyTowerDefense
{
    public static class GlobalConfig
    {
        public static Textures Textures { get; private set; } = new Textures();
        public static ScreenSize Screen { get; private set; }
        public static InputProcessor Input { get; private set; } = new InputProcessor();

        /// <summary>
        /// Initialize the configuration for the game
        /// </summary>
        /// <param name="c">Content Manager</param>
        /// <param name="w">Width of the screen</param>
        /// <param name="h">Height of the screen</param>
        /// <param name="keys">All of the keys to use for every state of the game</param>
        public static void InitializeConfig(ContentManager c, int w, int h, List<Keys> keys)
        {
            Textures.Initialize(c);
            Screen = new ScreenSize(w,h);
            Input.Initialize(keys);
        }

        /// <summary>
        /// Container for the screen size
        /// </summary>
        public class ScreenSize
        {
            public int Width { get; private set; }
            public int Height { get; private set; }

            public ScreenSize(int w, int h)
            {
                Width = w;
                Height = h;
            }
        }
    }
}
