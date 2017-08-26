using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Level
    {
        public Map map = new Map(GlobalConfig.GameDimensions.Size, "Map.txt");
        public List<Enemy> enemies = new List<Enemy>();
        public List<Tower> towers = new List<Tower>();
        public CollisionHandler CH = new CollisionHandler();
        public List<Projectile> projectiles = new List<Projectile>();
        public Animation animateTest;


        public Level()
        {
            enemies.Add(new Enemy(map.path));
            towers.Add(new Tower(5,50, 4, 4));
            towers.Add(new Tower(5, 70, 4,5));
            foreach (Tower t in towers)
            {
                CH.towers.Add(t);
            }
            foreach (Enemy e in enemies)
            {
                CH.enemies.Add(e);
            }
        }

        public void Update()
        {
            List<Enemy> newEnemyList = new List<Enemy>(enemies);
            foreach (Enemy e in enemies)
            {
                e.Update();
                if (e.Dead)
                {
                    newEnemyList.Remove(e);
                }
                enemies = newEnemyList;
                if (enemies.Count == 0)
                {
                    enemies.Add(new Enemy(map.path));
                    CH.enemies.Add(enemies[0]);
                }
            }
            foreach (Tower t in towers)
            {
                UpdateTowerEnemiesInRange(t);
                t.Update();
            }
            CH.Update();
        }

        public void UpdateTowerEnemiesInRange(Tower t)
        {
            t.enemiesInRange.Clear();
            foreach (Enemy e in enemies)
            {
                double distanceToEnemy = Math.Sqrt(Math.Pow((t.rect.Center.X - e.rect.Center.X), 2) +
                                                  Math.Pow((t.rect.Center.Y - e.rect.Center.Y), 2));
                if (distanceToEnemy <= t.range)
                {
                    t.enemiesInRange.Add(e);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }
            foreach (Tower t in towers)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
