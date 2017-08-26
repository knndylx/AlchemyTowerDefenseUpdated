using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.Util
{
    public class Animation
    {
        private int frameCounter = -1;
        private int secondaryCounter = 0;
        private int amountOfFrames;
        public bool active { get; private set; } = true;

        //private Vector2 pos;
        private Rectangle rect;
        private Texture2D texture;

        public Animation(Texture2D texture, int x, int y, int size)
        {
            this.texture = texture;
            amountOfFrames = texture.Width / 32;
            rect = new Rectangle(x, y, size, size);
        }

        public void Update()
        {
            frameCounter++;
            if (frameCounter > amountOfFrames - 1) active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect,new Rectangle(frameCounter*texture.Height,0,texture.Height,texture.Height),Color.White);
        }
    }
}
