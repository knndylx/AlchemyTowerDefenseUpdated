using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class HealthBar
    {
        public Enemy parent;
        public Texture2D barTexture;
        double[] partitions = new double[8];
        private Rectangle rect;
        

        public HealthBar(Enemy e)
        {
            parent = e;
            barTexture = GlobalConfig.Textures.Enemies["health"];

            //set up the partitions for checking which texture to show
            double split = parent.Health / 8;
            for (int i = 0; i < 8; i++)
            {
                partitions[i] = split * (i + 1);
            }
            rect = new Rectangle(0,0,0,0);
        }

        private int GetTextureYOffset()
        {
            double health = parent.Health;
            int rowSpacing = 8;
            int pixelOffset = 0;
            //number of pixels in between each texture in the health bar
            for(int i = 0; i < 8; i++)
            {
                if (health <= partitions[i])
                {
                    pixelOffset = barTexture.Height - (rowSpacing * (i + 1));
                    return pixelOffset;
                }
            }
            return -1;

        }

        private void SetRect()
        {
            //get the center position from the parent rectangle on the y axis
            int center = parent.rect.Left + parent.rect.Width/2;
            //set the rectangle's position and size on top of the enemy in the center of their pos
            rect = new Rectangle(center - (barTexture.Width/2),
                                 parent.rect.Top,
                                 20,
                                 8);
        }

        public void Update()
        {
            SetRect();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0,GetTextureYOffset(), 20,8);
            spriteBatch.Draw(barTexture, rect, sourceRectangle, Color.White);
        }
    }
}
