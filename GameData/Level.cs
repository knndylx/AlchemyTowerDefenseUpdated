using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Level
    {
        public Map map = new Map(GlobalConfig.GameDimensions.Size, "Map.txt");
        public List<Enemy> enemies = new List<Enemy>();

        public Level()
        {
            enemies.Add(new Enemy(map.path));
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
        }
    }
}
