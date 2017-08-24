using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Util
{
    public class Sounds
    {
        //Sound data
        public Dictionary<string, SoundEffect> Effects { get; private set; } = new Dictionary<string, SoundEffect>();

        public void Initialize(ContentManager c)
        {
            Effects = LoadSoundContent("sounds", c);
        }

        private Dictionary<string, SoundEffect> LoadSoundContent(string contentFolder, ContentManager c)
        {
            Dictionary<string, SoundEffect> sd = new Dictionary<string, SoundEffect>();

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

                sd[key] = c.Load<SoundEffect>(contentFolder + "/" + key);
            }

            return sd;
        }
    }
}
