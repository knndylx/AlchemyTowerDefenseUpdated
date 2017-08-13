using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Path
    {
        /// <summary>
        /// Nodes are what connect each segment of the path to each other
        /// </summary>
        public class PathNode
        {
            private int x, y;
            public PathNode previousNode, nextNode;
            private Texture2D texture = GlobalConfig.Textures.Icons["pathnode"];

            /// <summary>
            /// Draws the node in a color that depends on if its a start node, end node, or in the middle
            /// </summary>
            /// <param name="spriteBatch"></param>
            public void Draw(SpriteBatch spriteBatch)
            {
                if (previousNode == null)
                {
                    spriteBatch.Draw(texture, new Rectangle(x,y,texture.Width,texture.Height), Color.Green);
                } else if (nextNode == null)
                {
                    spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.Red);
                }
                else
                {
                    spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.White);
                }
            }
        }

        private PathNode startNode;

        /// <summary>
        /// Connects two nodes together if the connection is value
        /// </summary>
        /// <param name="s">Starting node</param>
        /// <param name="e">Ending Node</param>
        public void MakeSegment(PathNode s, PathNode e)
        {
            if (TestConnectionValid(s,e))
            {
                s.nextNode = e;
                e.previousNode = e;
            }
        }

        /// <summary>
        /// Tests if the connection between two nodes is valid
        /// </summary>
        /// <param name="s">Starting node</param>
        /// <param name="e">Ending node</param>
        /// <returns>Returns true if the connection is valid. Else returns false.</returns>
        public bool TestConnectionValid(PathNode s, PathNode e)
        {
            if (s.nextNode != null || e.previousNode != null) return false;
            return true;
        }

        /// <summary>
        /// Adds a node to the Path node
        /// </summary>
        /// <param name="node">Node that you would like to add to</param>
        public void AddNode(PathNode node, PathNode newNode)
        {
            if (node.nextNode == null)
            {
                node.nextNode = newNode;
            }
        }

        /// <summary>
        /// Draws the path
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            PathNode currentNode = startNode;
            while (currentNode != null)
            {
                currentNode.Draw(spriteBatch);
                currentNode = currentNode.nextNode;
            }
        }
    }
}
