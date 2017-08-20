using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Projectile
    {
        public Texture2D texture;
        public Color[] textureData;
        public Vector2 pos;
        public Vector2 velocity;
        public int maxSpeed;
        private Enemy target;


        public Rectangle rect;

        public Projectile(int x, int y, Enemy target)
        {
            texture = GlobalConfig.Textures.Icons["bullet"];
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            pos = new Vector2(x,y);
            this.target = target;
            maxSpeed = 5;
            Target();
        }

        /// <summary>
        /// Changes the velocity of the projectile at it's max speed homing on the target
        /// </summary>
        private void Target()
        {
            double y = (target.rect.Center.Y - rect.Center.Y);
            double x = (target.rect.Center.X - rect.Center.X);

            double angleToTarget = Math.Atan(Math.Abs(y)/Math.Abs(x));

            float xVel = (float)(5 * Math.Cos(angleToTarget));
            float yVel = (float)(5 * Math.Sin(angleToTarget));

            //if x > 0
            if (x > 0)
            {
                //if y > 0
                if (y > 0)
                {
                    
                }
                //else if y <= 0
                else
                {
                    yVel *= -1;
                }
            }
            //else if x <= 0
            else
            {
                //if y > 0
                if (y > 0)
                {
                    xVel *= -1;
                }
                //else if y <= 0
                else
                {
                    xVel *= -1;
                    yVel *= -1;
                }
            }
            velocity = new Vector2(xVel,yVel);
        }

        public void Update()
        {
            Target();
            pos = new Vector2(pos.X + velocity.X, pos.Y + velocity.Y);
            rect = new Rectangle((int)(pos.X), (int)(pos.Y), texture.Width, texture.Height);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
