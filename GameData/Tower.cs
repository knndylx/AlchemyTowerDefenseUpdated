using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Tower
    {
        public int Damage { get; private set; }

        /// <summary>
        /// lower is faster!
        /// </summary>
        public int RateOfFire { get; private set; }
        public int FramesSinceLastFire { get; private set; }

        private Texture2D texture;
        public Rectangle rect;

        public List<Projectile> projectiles = new List<Projectile>();

        public int range = 1000;
        public List<Enemy> enemiesInRange = new List<Enemy>();


        public Tower(int d, int rof, int x, int y)
        {
            Damage = d;
            RateOfFire = rof;
            texture = GlobalConfig.Textures.Towers["tower1"];
            rect = new Rectangle(x * GlobalConfig.GameDimensions.Size, y * GlobalConfig.GameDimensions.Size, 64, 64);
        }

        public void Fire(Enemy target)
        {
            projectiles.Add(new Projectile(rect.Center.X, rect.Center.Y,target));
            GlobalConfig.Sounds.Effects["Futuristic Sniper Rifle Single Shot"].Play();
        }

        public void Update()
        {
            if (FramesSinceLastFire == RateOfFire)
            {
                if (enemiesInRange.Count != 0)
                {
                    Fire(enemiesInRange[0]);
                    FramesSinceLastFire = 0;
                }
            }
            else
            {
                FramesSinceLastFire++;
            }
            foreach (Projectile p in projectiles)
            {
                p.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
            foreach (Projectile p in projectiles)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}