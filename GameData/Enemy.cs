using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Enemy
    {
        private enum Direction
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3
        }

        private int speed;
        private Direction dir;

        public Texture2D texture;
        public Color[] textureData;

        public Rectangle rect { get; private set; }
        private Vector2 pos;
        private Path path;
        private int Size;
        private Path.PathNode currentNode;

        public bool Dead { get; private set; } = false;
        public int StartHealth { get; private set; } = 100;
        public double Health { get; private set; }
        public HealthBar healthBar;

        public Enemy(Path p)
        {
            path = p;
            currentNode = p.startNode;
            Size = GlobalConfig.GameDimensions.Size;
            //starting position is the start of the path
            pos = new Vector2(path.startNode.x, path.startNode.y);
            texture = GlobalConfig.Textures.Enemies["enemy1"];
            rect = new Rectangle((int)pos.X - Size/2, (int)pos.Y - Size/2, Size, Size);
            dir = Direction.Down;
            speed = 1;
            Health = StartHealth;
            healthBar = new HealthBar(this);

            //load the texture data for collisions
            textureData = new Color[texture.Width* texture.Height];
            texture.GetData(textureData);
        }

        private void Move()
        {
            switch (dir)
            {
                case Direction.Down:
                    pos = new Vector2(pos.X, pos.Y + speed);
                    rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    break;
                case Direction.Left:
                    pos = new Vector2(pos.X - speed, pos.Y);
                    rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    break;
                case Direction.Up:
                    pos = new Vector2(pos.X, pos.Y - speed);
                    rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    break;
                case Direction.Right:
                    pos = new Vector2(pos.X + speed, pos.Y);
                    rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    break;
            }
        }

        private void UpdateDirection()
        {
            if (currentNode.nextNode == null) return;
            if (currentNode.nextNode.x == currentNode.x)
            {
                dir = currentNode.nextNode.y < currentNode.y ? Direction.Up : Direction.Down;
            }
            else if (currentNode.nextNode.y == currentNode.y)
            {
                dir = currentNode.nextNode.x < currentNode.x ? Direction.Left : Direction.Right;
            }
        }

        private void UpdateNode()
        {
            if (currentNode.nextNode == null) return;
            switch (dir)
            {
                case Direction.Down:
                    if (pos.Y > currentNode.nextNode.y)
                    {
                        currentNode = currentNode.nextNode;
                        pos = new Vector2(currentNode.x, currentNode.y);
                        rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    }
                    break;
                case Direction.Left:
                    if (pos.X < currentNode.nextNode.x)
                    {
                        currentNode = currentNode.nextNode;
                        pos = new Vector2(currentNode.x, currentNode.y);
                        rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    }
                    break;
                case Direction.Up:
                    if (pos.Y < currentNode.nextNode.y)
                    {
                        currentNode = currentNode.nextNode;
                        pos = new Vector2(currentNode.x, currentNode.y);
                        rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    }
                    break;
                case Direction.Right:
                    if (pos.X > currentNode.nextNode.x)
                    {
                        currentNode = currentNode.nextNode;
                        pos = new Vector2(currentNode.x, currentNode.y);
                        rect = new Rectangle((int)pos.X - Size / 2, (int)pos.Y - Size / 2, Size, Size);
                    }
                    break;
            }
            
        }

        public void Update()
        {
            Dead = Finish() || Health <= 0;
            Move();
            UpdateNode();
            UpdateDirection();
            healthBar.Update();
        }

        public void Collide(Projectile p, int damage)
        {
            Health -= damage;
            Console.WriteLine(Health);
            GlobalConfig.Sounds.Effects["qubodupImpactStone"].Play();
        }

        public bool Finish()
        {
            return currentNode == path.GetLastNode();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, null, Color.White, MathHelper.PiOver2 * (int)dir, new Vector2(texture.Width/2, texture.Height/2), 1f, SpriteEffects.None, 1);
            healthBar.Draw(spriteBatch);
        }
    }
}