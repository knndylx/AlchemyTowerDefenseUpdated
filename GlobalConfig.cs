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
        public static Sounds Sounds { get; private set; } = new Sounds();
        public static Dimensions GameDimensions { get; private set; }
        public static InputProcessor Input { get; private set; } = new InputProcessor();

        /// <summary>
        /// Initialize the configuration for the game
        /// </summary>
        /// <param name="c">Content Manager</param>
        /// <param name="w">Width of the screen</param>
        /// <param name="h">Height of the screen</param>
        /// <param name="s">Size of the tiles on the map</param>
        /// <param name="keys">All of the keys to use for every state of the game</param>
        public static void InitializeConfig(ContentManager c, int w, int h,int s, List<Keys> keys)
        {
            Textures.Initialize(c);
            Sounds.Initialize(c);
            GameDimensions = new Dimensions(w,h,s);
            Input.Initialize(keys);
        }

        /// <summary>
        /// Container for the screen size
        /// </summary>
        public class Dimensions
        {
            public int Width { get; private set; }
            public int Height { get; private set; }
            public int Size { get; private set; }
            public Dimensions(int w, int h, int s)
            {
                Width = w;
                Height = h;
                Size = s;
            }
        }
    }
}
