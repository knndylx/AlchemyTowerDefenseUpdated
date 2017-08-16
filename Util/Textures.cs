using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Util
{
    public class Textures
    {
        //These hold all applicable texture data
        public Dictionary<string, Texture2D> Tiles { get; private set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Buttons { get; private set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Decos { get; private set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Icons { get; private set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Toolbox { get; private set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> Enemies { get; private set; } = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Build the texture dictionaries
        /// </summary>
        /// <param name="c">Original content manager that contains the texture information</param>
        public void Initialize(ContentManager c)
        {
            Tiles = LoadTextureContent("tiles", c);
            Buttons = LoadTextureContent("buttons", c);
            Decos = LoadTextureContent("decos", c);
            Icons = LoadTextureContent("icons", c);
            Toolbox = LoadTextureContent("toolbox", c);
            Enemies = LoadTextureContent("enemies", c);
        }

        /// <summary>
        /// Loads the texture content. Helper method for loading all of the dictionaries
        /// </summary>
        /// <param name="contentFolder">Sub-folder that content loads from</param>
        /// <param name="c">Original content manager that contains the texture information</param>
        /// <returns></returns>
        private Dictionary<string, Texture2D> LoadTextureContent(string contentFolder, ContentManager c)
        {
            Dictionary<string, Texture2D> td = new Dictionary<string, Texture2D>();

            DirectoryInfo dir = new DirectoryInfo(c.RootDirectory + "/" + contentFolder);
            if (!dir.Exists)
            {
                Console.Write(contentFolder);
                throw new DirectoryNotFoundException();
            }
            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                td[key] = c.Load<Texture2D>(contentFolder + "/" + key);
            }

            return td;
        }
    }
}
