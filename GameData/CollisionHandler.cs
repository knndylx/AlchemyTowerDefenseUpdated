using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class CollisionHandler
    {
        public List<Enemy> enemies = new List<Enemy>();
        public List<Projectile> projectiles = new List<Projectile>();
        public List<Tower> towers = new List<Tower>();

        public void Update()
        {
            //check for collisions
            foreach (Tower t in towers)
            {
                List<Projectile> pCopy = new List<Projectile>(t.projectiles);
                foreach (Projectile p in pCopy)
                {
                    foreach (Enemy e in enemies)
                    {
                        if (!(p.rect.Right < e.rect.Left ||
                              p.rect.Left > e.rect.Right ||
                              p.rect.Top > e.rect.Bottom ||
                              p.rect.Bottom < e.rect.Top))
                        {
                            if (IntersectsPixel(p, e))
                            {
                                e.Collide(p, t.Damage);
                                t.projectiles.Remove(p);
                            }
                        }
                    }
                }
            }
        }

        private bool IntersectsPixel(Projectile p, Enemy e)
        {
            Rectangle rectA = p.rect;
            Rectangle rectB = e.rect;

            Color[] colorA = new Color[p.texture.Width * p.texture.Height];
            Color[] colorB = new Color[e.texture.Width * e.texture.Width];
            
            p.texture.GetData<Color>(colorA);
            e.texture.GetData<Color>(colorB);

            int top = Math.Max(rectA.Top, rectB.Top);
            int bottom = Math.Min(rectA.Bottom, rectB.Bottom);
            int left = Math.Max(rectA.Left, rectB.Left);
            int right = Math.Min(rectA.Right, rectB.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = colorA[(x - rectA.Left) + (y - rectA.Top) * rectA.Width];
                    Color color2 = colorB[(x - rectB.Left) + (y - rectB.Top) * rectB.Width];

                    if (color1.A != 0 && color2.A != 0)
                        return true;
                }
            }

            return false;
        }

        public void OnProjectileFired(Projectile p)
        {
            projectiles.Add(p);
        }
    }
}
